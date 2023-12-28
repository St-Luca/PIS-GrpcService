namespace PIS_GrpcService.Models;

public class Organization
{
    public int Id { get; set; }
    public string OrgName { get; set; } = string.Empty;
    public string INN { get; set; } = string.Empty;
    public string KPP { get; set; } = string.Empty;
    public string OrgAddress { get; set; } = string.Empty;
    public string OrgType { get; set; } = string.Empty;
    public string OrgAttribute { get; set; } = string.Empty;
    public List<Application> Applications { get; set; }
    public List<Contract> Contracts { get; set; }
    public List<CaptureAct> Acts { get; set; }

    public string GetName()
    {
        return OrgName;
    }
}
