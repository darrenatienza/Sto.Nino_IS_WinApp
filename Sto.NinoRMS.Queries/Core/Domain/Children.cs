using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sto.NinoRMS.Queries.Core.Domain
{
    public class Children
    {
        public Children() { }
        public int ChildrenID { get; set; }
        public string FullName { get; set; }
        public int Age { get; set; }
        public string Occupation { get; set; }
        public virtual Resident Resident { get; set; }
        public int ResidentID { get; set; }
    }
}
