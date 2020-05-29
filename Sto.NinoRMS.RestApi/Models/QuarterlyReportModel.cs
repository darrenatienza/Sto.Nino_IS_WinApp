using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi.Jwt.Models
{
    public class QuarterlyReportModel
    {
        public string AccomplishmentTitle { get; internal set; }
        public int AccomplishmentID { get;  set; }
        public int Quarter { get; set; }
        public int Year { get; set; }
        public string UserFullName { get; internal set; }
        public int UserID { get; set; }

        public int ID { get; set; }

        public int Count { get; set; }

        public string Gender { get; set; }
    }
}