
using CreditReview.API.Models;
using CreditReview.API.Repository;
using Microsoft.EntityFrameworkCore;

namespace CreditReview.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddDbContext<CreditRequestDbContext>(options =>
            {
                options.UseInMemoryDatabase(databaseName: "CreditReviewDB");
            });
            builder.Services.AddScoped<ICreditRequestRepository, CreditRequestRepository>();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();
            var baseUrl = "/creditreviews";
            app.MapGet(baseUrl, async (ICreditRequestRepository repo) => await repo.GetAll());

            app.MapPost(baseUrl, async (ICreditRequestRepository repo, CreditRequest model) =>
            {
                var response = await repo.CreateCreditRequest(model);
                return Results.Created($"{baseUrl}/{response.Id}", response);
            });

            app.MapPut(baseUrl + "/{id}/approve", async (ICreditRequestRepository repo, int id) => repo.SetCreditRequestStatus(id, CreditRequestStatus.Approved));
            app.MapPut(baseUrl + "/{id}/decline", async (ICreditRequestRepository repo, int id) => repo.SetCreditRequestStatus(id, CreditRequestStatus.Declined));

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
