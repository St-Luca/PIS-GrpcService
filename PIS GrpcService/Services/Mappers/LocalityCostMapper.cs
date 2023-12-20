using PIS_GrpcService.Models;
using PIS_GrpcService.PIS_GrpcService;

namespace PIS_GrpcService.Services.Mappers;

public static class LocalityCostMapper
{
    public static List<GrpcLocalityCost> Map(this List<LocalityCost> localityCosts)
    {
        return localityCosts.Select(x => x.Map()).ToList();
    }

    public static GrpcLocalityCost Map(this LocalityCost dbLocalityCost)
    {
        return new GrpcLocalityCost
        {
            IdCost = dbLocalityCost.IdCost,
            IdContract = dbLocalityCost.IdContract,
            IdLocality = dbLocalityCost.IdLocality,
            Cost = dbLocalityCost.Cost

        };
    }

    public static LocalityCost Map(this GrpcLocalityCost dbLocalityCost)
    {
        return new LocalityCost
        {
            IdCost = dbLocalityCost.IdCost,
            IdContract = dbLocalityCost.IdContract,
            IdLocality = dbLocalityCost.IdLocality,
            Cost = dbLocalityCost.Cost
        };
    }
}
