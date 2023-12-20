namespace PIS_GrpcService.Models;

public class LocalityCost
{
    public int IdCost { get; set; }
    public string IdContract { get; set; } = string.Empty;
    public string IdLocality { get; set; } = string.Empty;
    public string Cost { get; set; } = string.Empty;
}
