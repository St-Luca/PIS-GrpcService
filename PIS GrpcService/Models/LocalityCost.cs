namespace PIS_GrpcService.Models;

public class LocalityCost
{
    public int Id_Cost { get; set; }
    public string Id_Contract { get; set; } = string.Empty;
    public string Id_Locality { get; set; } = string.Empty;
    public string Cost { get; set; } = string.Empty;
}
