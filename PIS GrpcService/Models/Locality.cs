using System;

namespace PIS_GrpcService.Models;

public class Locality
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public List<LocalityCost> LocalityCosts { get; set; }

    public string GetName()
    {
        return Name;
    }
}
