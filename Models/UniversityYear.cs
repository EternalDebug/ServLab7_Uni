using System;
using System.Collections.Generic;

namespace APIuni.Models
{
    public partial class UniversityYear
    {
        public long? UniversityId { get; set; }
        public long? Year { get; set; }
        public long? NumStudents { get; set; }
        public double? StudentStaffRatio { get; set; }
        public long? PctInternationalStudents { get; set; }
        public long? PctFemaleStudents { get; set; }

        public virtual University? University { get; set; }
    }
}
