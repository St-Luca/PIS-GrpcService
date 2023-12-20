using Grpc.Core;
using Microsoft.EntityFrameworkCore;
using PIS_GrpcService.DataAccess;
using PIS_GrpcService.Services.Mappers;

namespace PIS_GrpcService.PIS_GrpcService.Services;

public class LocalityService : GrpcLocalityService.GrpcLocalityServiceBase
{
    private readonly ApplicationContext _dbContext;
    private readonly ILogger<LocalityService> _logger;
    public LocalityService(ILogger<LocalityService> logger, ApplicationContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    public override Task<LocalityArray> GetAll(Empty e, ServerCallContext context)
    {
        var response = _dbContext.Localities.Select(o => o.Map()).ToList();

        var result = new LocalityArray();
        result.List.AddRange(response);

        return Task.FromResult(result);
    }

    public override Task<GrpcLocality?> Get(IdRequest request, ServerCallContext context)
    {
        var response = _dbContext.Localities.FirstOrDefault(o => o.Id == request.Id)?.Map();

        return Task.FromResult(response);
    }
}
