using System;

namespace PIS_GrpcService.Models
{
    public class CaptureAct
    {
        public int Id { get; set; }
        public DateTime ActDate { get; set; }
        public Animal CapturedAnimal { get; set; } 
        public int Amount { get; set; }
        public Organization Performer { get; set; } 
        public Organization Customer { get; set; } 
        public List<Locality> Localities { get; set; } 
    }
}