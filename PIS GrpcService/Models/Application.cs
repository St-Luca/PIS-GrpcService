using System.ComponentModel.DataAnnotations.Schema;

namespace PIS_GrpcService.Models;

public class Application
{
    public int Id { get; set; }
    public DateTime? Date { get; set; }
    public string? ApplicantCategory { get; set; } = string.Empty;
    public string? AnimalDescription { get; set; } = string.Empty;
    public string? Urgency { get; set; } = string.Empty;
    public int IdLocality { get; set; }
    public int IdAct { get; set; }
    public int IdOrganization { get; set; }

    [ForeignKey("IdLocality")]
    public Locality Locality { get; set; } = null!;

    [ForeignKey("IdAct")]
    public CaptureAct Act { get; set; } = null!;

    [ForeignKey("IdOrganization")]
    public Organization Organization { get; set; } = null!;


    public bool IsInPeriodAndLocality(DateTime startDate, DateTime endDate, int localityName)
    {
        return Date >= startDate && Date <= endDate && Locality.GetId() == localityName;
    }
}
