using PIS_GrpcService.Models;
using PIS_GrpcService.PIS_GrpcService;
using System.Diagnostics.Contracts;
using Contract = PIS_GrpcService.Models.Contract;

namespace PIS_GrpcService.Services.Mappers;

public static class ContractMapper
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
            ConclusionDate = Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime((DateTime)dbContract.ConclusionDate),
            EffectiveDate = Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime((DateTime)dbContract.EffectiveDate),
            Amount = dbContract.Amount,
            Performer = dbContract.Performer.Map(),
            LocalityCosts = dbContract.LocalityCosts.Map()
        };
    }

    public static Contract Map(this GrpcContract dbContract)
    {
        return new Contract
        {
            Id = dbContract.Id,
            ConclusionDate = dbContract.ConclusionDate.ToDateTime(),
            EffectiveDate = dbContract.EffectiveDate.ToDateTime(),
            Amount = dbContract.Amount,
            Performer = dbContract.Performer.Map(),
            LocalityCosts = dbContract.LocalityCosts.Map()
        };
    }
}