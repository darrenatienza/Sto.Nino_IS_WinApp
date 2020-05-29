using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sto.NinoRMS.Queries.Core.Domain
{
    public class HealthDataBoard
    {
        public HealthDataBoard() { }
        public int HealthDataBoardID { get; set; }
        public virtual CommonHealthProfile CommonHealthProfile { get; set; }
        public int CommonHealthProfileID { get; set; }
        public virtual User User { get; set; }
        public int UserID { get; set; }
        public int Year { get; set; }
        public int Count { get; set; }
    }
}
