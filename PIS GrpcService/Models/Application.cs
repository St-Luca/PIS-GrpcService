namespace PIS_GrpcService.Models;

public class Application
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public string ApplicantCategory { get; set; } = string.Empty;
    public string AnimalDescription { get; set; } = string.Empty;
    public string Urgency { get; set; } = string.Empty;
    public string Locality { get; set; } = string.Empty;
    public string Organization { get; set; } = string.Empty;
}
