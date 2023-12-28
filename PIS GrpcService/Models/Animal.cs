using System.ComponentModel.DataAnnotations.Schema;

namespace PIS_GrpcService.Models;

public class Animal
{
    public int Id { get; set; }
    public string Category { get; set; } = string.Empty;
    public string Sex { get; set; } = string.Empty;
    public string Breed { get; set; } = string.Empty;
    public string Size { get; set; } = string.Empty;
    public string Coat { get; set; } = string.Empty;
    public string Color { get; set; } = string.Empty;
    public string Ears { get; set; } = string.Empty;
    public string Tail { get; set; } = string.Empty;
    public int IdCaptureAct { get; set; }

    [ForeignKey("IdCaptureAct")]
    public CaptureAct Act { get; set; } = null!;
    public string Mark { get; set; } = string.Empty;
    public string IdentChip { get; set; } = string.Empty;
}
