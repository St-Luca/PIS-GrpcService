using PIS_GrpcService.Models;
using PIS_GrpcService.PIS_GrpcService;

namespace PIS_GrpcService.Services.Mappers;

public static class LocalityMapper
{
    public static LocalityArray Map(this List<Locality> localities)
    {
        var locs = localities.Select(x => x.Map()).ToList();
        var res = new LocalityArray();
        res.List.AddRange(locs);
        return res;
    }

    public static List<Locality> Map(this LocalityArray localities)
    {
        return localities.List.Select(x => x.Map()).ToList();
        
    }

    public static GrpcLocality Map(this Locality dbOrganization)
    {
        return new GrpcLocality
        {
            Id = dbOrganization.Id,
            Name = dbOrganization.Name,
        };
    }

    public static Locality Map(this GrpcLocality dbLocality)
    {
        return new Locality
        {
            Id = dbLocality.Id,
            Name = dbLocality.Name
        };
    }
}
