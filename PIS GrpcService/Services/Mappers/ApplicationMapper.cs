using PIS_GrpcService.Models;
using PIS_GrpcService.PIS_GrpcService;

namespace PIS_GrpcService.Services.Mappers;

public static class ApplicationMapper
{
    public static List<GrpcApplication> Map(this List<Application> applications)
    {
        return applications.Select(x => x.Map()).ToList();
    }

    public static GrpcApplication Map(this Application dbApplication)
    {
        return new GrpcApplication
        {
            Id = dbApplication.Id,
           // Date = Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime((DateTime)dbApplication.Date),
            ApplicantCategory = dbApplication.ApplicantCategory,
            AnimalDescription = dbApplication.AnimalDescription,
            Urgency = dbApplication.Urgency,
            Locality = dbApplication.Locality,
            Organization = dbApplication.Organization
        };
    }

    public static Application Map(this GrpcApplication dbApplication)
    {
        return new Application
        {
            Id = dbApplication.Id,
            Date = dbApplication.Date.ToDateTime(),
            ApplicantCategory = dbApplication.ApplicantCategory,
            AnimalDescription = dbApplication.AnimalDescription,
            Urgency = dbApplication.Urgency,
            Locality = dbApplication.Locality,
            Organization = dbApplication.Organization
        };
    }
}
