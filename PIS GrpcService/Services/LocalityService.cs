using Grpc.Core;
using Microsoft.EntityFrameworkCore;
using PIS_GrpcService.DataAccess;
using PIS_GrpcService.PIS_GrpcService;
using PIS_GrpcService.PIS_GrpcService.Services;
using PIS_GrpcService.Services.Mappers;

namespace PIS_GrpcService.Services;

public class LocalityService : GrpcLocalityService.GrpcLocalityServiceBase
{
    private readonly LocalityContext _dbContext;
    private readonly ILogger<LocalityService> _logger;

    public LocalityService(ILogger<LocalityService> logger, LocalityContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    public override Task<LocalityArray> GetAll(Empty e, ServerCallContext context)
    {
        var response = _dbContext.Localitys.Select(o => o.Map()).ToList();

        var result = new LocalityArray();
        result.List.AddRange(response);

        return Task.FromResult(result);
    }

    public override Task<GrpcLocality?> Get(IdRequest id, ServerCallContext context)
    {
        var response = _dbContext.Localitys.FirstOrDefault(o => o.Id == id.Id)?.Map();

        return Task.FromResult(response);
    }

    public override Task<Empty> Edit(GrpcLocality locality, ServerCallContext context)
    {
        try
        {
            _dbContext.Update(locality.Map());
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
            var locality = await _dbContext.Localitys.FindAsync(id.Id);

            if (locality != null)
            {
                _dbContext.Localitys.Remove(locality);
                await _dbContext.SaveChangesAsync();
            }

        }
        catch (DbUpdateConcurrencyException)
        {
            //_logger.Log();
        }

        return new Empty();
    }

    public async override Task<Empty> Add(GrpcLocality locality, ServerCallContext context)
    {
        var entityLocality = locality?.Map();
        _dbContext.Localitys.Add(entityLocality);
        await _dbContext.SaveChangesAsync();

        return new Empty();
    }
}
