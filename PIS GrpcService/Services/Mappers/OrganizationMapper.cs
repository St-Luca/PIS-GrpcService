using PIS_GrpcService.Models;
using PIS_GrpcService.PIS_GrpcService;

namespace PIS_GrpcService.Services.Mappers;

public static class OrganizationMapper
{
    public static List<GrpcOrganization> Map(this List<Organization> organizations)
    {
        return organizations.Select(x => x.Map()).ToList();
    }

    public static GrpcOrganization Map(this Organization dbOrganization)
    {
        return new GrpcOrganization
        {
            Id = dbOrganization.Id,
            OrgName = dbOrganization.OrgName,
            INN = dbOrganization.INN,
            KPP = dbOrganization.KPP,
            OrgAddress = dbOrganization.OrgAddress,
            OrgType = dbOrganization.OrgType,
            OrgAttribute = dbOrganization.OrgAttribute
        };
    }

    public static Organization Map(this GrpcOrganization dbOrganization)
    {
        return new Organization
        {
            Id = dbOrganization.Id,
            OrgName = dbOrganization.OrgName,
            INN = dbOrganization.INN,
            KPP = dbOrganization.KPP,
            OrgAddress = dbOrganization.OrgAddress,
            OrgType = dbOrganization.OrgType,
            OrgAttribute = dbOrganization.OrgAttribute
        };
    }
}
