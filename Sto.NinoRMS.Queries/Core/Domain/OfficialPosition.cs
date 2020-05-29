using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sto.NinoRMS.Queries.Core.Domain
{

    public class OfficialPosition
    {
        public OfficialPosition()
        {
            
        }
        public int OfficialPositionID { get; set; }
        public string Title { get; set; }

    }
}
