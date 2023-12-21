using Google.Protobuf.WellKnownTypes;
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

    public async override Task<CaptureActArray> GetAll(Empty e, ServerCallContext context)
    {
       // var response = _dbContext.Acts.Select(o => o.Map()).ToList();
        var acts = await _dbContext.Acts.ToListAsync();
        //var localityIds = acts.Select(app => app.IdLocality).ToList();
        var organizationIds = acts.Select(app => app.IdOrganization).ToList();
        var animalIds = acts.Select(app => app.IdCapturedAnimal).ToList();
        var localityIds = acts.Select(app => app.Locality.Id).ToList();
        var applicationIds = acts.SelectMany(app => app.Applications.Select(l => l.Id)).ToList();

        var animals = await _dbContext.Animals.Where(loc => animalIds.Contains(loc.Id)).ToListAsync();
        var organizations = await _dbContext.Organizations.Where(org => organizationIds.Contains(org.Id)).ToListAsync();
        var localities = await _dbContext.Localities.Where(org => localityIds.Contains(org.Id)).ToListAsync();
        var applications = await _dbContext.Applications.Where(org => applicationIds.Contains(org.Id)).ToListAsync();

        var result = new CaptureActArray();

        foreach (var act in acts)
        {
            act.CapturedAnimal = animals.FirstOrDefault(d => d.Id == act.IdCapturedAnimal);
            act.Performer = organizations.FirstOrDefault(d => d.Id == act.IdOrganization);
            //act.Localities = localities.Where(d => d.Id == act.IdOrganization); //loc cost
            act.Applications = applications.Where(d => d.IdAct == act.Id).ToList();
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