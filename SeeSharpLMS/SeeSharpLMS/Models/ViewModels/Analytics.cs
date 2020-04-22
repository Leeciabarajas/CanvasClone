using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeeSharpLMS.Models.ViewModels
{
    public class Analytics
    {
        public int GradeA { get; set; }
        public int GradeB { get; set; }
        public int GradeC { get; set; }
        public int GradeD { get; set; }
        public int GradeF { get; set; }

        public int TotalGrades { get { return GradeA + GradeB + GradeC + GradeD + GradeF; } }
    }
}
