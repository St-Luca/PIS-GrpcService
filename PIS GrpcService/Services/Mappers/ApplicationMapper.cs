using PIS_GrpcService.Models;
using PIS_GrpcService.PIS_GrpcService;

namespace PIS_GrpcService.Services.Mappers;

public static class ApplicationMapper
{
    public static ApplicationArray Map(this List<Application> localities)
    {
        var locs = localities.Select(x => x.Map()).ToList();
        var res = new ApplicationArray();
        res.List.AddRange(locs);
        return res;
    }

    public static List<Application> Map(this ApplicationArray localities)
    {
        return localities.List.Select(x => x.Map()).ToList();

    }

    public static GrpcApplication Map(this Application dbApplication)
    {
        return new GrpcApplication
        {
            Id = dbApplication.Id,
            Date = Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime((DateTime)dbApplication.Date),
            ApplicantCategory = dbApplication.ApplicantCategory,
            AnimalDescription = dbApplication.AnimalDescription,
            Urgency = dbApplication.Urgency,
            Locality = dbApplication.Locality.Map(),
            //Act = dbApplication.Act.Map(),
            Organization = dbApplication.Organization.Map()
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
            Locality = dbApplication.Locality.Map(),
            //Act = dbApplication.Act.Map(),
            Organization = dbApplication.Organization.Map()
        };
    }
}
