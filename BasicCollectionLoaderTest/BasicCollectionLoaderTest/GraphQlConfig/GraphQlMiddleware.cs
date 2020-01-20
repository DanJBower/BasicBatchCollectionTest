using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using GraphQL;
using GraphQL.DataLoader;
using GraphQL.Instrumentation;
using GraphQL.Types;
using GraphQL.Validation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace BasicCollectionLoaderTest.GraphQlConfig
{
    public class GraphQlMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly GraphQlSettings _settings;
        private readonly IDocumentExecuter _documentExecutor;
        private readonly IDocumentWriter _documentWriter;
        private readonly IServiceProvider _serviceProvider;

        public GraphQlMiddleware(
            RequestDelegate next,
            GraphQlSettings settings,
            IDocumentExecuter documentExecutor,
            IDocumentWriter documentWriter,
            IServiceProvider serviceProvider)
        {
            _next = next;
            _settings = settings;
            _documentExecutor = documentExecutor;
            _documentWriter = documentWriter;
            _serviceProvider = serviceProvider;
        }

        public async Task Invoke(HttpContext context, ISchema schema)
        {
            if (!IsGraphQlRequest(context))
            {
                await _next(context).ConfigureAwait(false);
                return;
            }

            await ExecuteAsync(context, schema).ConfigureAwait(false);
        }

        private bool IsGraphQlRequest(HttpContext context)
        {
            return context.Request.Path.StartsWithSegments(_settings.Path, StringComparison.OrdinalIgnoreCase)
                && string.Equals(context.Request.Method, "POST", StringComparison.OrdinalIgnoreCase);
        }

        private async Task ExecuteAsync(HttpContext context, ISchema schema)
        {
            GraphQlQuery request = Deserialize<GraphQlQuery>(context.Request.Body);
            DataLoaderDocumentListener listener = _serviceProvider.GetRequiredService<DataLoaderDocumentListener>();

            ExecutionResult result = await _documentExecutor.ExecuteAsync(_ =>
            {
                _.Schema = schema;
                _.Query = request?.Query;
                _.OperationName = request?.OperationName;
                _.Inputs = request?.Variables;
                _.UserContext = _settings.BuildUserContext?.Invoke(context);
                _.ValidationRules = DocumentValidator.CoreRules;
                _.ExposeExceptions = true;
                _.ThrowOnUnhandledException = true;
                _.Listeners.Add(listener);

                // Temporarily removed as GraphiQL runs a schema query that is quite deep.
                // _.ComplexityConfiguration = new ComplexityConfiguration { MaxDepth = 4 };
                _.EnableMetrics = _settings.EnableMetrics;
                if (_settings.EnableMetrics)
                {
                    _.FieldMiddleware.Use<InstrumentFieldsMiddleware>();
                }
            }).ConfigureAwait(false);

            await WriteResponseAsync(context, result).ConfigureAwait(false);
        }

        private async Task WriteResponseAsync(HttpContext context, ExecutionResult result)
        {
            context.Response.ContentType = "application/json";

            context.Response.StatusCode =
                result.Errors?.Any() == true ? (int)HttpStatusCode.BadRequest : (int)HttpStatusCode.OK;

            await _documentWriter.WriteAsync(context.Response.Body, result).ConfigureAwait(false);
        }

        private static T Deserialize<T>(Stream stream)
        {
            using StreamReader reader = new StreamReader(stream, Encoding.UTF8);
            using JsonTextReader jsonReader = new JsonTextReader(reader);
            JsonSerializer jsonSerializer = new JsonSerializer();
            return jsonSerializer.Deserialize<T>(jsonReader);
        }
    }
}
