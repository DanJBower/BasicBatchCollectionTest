using BasicCollectionLoaderTest.GraphQlConfig;
using BasicCollectionLoaderTest.School;
using GraphiQl;
using GraphQL;
using GraphQL.DataLoader;
using GraphQL.NewtonsoftJson;
using GraphQL.Types;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BasicCollectionLoaderTest
{
    public class Startup
    {
        private const string GraphQlPath = "/graphql";

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(options => options.EnableEndpointRouting = false);
            services.AddSingleton<IDocumentExecuter, DocumentExecuter>();
            services.AddSingleton<IDocumentWriter, DocumentWriter>();
            services.AddSingleton<IDataLoaderContextAccessor, DataLoaderContextAccessor>();
            services.AddSingleton<DataLoaderDocumentListener>();
            services.AddScoped<SchoolQuery>();
            services.AddScoped<ISchema, SchoolSchema>();



            // TODO Try removing this
            // This has to be allowed as the GraphQL Middleware Deserialize method is required to be synchronous
            // for now. Hopefully they will fix in an update and this can be removed.
            /*services.Configure<IISServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });*/
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseMiddleware<GraphQlMiddleware>(new GraphQlSettings
            {
                Path = GraphQlPath,
                BuildUserContext = ctx => new GraphQlUserContext
                {
                    User = ctx.User,
                },
                EnableMetrics = true,
            });

            app.UseGraphiQl(GraphQlPath);

            app.UseMvc();
        }
    }
}
