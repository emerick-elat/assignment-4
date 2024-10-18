using Autofac;
using Autofac.Core;
using Autofac.Extensions.DependencyInjection;
using Bank.ClientAPI.Profiles;
using Bank.UseCases.Account.QueryGetAccounts;
using DI;
using Entities;
using Infrastructure.Context;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Bank.ClientAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            
            builder.Services.AddControllers();
            builder.Services.AddDbContext<BankContext>();

            // Use Autofac as the service provider factory.
            builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
            // Configure Autofac container.
            builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
            {
                containerBuilder.RegisterModule(new BankAutofacModule());
            });

            builder.Services.AddAuthentication("Identity.Bearer").AddCookie("Identity.Bearer");

            builder.Services.AddIdentity<Customer, BankRole>()
                .AddApiEndpoints()
                .AddUserStore<CustomerStore>()
                .AddRoleStore<BankRoleStore>()
                .AddDefaultTokenProviders();

            //builder.Services.AddIdentityCore<BankCustomer>().AddApiEndpoints().AddEntityFrameworkStores<BankContext>();

            //builder.Services.AddIdentityApiEndpoints<BankCustomer>().AddEntityFrameworkStores<BankContext>();
            
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
                    services.GetRequiredService<BankContext>().Database.Migrate();
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
