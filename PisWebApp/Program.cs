using Grpc.Net.Client;
using static PIS_GrpcService.PIS_GrpcService.GrpcAnimalService;
using static PIS_GrpcService.PIS_GrpcService.GrpcApplicationService;
using static PIS_GrpcService.PIS_GrpcService.GrpcLocalityCostService;
using static PIS_GrpcService.PIS_GrpcService.GrpcLocalityService;
using static PIS_GrpcService.PIS_GrpcService.GrpcOrganizationService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddSingleton(provider =>
{
    var channel = GrpcChannel.ForAddress("http://localhost:5114"); 
    return new GrpcOrganizationServiceClient(channel);
});

builder.Services.AddSingleton(provider =>
{
    var channel = GrpcChannel.ForAddress("http://localhost:5114");
    return new GrpcAnimalServiceClient(channel);
});

builder.Services.AddSingleton(provider =>
{
    var channel = GrpcChannel.ForAddress("http://localhost:5114");
    return new GrpcLocalityServiceClient(channel);
});

builder.Services.AddSingleton(provider =>
{
    var channel = GrpcChannel.ForAddress("http://localhost:5114");
    return new GrpcLocalityCostServiceClient(channel);
});

builder.Services.AddSingleton(provider =>
{
    var channel = GrpcChannel.ForAddress("http://localhost:5114");
    return new GrpcApplicationServiceClient(channel);
});

//builder.Services.AddSingleton(provider =>
//{
//    var channel = GrpcChannel.ForAddress("http://localhost:5114");
//    return new GrpcMunicipalContractServiceClient(channel);
//});

//builder.Services.AddSingleton(provider =>
//{
//    var channel = GrpcChannel.ForAddress("http://localhost:5114");
//    return new GrpcCaptureActServiceClient(channel);
//});

//builder.Services.AddSingleton(provider =>
//{
//    var channel = GrpcChannel.ForAddress("http://localhost:5114");
//    return new GrpcReportServiceClient(channel);
//});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Animal}/{action=Index}/{id?}");

app.Run();
