using System;
using BasicCollectionLoaderTest.GraphQlConfig;
using BasicCollectionLoaderTest.School;
using BasicCollectionLoaderTest.Types;
using GraphiQl;
using GraphQL;
using GraphQL.DataLoader;
using GraphQL.NewtonsoftJson;
using GraphQL.Types;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using SchoolEfCore.Context;
using SchoolEfCore.Interfaces;

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
            services.AddScoped<PupilType>();
            services.AddScoped<ClassType>();
            services.AddDbContextPool<SchoolContext>(builder => builder.UseLoggerFactory(MyLoggerFactory)
                .EnableSensitiveDataLogging()
                .UseMySql(
                    $"Server={SchoolContext.Server};Database={SchoolContext.DatabaseName};" +
                    $"User={SchoolContext.User};Password={SchoolContext.Password};",
                    mySqlOptions =>
                    {
                        mySqlOptions.ServerVersion(new Version(8, 0, 18), ServerType.MySql);
                    }));

            services.AddScoped<IDbContext>(provider => provider.GetService<SchoolContext>());
            services.AddScoped<DbContext>(provider => provider.GetService<SchoolContext>());
            services.AddScoped<ISchoolContext>(provider => provider.GetService<SchoolContext>());
            services.AddControllers();

            services.Configure<IISServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });
        }

        private static readonly LoggerFactory MyLoggerFactory =
            new LoggerFactory(new[] {
                new Microsoft.Extensions.Logging.Debug.DebugLoggerProvider()
            });

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
