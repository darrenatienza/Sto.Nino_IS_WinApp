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
    public class IndigencyRepo : Repository<Indigency>, IIndigencyRepo
    {
        public IndigencyRepo(DataContext context)
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


        public IEnumerable<Indigency> GetAllBy(string criteria)
        {
            return DataContext.Indigencies.Include(x => x.Resident).Where(x => x.Resident.FullName.Contains(criteria)).ToList();
        }


        public Indigency GetBy(int indigencyID)
        {
            return DataContext.Indigencies.Include(x => x.Resident).FirstOrDefault(x => x.IndigencyID== indigencyID);
        }


        
    }

   
}
