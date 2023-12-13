using Grpc.Core;
using PIS_GrpcService;
using System.Net;

namespace PisWebApp.Services;

public class OrganizationService : Organizationer.OrganizationerBase
{
    private readonly ILogger<OrganizationService> _logger;
    public OrganizationService(ILogger<OrganizationService> logger)
    {
        _logger = logger;
    }

    public override Task<OrganizationArray> Get(Empty e, ServerCallContext context)
    { //Здесь из контекста энтити фреймворка данные достаем
        var response = new OrganizationArray();
        response.List.Add(new Organization { Id = 1, OrgName = "Smartway", INN = "111", KPP = "ss" });
        response.List.Add(new Organization { Id = 2, OrgName = "TumGU", INN = "222", KPP = "wtf" });
        return Task.FromResult(response);
    }
}
