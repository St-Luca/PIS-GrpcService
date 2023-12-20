using Grpc.Core;
using Microsoft.EntityFrameworkCore;
using PIS_GrpcService.DataAccess;
using PIS_GrpcService.Services.Mappers;

namespace PIS_GrpcService.PIS_GrpcService.Services;

public class CaptureActService : GrpcCaptureActService.GrpcCaptureActServiceBase
{
    private readonly ApplicationContext _dbContext;
    private readonly ILogger<CaptureActService> _logger;
    public CaptureActService(ILogger<CaptureActService> logger, ApplicationContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    public override Task<CaptureActArray> GetAll(Empty e, ServerCallContext context)
    { 
        var response = _dbContext.CaptureActs.Select(o => o.Map()).ToList();

        var result = new CaptureActArray();
        result.List.AddRange(response);

        return Task.FromResult(result);
    }

    public override Task<GrpcCaptureAct?> Get(IdRequest request, ServerCallContext context)
    {
        var response = _dbContext.CaptureAct.FirstOrDefault(o => o.Id == request.Id)?.Map();

        return Task.FromResult(response);
    }

    public override Task<Empty> Edit(GrpcCaptureAct act, ServerCallContext context)
    {
        try
        {
            _dbContext.Update(act.Map());
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
            var act = await _dbContext.CaptureAct.FindAsync(id.Id);

            if (act != null)
            {
                _dbContext.Organizations.Remove(act);
                await _dbContext.SaveChangesAsync();
            }

        }
        catch (DbUpdateConcurrencyException)
        {
            //_logger.Log();
        }

        return new Empty();
    }

    public async override Task<Empty> Add(GrpcCaptureAct act, ServerCallContext context)
    {
        var entityCaptureAct = act?.Map();
        _dbContext.Organizations.Add(entityCaptureAct);
        await _dbContext.SaveChangesAsync();

        return new Empty();
    }
}
