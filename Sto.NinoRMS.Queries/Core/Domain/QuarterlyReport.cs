using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sto.NinoRMS.Queries.Core.Domain
{
    public class QuarterlyReport
    {
        public QuarterlyReport() { }
        public int QuarterlyReportID { get; set; }
        public virtual Accomplishment Accomplishment { get; set; }
        public int AccomplishmentID { get; set; }
        public int Quarter { get; set; }
        public int Year { get; set; }
        public virtual User User { get; set; }
        public int UserID { get; set; }
        public string Gender { get; set; }

        public int Count { get; set; }
    }
}
