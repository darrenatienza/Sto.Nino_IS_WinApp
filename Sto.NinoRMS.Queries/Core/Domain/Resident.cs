using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sto.NinoRMS.Queries.Core.Domain
{

    public class Resident
    {
        public Resident()
        {
            Educations = new HashSet<Education>();
            Childrens = new HashSet<Children>();
        }
        public int ResidentID { get; set; }
        public string FullName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthday { get; set; }
        public string Gender { get; set; }
        public string Religion { get; set; }
        public string FathersFullName { get; set; }
        public string FathersOccupation { get; set; }
        public string MothersFullName { get; set; }
        public string MothersOccupation { get; set; }
        public string CivilStatus { get; set; }
        public string Citizenship { get; set; }
        public string Purok { get; set; }
        public string Sitio { get; set; }
        public string ContactNumber { get; set; }
        public string Occupation { get; set; }
        public string ImageName { get; set; }


        public string SpouseFullName { get; set; }

        public string SpouseOccupation { get; set; }

        public ICollection<Education> Educations { get; set; }
        public ICollection<Children> Childrens { get; set; }
    }
}
