using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace PIS_GrpcService.Models
{
    public class CaptureAct
    {
        public int Id { get; set; }
        public DateTime ActDate { get; set; }
        public int IdCapturedAnimal { get; set; }

        [ForeignKey("IdCapturedAnimal")]
        public Animal CapturedAnimal { get; set; }

        public int IdContract { get; set; }

        [ForeignKey("IdContract")]
        public Contract Contract { get; set; }
        public int Amount { get; set; }
        public int IdOrganization { get; set; }

        [ForeignKey("IdOrganization")]
        public Organization Performer { get; set; } 
        public Locality Locality { get; set; }
        public List<Application> Applications { get; set; }
        public List<Animal> Animals { get; set; }
    }
}