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
    public class ResidencyRepo : Repository<Residency>, IResidencyRepo
    {
        public ResidencyRepo(DataContext context)
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


       
        public IEnumerable<Residency> GetAllBy(string criteria)
        {
            return DataContext.Residencies.Include(x => x.Resident).Where(x => x.Resident.FullName.Contains(criteria)).ToList();
        }


        public Residency GetBy(int residencyID)
        {
            return DataContext.Residencies.Include(x => x.Resident).FirstOrDefault(x => x.ResidencyID == residencyID);
        }
    }

   
}
