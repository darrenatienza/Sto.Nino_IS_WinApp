using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OERS.Queries.Core.Domain
{
    public class ReservationLog
    {
        public ReservationLog()
        {

        }
        public int ReservationLogID { get; set; }
        public string EmployeeName { get; set; }
        public string ItemDescription { get; set; }
        public DateTime DateTimeLog { get; set; }
        public string Room { get; set; }
        public string ReservationStatus { get; set; }

    }
}
