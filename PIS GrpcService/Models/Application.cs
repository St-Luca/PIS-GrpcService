﻿using System.ComponentModel.DataAnnotations.Schema;

namespace PIS_GrpcService.Models;

public class Application
{
    public int Id { get; set; }
    public DateTime? Date { get; set; }
    public string? ApplicantCategory { get; set; } = string.Empty;
    public string? AnimalDescription { get; set; } = string.Empty;
    public string? Urgency { get; set; } = string.Empty;
    public int IdLocality { get; set; }
    public int IdOrganization { get; set; }

    [ForeignKey("IdLocality")]
    public Locality Locality { get; set; } = null!;

    [ForeignKey("IdOrganization")]
    public Organization Organization { get; set; } = null!;
}
