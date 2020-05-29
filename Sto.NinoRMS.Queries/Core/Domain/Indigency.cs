using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sto.NinoRMS.Queries.Core.Domain
{

    public class Indigency
    {
        public Indigency()
        {
            //Permissions = new HashSet<Permission>();
        }
        public int IndigencyID { get; set; }
        public virtual Resident Resident { get; set; }
        public int ResidentID { get; set; }
    }
}
