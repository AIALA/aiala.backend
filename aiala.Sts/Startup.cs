using System.Globalization;
using IdentityServer4.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using xappido.Operations;
using xappido.Sts;
using xappido.Sts.Options;

namespace aiala.Sts
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions()
                .Configure<StsOptions>(Configuration.GetSection("Sts"))
                .Configure<StsUiOptions>(Configuration.GetSection("Sts:Ui"))
                .Configure<IdentityServerOptions>(Configuration.GetSection("Sts:IdentityServer"))
                .Configure<AccessTokenValidationOptions>(Configuration.GetSection("Sts:AccessTokenValidation"))
                .Configure<StsManagementClientOptions>(Configuration.GetSection("Sts:ManagementClient"))
                .Configure<OperationOptions>(Configuration.GetSection("Operations"));

            services.AddStsServer()
                .AddUserManagementApi()
                .AddLocalization(new CultureInfo("de-CH"), "ui_locales", new CultureInfo("de-CH"), new CultureInfo("en-US"))
                .AddApplicationInsights()
                .AddIdentityServer();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseStsServer(env)
                .SeedManagementResources();
        }
    }
}
