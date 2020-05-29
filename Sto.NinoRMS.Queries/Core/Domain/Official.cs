using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sto.NinoRMS.Queries.Core.Domain
{

    public class Official
    {
        public Official()
        {
            
        }
        public int OfficialID { get; set; }
        public virtual Resident Resident { get; set; }
        public int ResidentID { get; set; }
        public virtual OfficialPosition OfficialPosition { get; set; }
        public int OfficialPositionID { get; set; }
        public string Designation { get; set; }

    }
}
