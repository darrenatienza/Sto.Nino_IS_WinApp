using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sto.NinoRMS.Queries.Core.Domain
{
    public class Education
    {
        public Education() { }
        public int EducationID { get; set; }
        public string SchoolName { get; set; }

        public string Level { get; set; }
        public string InclusiveDate { get; set; }
        public string Course { get; set; }
        public virtual Resident Resident { get; set; }
        public int ResidentID { get; set; }
    }
}
