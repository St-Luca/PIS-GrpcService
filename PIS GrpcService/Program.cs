using PIS_GrpcService.DataAccess;
using PIS_GrpcService.Services;
//using PisWebApp.Services;
using Microsoft.EntityFrameworkCore;
using PIS_GrpcService.PIS_GrpcService.Services;
using PIS_GrpcService.Models;

namespace PIS_GrpcService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            string connection = builder.Configuration.GetConnectionString("DefaultConnection");
            // ��������� �������� ApplicationContext � �������� ������� � ����������
            builder.Services.AddDbContext<ApplicationContext>(options => options.UseNpgsql(connection));

            // Additional configuration is required to successfully run gRPC on macOS.
            // For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682

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
            //app.MapGrpcService<AnimalCardService>();
            app.MapGrpcService<ApplicationService>();
            app.MapGrpcService<ReportService>();

            app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

            app.Run();
        }
    }
}