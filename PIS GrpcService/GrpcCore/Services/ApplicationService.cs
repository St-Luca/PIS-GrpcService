using Grpc.Core;
using Microsoft.EntityFrameworkCore;
using PIS_GrpcService.DataAccess.Repositories;
using PIS_GrpcService.Models;
using PIS_GrpcService.PIS_GrpcService;
using PIS_GrpcService.Services.Mappers;
using Empty = PIS_GrpcService.PIS_GrpcService.Empty;

namespace PIS_GrpcService.GrpcCore.Services;


public class ApplicationService : GrpcApplicationService.GrpcApplicationServiceBase
{
    private readonly CatchingApplicationsRepository repository;

    public ApplicationService(CatchingApplicationsRepository applicationsRepository)
    {
        repository = applicationsRepository;
    }

    public async override Task<ApplicationArray> GetAll(Empty e, ServerCallContext context)
    {
        var applications = repository.GetAll();

        var result = new ApplicationArray();

        foreach (var app in applications)
        {
            result.List.Add(app.MapToGrpc());
        }

        return result;
    }

    public async override Task<GrpcApplication?> Get(IdRequest id, ServerCallContext context)
    {
        var response = repository.Get(id.Id);

        return response.MapToGrpc();
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
