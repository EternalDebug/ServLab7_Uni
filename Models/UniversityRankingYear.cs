using System;
using System.Collections.Generic;

namespace ServLab7.Models
{
    public partial class UniversityRankingYear
    {
        public long? UniversityId { get; set; }
        public long? RankingCriteriaId { get; set; }
        public long? Year { get; set; }
        public long? Score { get; set; }

        public virtual RankingCriterion? RankingCriteria { get; set; }
        public virtual University? University { get; set; }
    }
}
