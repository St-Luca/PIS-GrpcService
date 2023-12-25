using Grpc.Core;
using Microsoft.EntityFrameworkCore;
using PIS_GrpcService.DataAccess;
using PIS_GrpcService.DataAccess.Repositories;
using PIS_GrpcService.Models;
using PIS_GrpcService.PIS_GrpcService;
using PIS_GrpcService.PIS_GrpcService.Services;
using PIS_GrpcService.Services.Mappers;
using System.Net.Sockets;

namespace PIS_GrpcService.Services;

public class ApplicationService : GrpcApplicationService.GrpcApplicationServiceBase
{
    private readonly CatchingApplicationsRepository repository;
    private readonly LocalitiesRepository localitiesRepository;
    private readonly OrganizationsRepository organizationsRepository;
    private readonly ILogger<ApplicationService> _logger;

    public ApplicationService(ILogger<ApplicationService> logger, CatchingApplicationsRepository applicationsRepository, 
        LocalitiesRepository localities, 
        OrganizationsRepository organizations)
    {
        _logger = logger;
        repository = applicationsRepository;
        localitiesRepository = localities;
        organizationsRepository = organizations;
    }

    public async override Task<ApplicationArray> GetAll(Empty e, ServerCallContext context)
    {
        var applications = repository.GetAll();
        var localityIds = applications.Select(app => app.IdLocality).ToList();
        var organizationIds = applications.Select(app => app.IdOrganization).ToList();

        var localities = localitiesRepository.GetAll().Where(loc => localityIds.Contains(loc.Id)).ToList();
        var organizations = organizationsRepository.GetAll().Where(org => organizationIds.Contains(org.Id)).ToList();

        var result = new ApplicationArray();

        foreach (var app in applications)
        {
            app.Locality = localities.First(d => d.Id == app.IdLocality);
            app.Organization = organizations.First(d => d.Id == app.IdOrganization);
            result.List.Add(app.MapToGrpc());
        }

        return result;
    }

    public override Task<GrpcApplication?> Get(IdRequest id, ServerCallContext context)
    {
        var response = repository.Get(id.Id)?.MapToGrpc();

        return Task.FromResult(response);
    }

    public override Task<Empty> Edit(GrpcApplication application, ServerCallContext context)
    {
        try
        {
            repository.Edit(application.MapFromGrpc());
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
            var application = repository.Get(id.Id);

            if (application != null)
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

    public async override Task<Empty> Add(GrpcApplication application, ServerCallContext context)
    {
        var entityApplication = application?.MapFromGrpc();

        if (entityApplication != null)
        {
            repository.Add(entityApplication);
        }

        return new Empty();
    }
}
