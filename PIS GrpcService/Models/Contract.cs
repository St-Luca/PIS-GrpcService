using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace PIS_GrpcService.Models
{
    public class Contract
    {
        public int Id { get; set; }
        public DateTime ConclusionDate { get; set; }
        public DateTime EffectiveDate { get; set; }
        public int Amount { get; set; }
        public int IdOrganization { get; set; }

        [ForeignKey("IdOrganization")]
        public Organization Performer { get; set; } 
        public List<LocalityCost> LocalityCosts { get; set; }


        public LocalityCost GetCostContract(int localityId)
        {
            var costInCity = GetCostByLocality(LocalityCosts, localityId);
            return costInCity;
        }

        public LocalityCost GetCostByLocality(List<LocalityCost> localityCosts, int localityId)
        {
            return localityCosts.First(lc => lc.IdLocality == localityId);
        }
    }
}