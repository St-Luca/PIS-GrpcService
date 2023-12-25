using PIS_GrpcService.Models;
using PIS_GrpcService.PIS_GrpcService;
using System.Diagnostics.Contracts;
using Contract = PIS_GrpcService.Models.Contract;

namespace PIS_GrpcService.Services.Mappers;

public static class ContractMapper
{
    public static List<GrpcContract> MapToGrpc(this List<Contract> contracts)
    {
        return contracts.Select(x => x.MapToGrpc()).ToList();
    }

    public static GrpcContract MapToGrpc(this Contract dbContract)
    {
        return new GrpcContract
        {
            Id = dbContract.Id,
            ConclusionDate = Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime((DateTime)dbContract.ConclusionDate),
            EffectiveDate = Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime((DateTime)dbContract.EffectiveDate),
            Amount = dbContract.Amount,
            Performer = dbContract.Performer.MapToGrpc(),
            LocalityCosts = dbContract.LocalityCosts.MapToGrpc()
        };
    }

    public static Contract MapFromGrpc(this GrpcContract dbContract)
    {
        return new Contract
        {
            Id = dbContract.Id,
            ConclusionDate = dbContract.ConclusionDate.ToDateTime(),
            EffectiveDate = dbContract.EffectiveDate.ToDateTime(),
            Amount = dbContract.Amount,
            Performer = dbContract.Performer.MapFromGrpc(),
            LocalityCosts = dbContract.LocalityCosts.MapFromGrpc()
        };
    }
}