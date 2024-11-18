using Autofac;
using Autofac.Core;
using Autofac.Extensions.DependencyInjection;
using Bank.ClientAPI.Profiles;
using Bank.UseCases.Account.QueryGetAccounts;
using DI;
using Entities;
using Infrastructure.Context;
using Infrastructure.Identity;
using Infrastructure.Multitenancy;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace Bank.ClientAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            
            builder.Services.AddControllers();
            builder.Services.AddDbContext<TenantsDBContext>();
            //builder.Services.AddDbContext<BankContext>();
            builder.Services.AddDbContextFactory<BankContext>(opt => { }, ServiceLifetime.Scoped);

            builder.Services.AddDistributedMemoryCache();
            
            // Use Autofac as the service provider factory.
            builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
            // Configure Autofac container.
            builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
            {
                containerBuilder.RegisterModule(new BankAutofacModule());
            });

            builder.Services.AddIdentity<Customer, BankRole>()
                .AddApiEndpoints()
                .AddUserStore<CustomerStore>()
                .AddRoleStore<BankRoleStore>()
                .AddSignInManager<SignInManager<Customer>>()
                .AddUserManager<UserManager<Customer>>()
                .AddDefaultTokenProviders();

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme,
                options => builder.Configuration.Bind("JwtSettings", options))
            .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme,
                options => builder.Configuration.Bind("CookieSettings", options)).AddCookie("Identity.Bearer"); ;

            //builder.Services.AddIdentityCore<BankCustomer>().AddApiEndpoints().AddEntityFrameworkStores<BankContext>();

            //builder.Services.AddIdentityApiEndpoints<BankCustomer>().AddEntityFrameworkStores<BankContext>();
            //builder.Services.AddIdentityApiEndpoints<Customer>().AddApiEndpoints();

            builder.Services.AddAutoMapper(typeof(AccountProfile));
            builder.Services.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(GetCustomerAccountsQueryHandler).Assembly));

            
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options => {
                options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Name = "Authorization"
                });
            });


            var app = builder.Build();

            app.MapIdentityApi<Customer>();

            // Migrate DB at startup
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    services.GetRequiredService<TenantsDBContext>().Database.Migrate();
                    var tenantService = services.GetService<ITenantService>();
                    var factory = services.GetService<IDbContextFactory<BankContext>>();
                    var tenants = tenantService!.GetTenants();
                    foreach (var tenant in tenants)
                    {
                        tenantService.SetTenant(tenant);
                        using var ctx = factory!.CreateDbContext();
                        services.GetRequiredService<BankContext>().Database.Migrate();
                    }
                    
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred while migrating the database: {ex.Message}");
                }
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
