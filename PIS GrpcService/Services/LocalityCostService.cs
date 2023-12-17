using Grpc.Core;
using Microsoft.EntityFrameworkCore;
using PIS_GrpcService.DataAccess;
using PIS_GrpcService.Models;
using PIS_GrpcService.PIS_GrpcService;
using PIS_GrpcService.PIS_GrpcService.Services;
using PIS_GrpcService.Services.Mappers;

namespace PIS_GrpcService.Services;

public class LocalityCostService : GrpcLocalityCostService.GrpcLocalityCostServiceBase
{
    private readonly ApplicationContext _dbContext;
    private readonly ILogger<LocalityCostService> _logger;

    public LocalityCostService(ILogger<LocalityCostService> logger, ApplicationContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    public override Task<LocalityCostArray> GetAll(Empty e, ServerCallContext context)
    {
        var response = _dbContext.LocalityCosts.Select(o => o.Map()).ToList();

        var result = new LocalityCostArray();
        result.List.AddRange(response);

        return Task.FromResult(result);
    }

    public override Task<GrpcLocalityCost?> Get(IdRequest id, ServerCallContext context)
    {
        var response = _dbContext.LocalityCosts.FirstOrDefault(o => o.IdCost == id.Id)?.Map();

        return Task.FromResult(response);
    }

    public override Task<Empty> Edit(GrpcLocalityCost localityCost, ServerCallContext context)
    {
        try
        {
            _dbContext.Update(localityCost.Map());
            _dbContext.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            //_logger.Log();
        }

        return Task.FromResult(new Empty());
    }

    public async override Task<Empty> Delete(IdRequest id, ServerCallContext context)
    {
        try
        {
            var localityCost = await _dbContext.LocalityCosts.FindAsync(id.Id);

            if (localityCost != null)
            {
                _dbContext.LocalityCosts.Remove(localityCost);
                await _dbContext.SaveChangesAsync();
            }

        }
        catch (DbUpdateConcurrencyException)
        {
            //_logger.Log();
        }

        return new Empty();
    }

    public async override Task<Empty> Add(GrpcLocalityCost localityCost, ServerCallContext context)
    {
        var entityLocalityCost = localityCost?.Map();
        _dbContext.LocalityCosts.Add(entityLocalityCost);
        await _dbContext.SaveChangesAsync();

        return new Empty();
    }
}
