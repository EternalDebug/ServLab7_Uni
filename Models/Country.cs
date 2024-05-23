using System;
using System.Collections.Generic;

namespace APIuni.Models
{
    public partial class Country
    {
        public Country()
        {
            Universities = new HashSet<University>();
        }

        public long Id { get; set; }
        public string? CountryName { get; set; }

        public virtual ICollection<University> Universities { get; set; }
    }
}
