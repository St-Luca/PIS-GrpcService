using PIS_GrpcService.Models;
using PIS_GrpcService.PIS_GrpcService;

namespace PIS_GrpcService.Services.Mappers;

public static class ApplicationMapper
{
    public static ApplicationArray MapToGrpc(this List<Application> localities)
    {
        var locs = localities.Select(x => x.MapToGrpc()).ToList();
        var res = new ApplicationArray();
        res.List.AddRange(locs);
        return res;
    }

    public static List<Application> MapFromGrpc(this ApplicationArray localities, Organization org, Locality loc)
    {
        return localities.List.Select(x => x.MapFromGrpc(loc, org)).ToList();

    }

    public static GrpcApplication MapToGrpc(this Application dbApplication)
    {
        return new GrpcApplication
        {
            Id = dbApplication.Id,
            Date = Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime((DateTime)dbApplication.Date),
            ApplicantCategory = dbApplication.ApplicantCategory,
            AnimalDescription = dbApplication.AnimalDescription,
            Urgency = dbApplication.Urgency,
            IdLocality = dbApplication.Locality.Id,
            Locality = dbApplication.Locality.MapToGrpc(),
            IdOrganization = dbApplication.Organization.Id
        };
    }

    public static Application MapFromGrpc(this GrpcApplication dbApplication, Locality loc, Organization org)
    {
        return new Application
        {
            Id = dbApplication.Id,
            Date = dbApplication.Date.ToDateTime(),
            ApplicantCategory = dbApplication.ApplicantCategory,
            AnimalDescription = dbApplication.AnimalDescription,
            Urgency = dbApplication.Urgency,
            Locality = loc,
            Organization = org
        };
    }
}
