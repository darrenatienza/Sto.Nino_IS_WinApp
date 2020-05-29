using Sto.NinoRMS.Queries.Core.Domain;
using Sto.NinoRMS.Queries.Core.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Sto.NinoRMS.Queries.Persistence.Repositories
{
    public class EducationRepo : Repository<Education>, IEducationRepo
    {
        public EducationRepo(DataContext context)
            : base(context)
        {
        }
       
        public DataContext DataContext
        {
            get
            {
                return Context as DataContext;
            }
        }







        public IEnumerable<Education> GetAll(int residentID)
        {
            return DataContext.Educations.Where(x => x.ResidentID == residentID);
        }
    }

   
}
