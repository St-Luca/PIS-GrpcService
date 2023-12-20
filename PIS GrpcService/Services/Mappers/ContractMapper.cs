using PIS_GrpcService.Models;
using PIS_GrpcService.PIS_GrpcService;
using System.Diagnostics.Contracts;

namespace PIS_GrpcService.Services.Mappers;

/*public static class ContractMapper
{
    public static List<GrpcContract> Map(this List<Contract> contracts)
    {
        return contracts.Select(x => x.Map()).ToList();
    }

    public static GrpcContract Map(this Contract dbContract)
    {
        return new GrpcContract
        {
            Id = dbContract.Id,
            ConcDate = dbContract.ConcDate,
            EffDate = dbContract.EffDate,
            Amount = dbContract.Amount,
            Performer = dbContract.Performer,
            Customer = dbContract.Customer,
            Localities = dbContract.Localities
        };
    }

    public static Contract Map(this GrpcContract dbContract)
    {
        return new Contract
        {
            Id = dbContract.Id,
            ConcDate = dbContract.ConcDate,
            EffDate = dbContract.EffDate,
            Amount = dbContract.Amount,
            Performer = dbContract.Performer,
            Customer = dbContract.Customer,
            Localities = dbContract.Localities
        };
    }
}*/