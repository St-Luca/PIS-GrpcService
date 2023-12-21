using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.EntityFrameworkCore;
using PIS_GrpcService.DataAccess;
using PIS_GrpcService.Models;
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

    public async override Task<CaptureActArray> GetAll(Empty e, ServerCallContext context)
    {
        var acts = await _dbContext.Acts
            .Include(a => a.Animals)
            .Include(a => a.Applications)
            .ToListAsync();

        var organizationIds = acts.Select(app => app.IdOrganization).ToList();
        var localityIds = acts.Select(app => app.IdLocality).ToList();

        var organizations = await _dbContext.Organizations
            .Where(org => organizationIds.Contains(org.Id))
            .ToListAsync();

        var localities = await _dbContext.Localities
            .Where(loc => localityIds.Contains(loc.Id))
            .ToListAsync();

        var result = new CaptureActArray();

        foreach (var act in acts)
        {
            act.Performer = organizations.FirstOrDefault(d => d.Id == act.IdOrganization);
            act.Locality = localities.FirstOrDefault(d => d.Id == act.IdLocality);
            act.Locality.LocalityCosts = new List<LocalityCost> { new LocalityCost { IdContract = act.IdContract, IdLocality = act.IdLocality, Cost = 50 } };
            result.List.Add(act.Map());
        }

        return result;
    }

    public override Task<GrpcCaptureAct?> Get(IdRequest request, ServerCallContext context)
    {
        var response = _dbContext.Acts.FirstOrDefault(o => o.Id == request.Id)?.Map();

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
            var act = await _dbContext.Acts.FindAsync(id.Id);

            if (act != null)
            {
                _dbContext.Acts.Remove(act);
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
        _dbContext.Acts.Add(entityCaptureAct);
        await _dbContext.SaveChangesAsync();

        return new Empty();
    }
}