using Grpc.Core;
using PIS_GrpcService.DataAccess.Repositories;
using PIS_GrpcService.PIS_GrpcService;
using PIS_GrpcService.Services.Mappers;

namespace PIS_GrpcService.GrpcCore.Services;

public class LocalityCostService : GrpcLocalityCostService.GrpcLocalityCostServiceBase
{
    private readonly LocalityCostsRepository repository;

    public LocalityCostService(LocalityCostsRepository localitiesRepository)
    {
        repository = localitiesRepository;
    }

    public override Task<LocalityCostArray> GetAll(Empty e, ServerCallContext context)
    {
        var response = repository.GetAll().Select(o => o.MapToGrpc()).ToList();

        var result = new LocalityCostArray();
        result.List.AddRange(response);

        return Task.FromResult(result);
    }

    public override Task<GrpcLocalityCost?> Get(IdRequest id, ServerCallContext context)
    {
        var response = repository.Get(id.Id)?.MapToGrpc();

        return Task.FromResult(response);
    }
}
