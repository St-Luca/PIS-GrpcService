using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.EntityFrameworkCore;
using PIS_GrpcService.DataAccess;
using PIS_GrpcService.DataAccess.Repositories;
using PIS_GrpcService.Models;
using PIS_GrpcService.Services.Mappers;
using System.Diagnostics.Contracts;

namespace PIS_GrpcService.PIS_GrpcService.Services;

public class CaptureActService : GrpcCaptureActService.GrpcCaptureActServiceBase
{
    private readonly CaptureActsRepository repository;
    private readonly LocalitiesRepository localitiesRepository;
    private readonly OrganizationsRepository organizationsRepository;
    private readonly MunicipalContractsRepository contractsRepository;
    private readonly CatchingApplicationsRepository applicationsRepository;
    private readonly ILogger<CaptureActService> _logger;
    public CaptureActService(ILogger<CaptureActService> logger, 
        CaptureActsRepository actRepository, 
        LocalitiesRepository localitiesRepository,
        OrganizationsRepository organizationsRepository,
        MunicipalContractsRepository contractsRepository,
        CatchingApplicationsRepository applicationsRepository)
    {
        _logger = logger;
        repository = actRepository;
        this.localitiesRepository = localitiesRepository;
        this.organizationsRepository = organizationsRepository;
        this.contractsRepository = contractsRepository;
        this.applicationsRepository = applicationsRepository;
    }

    public async override Task<CaptureActArray> GetAll(Empty e, ServerCallContext context)
    {
        var acts = repository.GetAll();

        var contractIds = acts.Select(app => app.IdContract).ToList();

        var contracts = contractsRepository.GetAll()
            .Where(contract => contractIds.Contains(contract.Id))
            .ToList();

        var applications = applicationsRepository.GetAll()
            .Where(app => acts.Select(act => act.Id).Contains(((int)app.IdAct)))
            .ToList();

        /*var organizationIds = acts.Select(app => app.IdOrganization).ToList();
        var localityIds = acts.Select(app => app.IdLocality).ToList();
        var contractIds = acts.Select(app => app.IdContract).ToList();

        var organizations = organizationsRepository.GetAll()
            .Where(org => organizationIds.Contains(org.Id))
            .ToList();

        var localities = localitiesRepository.GetAll()
            .Where(loc => localityIds.Contains(loc.Id))
            .ToList();

        var contracts = contractsRepository.GetAll()
            .Where(contract => contractIds.Contains(contract.Id))
            .ToList();

        var applications = applicationsRepository.GetAll()
            .Where(app => acts.Select(act => act.Id).Contains(app.IdAct))
            .ToList();*/

        var result = new CaptureActArray();

        foreach (var act in acts)
        {
            /*act.Performer = organizations.First(d => d.Id == act.IdOrganization);
            act.Contract = contracts.First(c => c.Id == act.IdContract);
            act.Applications = applications.Where(a => a.IdAct == act.Id).ToList();*/


            //act.Applications = applications;

            act.Contract = contracts.First(c => c.Id == act.IdContract);
            act.Applications = applications.Where(a => a.IdAct == act.Id).ToList();

            result.List.Add(act.MapToGrpc());
        }

        return result;
    }

    public override Task<GrpcCaptureAct?> Get(IdRequest request, ServerCallContext context)
    {
        var response = repository.Get(request.Id)?.MapToGrpc();

        return Task.FromResult(response);
    }

    public override Task<Empty> Edit(GrpcCaptureAct act, ServerCallContext context)
    {
        try
        {
            repository.Edit(act.MapFromGrpc());
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
            var act = repository.Get(id.Id);

            if (act != null)
            {
                repository.Delete(id.Id);
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
        var entityCaptureAct = act?.MapFromGrpc();

        if (entityCaptureAct != null)
        {
            repository.Add(entityCaptureAct);
        }

        return new Empty();
    }
}