using PIS_GrpcService.Models;
using PIS_GrpcService.PIS_GrpcService;

namespace PIS_GrpcService.Services.Mappers;

public static class LocalityMapper
{
    public static List<GrpcLocality> Map(this List<Locality> localities)
    {
        return localities.Select(x => x.Map()).ToList();
    }

    public static GrpcLocality Map(this Locality dbLocality)
    {
        return new GrpcLocality
        {
            Id = dbLocality.Id,
           // Date = Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime((DateTime)dbLocality.Date),
            ApplicantCategory = dbLocality.ApplicantCategory,
            AnimalDescription = dbLocality.AnimalDescription,
            Urgency = dbLocality.Urgency,
            Locality = dbLocality.Locality,
            Organization = dbLocality.Organization
        };
    }

    public static Locality Map(this GrpcLocality dbLocality)
    {
        return new Locality
        {
            Id = dbLocality.Id,
            Date = dbLocality.Date.ToDateTime(),
            ApplicantCategory = dbLocality.ApplicantCategory,
            AnimalDescription = dbLocality.AnimalDescription,
            Urgency = dbLocality.Urgency,
            Locality = dbLocality.Locality,
            Organization = dbLocality.Organization
        };
    }
}
