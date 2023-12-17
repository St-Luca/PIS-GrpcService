namespace PIS_GrpcService.Models
{
    public class LocalityCost
    {
        public int Id_Contract { get; set; }
        public int Id_Locality { get; set; }
        public string Cost { get; set; } = string.Empty;
    }
}
