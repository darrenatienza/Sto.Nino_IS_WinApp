using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sto.NinoRMS.Queries.Core.Domain
{

    public class Residency
    {
        public Residency()
        {
            
        }
        public int ResidencyID { get; set; }
        public virtual Resident Resident { get; set; }
        public int ResidentID { get; set; }
        public string LengthOfResidency { get; set; }
    }
}
