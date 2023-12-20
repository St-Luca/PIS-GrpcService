using System;

namespace PIS_GrpcService.Models
{
    public class Contract
    {
        public int Id { get; set; }
        public DateTime ConcDate { get; set; }
        public DateTime EffDate { get; set; }
        public int Amount { get; set; }
        public string Performer { get; set; } = string.Empty;
        public string Customer { get; set; } = string.Empty;
        public string Localities { get; set; } = string.Empty;
    }
}
