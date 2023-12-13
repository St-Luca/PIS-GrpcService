using Grpc.Core;
using Microsoft.EntityFrameworkCore;
using PIS_GrpcService;
using PIS_GrpcService.DataAccess;
using PIS_GrpcService.PIS_GrpcService;
using PIS_GrpcService.Services.Mappers;
using System.Net;

namespace PisWebApp.Services;

public class OrganizationService : Organizationer.OrganizationerBase
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

    public override Task<GrpcOrganization?> Get(IdRequest id, ServerCallContext context)
    {
        var response = _dbContext.Organizations.FirstOrDefault(o => o.Id == id.Id)?.Map();

        return Task.FromResult(response);
    }
}
