using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sto.NinoRMS.Queries.Core.Domain
{

    public class BrgyBizClearance
    {
        public BrgyBizClearance()
        {
            
        }
        public int BrgyBizClearanceID { get; set; }
        public virtual Resident Resident { get; set; }
        public int ResidentID { get; set; }
        public string BizName { get; set; }
        public bool isFollowingAllProvision { get; set; }
        public bool isHinderPermitApplication { get; set; }

    }
}
