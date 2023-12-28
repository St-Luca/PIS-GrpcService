using Grpc.Core;
using Microsoft.EntityFrameworkCore;
using PIS_GrpcService.DataAccess.Repositories;
using PIS_GrpcService.PIS_GrpcService;
using PIS_GrpcService.Services.Mappers;

namespace PIS_GrpcService.GrpcCore.Services;

public class OrganizationService : GrpcOrganizationService.GrpcOrganizationServiceBase
{
    private readonly OrganizationsRepository repository;
    public OrganizationService(OrganizationsRepository organizationsRepository)
    {
        repository = organizationsRepository;
    }

    public override Task<OrganizationArray> GetAll(Empty e, ServerCallContext context)
    {
        var response = repository.GetAll().Select(o => o.MapToGrpc()).ToList();

        var result = new OrganizationArray();
        result.List.AddRange(response);

        return Task.FromResult(result);
    }

    public override Task<GrpcOrganization?> Get(IdRequest request, ServerCallContext context)
    {
        var response = repository.Get(request.Id)?.MapToGrpc();

        return Task.FromResult(response);
    }

    public override Task<Empty> Edit(GrpcOrganization organization, ServerCallContext context)
    {
        try
        {
            repository.Edit(organization.MapFromGrpc());
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
            var organization = repository.Get(id.Id);

            if (organization != null)
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

    public async override Task<Empty> Add(GrpcOrganization organization, ServerCallContext context)
    {
        var entityOrganization = organization?.MapFromGrpc();

        if (entityOrganization != null)
        {
            repository.Add(entityOrganization);
        }

        return new Empty();
    }
}
