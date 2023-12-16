using Grpc.Core;
using Microsoft.EntityFrameworkCore;
using PIS_GrpcService;
using PIS_GrpcService.DataAccess;
using PIS_GrpcService.Models;
using PIS_GrpcService.PIS_GrpcService;
using PIS_GrpcService.Services.Mappers;
using System.Net;
using System.Reflection.Metadata;

namespace PisWebApp.Services;

public class OrganizationService : GrpcOrganizationService.GrpcOrganizationServiceBase
{
    private readonly ApplicationContext _dbContext;
    private readonly ILogger<OrganizationService> _logger;
    public OrganizationService(ILogger<OrganizationService> logger, ApplicationContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    public override Task<OrganizationArray> GetAll(Empty e, ServerCallContext context)
    { 
        var response = _dbContext.Organizations.Select(o => o.Map()).ToList();

        var result = new OrganizationArray();
        result.List.AddRange(response);

        return Task.FromResult(result);
    }

    public override Task<GrpcOrganization?> Get(IdRequest request, ServerCallContext context)
    {
        var response = _dbContext.Organizations.FirstOrDefault(o => o.Id == request.Id)?.Map();

        return Task.FromResult(response);
    }

    public override Task<Empty> Edit(GrpcOrganization organization, ServerCallContext context)
    {
        try
        {
            _dbContext.Update(organization.Map());
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
            var organization = await _dbContext.Organizations.FindAsync(id.Id);

            if (organization != null)
            {
                _dbContext.Organizations.Remove(organization);
                await _dbContext.SaveChangesAsync();
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
        var entityOrganization = organization?.Map();
        _dbContext.Organizations.Add(entityOrganization);
        await _dbContext.SaveChangesAsync();

        return new Empty();
    }
}
