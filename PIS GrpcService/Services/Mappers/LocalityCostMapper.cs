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
            //Id_Cost = dbLocalityCost.Id_Cost,
            //Id_Contract = dbLocalityCost.Id_Contract,
            //Id_Locality = dbLocalityCost.Id_Locality,
            Cost = dbLocalityCost.Cost

        };
    }

    public static LocalityCost Map(this GrpcLocalityCost dbLocalityCost)
    {
        return new LocalityCost
        {
            //Id_Cost = dbLocalityCost.Id_Cost,
            //Id_Contract = dbLocalityCost.Id_Contract,
            //Id_Locality = dbLocalityCost.Id_Locality,
            Cost = dbLocalityCost.Cost
        };
    }
}
