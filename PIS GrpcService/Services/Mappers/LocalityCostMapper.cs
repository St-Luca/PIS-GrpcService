using PIS_GrpcService.Models;
using PIS_GrpcService.PIS_GrpcService;

namespace PIS_GrpcService.Services.Mappers;

public static class LocalityCostMapper
{
    public static LocalityCostArray Map(this List<LocalityCost> localities)
    {
        var locs = localities.Select(x => x.Map()).ToList();
        var res = new LocalityCostArray();
        res.List.AddRange(locs);
        return res;
    }

    public static List<LocalityCost> Map(this LocalityCostArray localities)
    {
        return localities.List.Select(x => x.Map()).ToList();

    }

    public static GrpcLocalityCost Map(this LocalityCost dbLocalityCost)
    {
        return new GrpcLocalityCost
        {
            IdContract = dbLocalityCost.IdContract,
            IdLocality = dbLocalityCost.IdLocality,
            Cost = dbLocalityCost.Cost

        };
    }

    public static LocalityCost Map(this GrpcLocalityCost dbLocalityCost)
    {
        return new LocalityCost
        {
            IdContract = dbLocalityCost.IdContract,
            IdLocality = dbLocalityCost.IdLocality,
            Cost = dbLocalityCost.Cost
        };
    }
}
