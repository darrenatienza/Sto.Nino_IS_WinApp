using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sto.NinoRMS.Queries.Core.Domain
{

    public class BrgyClearance
    {
        public BrgyClearance()
        {
            //Permissions = new HashSet<Permission>();
        }
        public int BrgyClearanceID { get; set; }
        public virtual Resident Resident { get; set; }
        public int ResidentID { get; set; }
        public string PurposeOfRequest { get; set; }
        public string CommTaxtCert { get; set; }
        public DateTime DateIssued { get; set; }
        public string PlaceIssued { get; set; }
        public string TinNo { get; set; }

    }
}
