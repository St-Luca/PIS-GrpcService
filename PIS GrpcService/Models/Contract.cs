using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace PIS_GrpcService.Models
{
    public class Contract
    {
        public int Id { get; set; }
        public DateTime ConcDate { get; set; }
        public DateTime EffDate { get; set; }
        public int Amount { get; set; }
        public int IdOrganization { get; set; }

        [ForeignKey("IdOrganization")]
        public Organization Performer { get; set; } 

        [ForeignKey("IdLocality")]
        public List<Locality> Localities { get; set; } 
    }
}