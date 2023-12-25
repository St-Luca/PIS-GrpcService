using PIS_GrpcService.Models;
using PIS_GrpcService.PIS_GrpcService;

namespace PIS_GrpcService.Services.Mappers;

public static class LocalityMapper
{
    public static LocalityArray MapToGrpc(this List<Locality> localities)
    {
        var locs = localities.Select(x => x.MapToGrpc()).ToList();
        var res = new LocalityArray();
        res.List.AddRange(locs);
        return res;
    }

    public static List<Locality> MapFromGrpc(this LocalityArray localities)
    {
        return localities.List.Select(x => x.MapFromGrpc()).ToList();
        
    }

    public static GrpcLocality MapToGrpc(this Locality dbOrganization)
    {
        return new GrpcLocality
        {
            Id = dbOrganization.Id,
            Name = dbOrganization.Name,
        };
    }

    public static Locality MapFromGrpc(this GrpcLocality dbLocality)
    {
        return new Locality
        {
            Id = dbLocality.Id,
            Name = dbLocality.Name
        };
    }

    public static LocalityCostArray MapToGrpc(this List<LocalityCost> localities)
    {
        var locs = localities.Select(x => x.MapToGrpc()).ToList();
        var res = new LocalityCostArray();
        res.List.AddRange(locs);
        return res;
    }

    public static List<LocalityCost> MapFromGrpc(this LocalityCostArray localities)
    {
        return localities.List.Select(x => x.MapFromGrpc()).ToList();

    }

    public static GrpcLocalityCost MapToGrpc(this LocalityCost dbLocalityCost)
    {
        return new GrpcLocalityCost
        {
            IdContract = dbLocalityCost.IdContract,
            IdLocality = dbLocalityCost.IdLocality,
            Cost = dbLocalityCost.Cost

        };
    }

    public static LocalityCost MapFromGrpc(this GrpcLocalityCost dbLocalityCost)
    {
        return new LocalityCost
        {
            IdContract = dbLocalityCost.IdContract,
            IdLocality = dbLocalityCost.IdLocality,
            Cost = dbLocalityCost.Cost
        };
    }
}
