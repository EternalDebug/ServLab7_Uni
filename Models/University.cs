using System;
using System.Collections.Generic;

namespace APIuni.Models
{
    public partial class University
    {
        public long Id { get; set; }
        public long? CountryId { get; set; }
        public string? UniversityName { get; set; }

        public virtual Country? Country { get; set; }
    }
}
