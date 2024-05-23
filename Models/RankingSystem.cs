using System;
using System.Collections.Generic;

namespace APIuni.Models
{
    public partial class RankingSystem
    {
        public RankingSystem()
        {
            RankingCriteria = new HashSet<RankingCriterion>();
        }

        public long Id { get; set; }
        public string? SystemName { get; set; }

        public virtual ICollection<RankingCriterion> RankingCriteria { get; set; }
    }
}
