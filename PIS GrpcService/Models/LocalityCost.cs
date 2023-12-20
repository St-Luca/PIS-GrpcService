using System.ComponentModel.DataAnnotations.Schema;

namespace PIS_GrpcService.Models;

public class LocalityCost
{
    public int IdContract { get; set; }
   // public MunicipalContract MunicipalContract { get; set; }
    public int IdLocality { get; set; } 

    [ForeignKey("IdLocality")]
    public Locality? Locality { get; set; }
    public int Cost { get; set; }
}
