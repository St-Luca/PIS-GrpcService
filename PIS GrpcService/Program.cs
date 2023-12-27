using PIS_GrpcService.DataAccess;
using PIS_GrpcService.Services;
//using PisWebApp.Services;
using Microsoft.EntityFrameworkCore;
using PIS_GrpcService.PIS_GrpcService.Services;
using PIS_GrpcService.Models;
using PIS_GrpcService.DataAccess.Repositories;

namespace PIS_GrpcService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            string connection = builder.Configuration.GetConnectionString("DefaultConnection");
            
            builder.Services.AddDbContext<ApplicationContext>(options => options.UseNpgsql(connection));

            builder.Services.AddScoped<OrganizationsRepository>();
            builder.Services.AddScoped<AnimalsRepository>();
            builder.Services.AddScoped<CaptureActsRepository>();
            builder.Services.AddScoped<CatchingApplicationsRepository>();
            builder.Services.AddScoped<LocalitiesRepository>();
            builder.Services.AddScoped<LocalityCostsRepository>();
            builder.Services.AddScoped<MunicipalContractsRepository>();

            // Add services to the container.
            builder.Services.AddGrpc();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            app.MapGrpcService<LocalityService>();
            app.MapGrpcService<OrganizationService>();
            app.MapGrpcService<LocalityCostService>();
            app.MapGrpcService<ContractService>();
            app.MapGrpcService<CaptureActService>();
            app.MapGrpcService<AnimalService>();
            app.MapGrpcService<ApplicationService>();
            app.MapGrpcService<ReportService>();

            app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

            app.Run();
        }
    }
}


