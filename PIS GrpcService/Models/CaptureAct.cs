using System;

namespace PIS_GrpcService.Models
{
    public class CaptureAct
    {
        public int Id { get; set; }
        public DateTime ActDate { get; set; }
        public string CapturedAnimal { get; set; } = string.Empty;
        public int Amount { get; set; }
        public string Performer { get; set; } = string.Empty;
        public string Customer { get; set; } = string.Empty;
        public string Localities { get; set; } = string.Empty;
    }
}