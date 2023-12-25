using Grpc.Core;
using Microsoft.EntityFrameworkCore;
using PIS_GrpcService.DataAccess;
using PIS_GrpcService.DataAccess.Repositories;
using PIS_GrpcService.Models;
using PIS_GrpcService.PIS_GrpcService;
using PIS_GrpcService.PIS_GrpcService.Services;
using PIS_GrpcService.Services.Mappers;

namespace PIS_GrpcService.Services;

public class LocalityCostService : GrpcLocalityCostService.GrpcLocalityCostServiceBase
{
    private readonly LocalityCostsRepository repository;
    private readonly ILogger<LocalityCostService> _logger;

    public LocalityCostService(ILogger<LocalityCostService> logger, LocalityCostsRepository localitiesRepository)
    {
        _logger = logger;
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
