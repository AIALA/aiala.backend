using System.Collections.Generic;
using System.Globalization;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using aiala.Backend.Authorization.PermissionGroups;
using aiala.Backend.Data;
using aiala.Backend.Options;
using aiala.Backend.Services;
using aiala.Backend.Services.Hosted;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using xappido.Authentication.Otp;
using xappido.Authentication.Otp.Options;
using xappido.Authorization.Extensions;
using xappido.Core;
using xappido.Directory;
using xappido.Directory.Services;
using xappido.Directory.Settings.Options;
using xappido.Directory.SignalR.Hubs;
using xappido.Messaging;
using xappido.Mvc.Conventions;
using xappido.Operations;
using xappido.Output;
using xappido.Output.Template;
using xappido.Storage;
using xappido.Storage.Azure;
using xappido.Sts.ManagementClient;
using xappido.Swagger;
using xappido.Swagger.Options;

namespace aiala.Backend
{
    public class Startup
    {
        private readonly CultureInfo[] _supportedCultures = new[]
        {
            new CultureInfo("en-US"),
            new CultureInfo("en"),
            new CultureInfo("de-CH"),
            new CultureInfo("de"),
            new CultureInfo("fr-CH"),
            new CultureInfo("fr"),
            new CultureInfo("es-ES"),
            new CultureInfo("es"),
        };

        public Startup(IConfiguration configuration, IHostingEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public IConfiguration Configuration { get; }

        public IHostingEnvironment Environment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var otpSection = Configuration.GetSection("OneTimePasswordAuth");

            services.AddOptions()
                .Configure<OperationOptions>(Configuration.GetSection("Operations"))
                .Configure<DirectoryOptions>(Configuration.GetSection("Directory"))
                .Configure<DirectoryProfileOptions>(Configuration.GetSection("Directory:Profile"))
                .Configure<DirectoryLinksOptions>(Configuration.GetSection("Directory:Links"))
                .Configure<DirectoryPermissionOptions>(Configuration.GetSection("Directory:Permissions"))
                .Configure<RecaptchaOptions>(Configuration.GetSection("Recaptcha"))
                .Configure<SmtpMailOptions>(Configuration.GetSection("Notification:Smtp"))
                .Configure<StsManagementClientOptions>(Configuration.GetSection("Sts:ManagementClient"))
                .Configure<AppSwaggerOptions>(Configuration.GetSection("Swagger"))
                .Configure<AzureStorageOptions>(Configuration.GetSection("Storage:AzureBlobStorage"))
                .Configure<SettingsOptions>(Configuration.GetSection("Settings"))
                .Configure<NotificationWorkerOptions>(Configuration.GetSection("Notification:Worker"))
                .Configure<AzureVisionOptions>(Configuration.GetSection("AzureVision"))
                .Configure<OtpAuthOptions>(otpSection)
                ;

            var portalConnectionString = Configuration.GetConnectionString("PortalDatabase");

            services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
                .AddIdentityServerAuthentication(options =>
                {
                    var appSettings = Configuration.GetSection("Sts:AccessTokenValidation").Get<IdentityServerAuthenticationOptions>();
                    options.Authority = appSettings.Authority;
                    options.ApiName = appSettings.ApiName;
                    options.ApiSecret = appSettings.ApiSecret;
                    options.RequireHttpsMetadata = appSettings.RequireHttpsMetadata;
                    options.TokenRetriever = CustomTokenRetriever.FromHeaderAndQueryString;
                })
                .AddOneTimePasswordAuthentication(services, otpSection.Get<OtpAuthOptions>(), portalConnectionString);

            services.AddMessaging();
            services.AddOperations();
            services.AddLocalization();
            services.AddOutput()
                .WithEmbeddedResourceTemplateStorage(new EmbeddedResourceTemplateStorage.EmbeddedResource
                {
                    Assembly = typeof(Startup).Assembly,
                    Namespace = "aiala.Backend.Resources.Views"
                })
                .WithRazorTemplateEngine();
            services.AddSwagger();
            services.AddStorage()
                .AddAzureBlobstorage();

            services.AddDbContext<AppDbContext>(builder =>
            {
                builder.UseSqlServer(portalConnectionString, options => options.MigrationsAssembly(typeof(AppDbContext).GetAssemblyName()));
            });

            services.AddCors(options =>
            {
                options.AddPolicy("aialaCorsPolicy", builder =>
                {
                    builder.AllowCredentials();
                    builder.AllowAnyHeader();
                    builder.AllowAnyMethod();
                    builder.AllowAnyOrigin();
                });
            });

            services
                .AddMvc(options =>
                {
                    options.Conventions.Insert(0, new RouteConvention("api/"));
                    options.ConfigureModelBindingMessages(services);
                })
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                    options.SerializerSettings.NullValueHandling = NullValueHandling.Include;
                    options.SerializerSettings.Converters.Add(new StringEnumConverter());
                    options.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
                });

            services.AddRouting(options => options.LowercaseUrls = true);

            services.AddPolicyAuthorization()
                .AddPermissions();

            services.AddDirectory<AppDbContext, Tenant, Account, User>()
                .AddPermissionGroup<AppAdministratorPermissionGroup>()
                .AddPermissionGroup<MobileAppUserPermissionGroup>()
                .AddPermissionGroup<WebAppUserPermissionGroup>()
                .AddSignalR()
                .AddSettings()
                .AddRecaptcha();

            services.AddUserManagementClient();

            services.AddTransient<IPictureHelperService, PictureHelperService>();
            services.AddSingleton<IEmergencyNotificationsQueueService, EmergencyNotificationsQueueService>();
            services.AddHostedService<EmergencyNotificationsHostedService>();
            services.Replace(ServiceDescriptor.Transient<IProfileService, ProfileService>());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(
            IApplicationBuilder app,
            IHostingEnvironment env,
            AppDbContext dbContext)
        {
            dbContext.Database.Migrate();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("aialaCorsPolicy");
            app.UseAuthentication();

            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture(_supportedCultures[0], _supportedCultures[0]),
                SupportedCultures = _supportedCultures,
                SupportedUICultures = _supportedCultures,
                FallBackToParentCultures = true,
                FallBackToParentUICultures = true,
                RequestCultureProviders = new List<IRequestCultureProvider>
                {
                    new AcceptLanguageHeaderRequestCultureProvider()
                }
            });

            app.UseSwagger();

            app.UseOneTimePassword();
            app.UseOperations();
            app.UseDirectory<AppDbContext, Tenant, Account, User>();

            app.UseSignalR(route =>
            {
                route.MapHub<UserManagementHub>("/hubs/usermanagement");
            });

            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseMvc();
        }
    }
}
