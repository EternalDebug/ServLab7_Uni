﻿using System;
using System.Collections.Generic;

namespace APIuni.Models
{
    public partial class RankingCriterion
    {
        public long Id { get; set; }
        public long? RankingSystemId { get; set; }
        public string? CriteriaName { get; set; }

        public virtual RankingSystem? RankingSystem { get; set; }
    }
}
