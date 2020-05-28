using GraphQL;
using GraphQL.Server;
using GraphQL.Server.Ui.Playground;
using MeetupGraphQLNet.Models;
using MeetupGraphQLNet.Mutations;
using MeetupGraphQLNet.Queries;
using MeetupGraphQLNet.Schemas;
using MeetupGraphQLNet.Types;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MeetupGraphQLNet
{
    public class Startup
    {
        readonly IWebHostEnvironment WebHost;

        public Startup(IConfiguration configuration, IWebHostEnvironment webHost)
        {
            Configuration = configuration;
            WebHost = webHost;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //EF
            services.AddEntityFrameworkNpgsql()
                .AddDbContext<MeetupGraphQLNetContext>(
                    options => options.UseNpgsql(
                        Configuration.GetConnectionString("MeetupGraphQLNet")));

            //Kestrel
            services.Configure<KestrelServerOptions>(o =>
            {
                o.AllowSynchronousIO = true;
            });

            //GraphQL
            services.AddScoped<PessoaType>();
            services.AddScoped<PessoaInputType>();

            //services.AddScoped<PessoaQuery>();
            //services.AddScoped<PessoaMutation>();

            //services.AddScoped<Query>();
            //services.AddScoped<Mutation>();

            services.AddScoped<IDependencyResolver>(s => new FuncDependencyResolver(
                s.GetRequiredService));

            services.AddScoped<MeetupGraphQLNetSchema>();

            services.AddGraphQL(o =>
            {
                o.ExposeExceptions = WebHost.IsDevelopment();
                o.EnableMetrics = WebHost.IsDevelopment();
            }).AddGraphTypes(ServiceLifetime.Scoped);


            //Mvc
            services
                .AddControllers()
                .AddNewtonsoftJson()
                .AddControllersAsServices();

            services.AddMvc().SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_3_0);


            //Cors
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //Cors
            app.UseCors("CorsPolicy");

            //Mvc
            app.UseRouting();

            //GraphQL
            app.UseGraphQL<MeetupGraphQLNetSchema>();
            app.UseGraphQLPlayground(new GraphQLPlaygroundOptions());
        }
    }
}
