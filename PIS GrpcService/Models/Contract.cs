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


        public LocalityCost GetCostContract(Locality locality)
        {
            var costInCity = GetCostByLocality(LocalityCosts, locality);
            return costInCity;
        }

        public LocalityCost? GetCostByLocality(List<LocalityCost> localityCosts, Locality locality)
        {
            return localityCosts.FirstOrDefault(lc => lc.Locality == locality);
        }
    }
}