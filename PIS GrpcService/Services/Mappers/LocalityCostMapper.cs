using PIS_GrpcService.Models;
using PIS_GrpcService.PIS_GrpcService;

namespace PIS_GrpcService.Services.Mappers;

public static class LocalityCostMapper
{
    public static List<GrpcLocalityCost> Map(this List<LocalityCost> localityCost)
    {
        return localityCost.Select(x => x.Map()).ToList();
    }

    public static GrpcLocalityCost Map(this LocalityCost dbLocalityCost)
    {
        return new GrpcLocalityCost
        {
            Id_Contract = dbLocalityCost.Id_Contract,
            Id_Locality = dbLocalityCost.Id_Locality,
            Cost = dbLocalityCost.Cost
        };
    }

    public static LocalityCost Map(this GrpcLocalityCost dbLocalityCost)
    {
        return new LocalityCost
        {
            Id_Contract = dbLocalityCost.Id_Contract,
            Id_Locality = dbLocalityCost.Id_Locality,
            Cost = dbLocalityCost.Cost
        };
    }
}
