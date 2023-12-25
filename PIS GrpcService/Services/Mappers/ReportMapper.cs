using PIS_GrpcService.DataAccess;
using PIS_GrpcService.Models;
using PIS_GrpcService.PIS_GrpcService;

namespace PIS_GrpcService.Services.Mappers;

public static class ReportMapper
{
    public static GrpcReport MapToGrpc(this Report dbOrganization)
    {
        return new GrpcReport
        {
            Number = dbOrganization.Number,
            Name = dbOrganization.Name,
            Description = dbOrganization.Description
        };
    }

    public static Report MapFromGrpc(this GrpcReport dbLocality)
    {
        return new Report
        {
            Number = dbLocality.Number,
            Name = dbLocality.Name,
            Description = dbLocality.Description
        };
    }
}
