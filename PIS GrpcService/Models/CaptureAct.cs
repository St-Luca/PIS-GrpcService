using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace PIS_GrpcService.Models;

public class CaptureAct
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public int IdContract { get; set; }

    [ForeignKey("IdContract")]
    public Contract Contract { get; set; }
    public int Amount { get; set; }
    public int IdOrganization { get; set; }

    [ForeignKey("IdOrganization")]
    public Organization Performer { get; set; }
    public int IdLocality { get; set; }

    [ForeignKey("IdLocality")]
    public Locality Locality { get; set; }
    public List<Application> Applications { get; set; }
    public List<Animal> Animals { get; set; }

    public bool IsInPeriodAndLocality(DateTime startDate, DateTime endDate, string localityName)
    {
        return ((Date >= startDate) && (Date <= endDate) && (Locality.GetName() == localityName));
    }

    public bool IsInOrganization(string orgName)
    {
        return Performer.GetName() == orgName;
    }

    public LocalityCost GetCostClosedApp(int localityId)
    {
        return Contract.GetCostContract(localityId);
    }
}