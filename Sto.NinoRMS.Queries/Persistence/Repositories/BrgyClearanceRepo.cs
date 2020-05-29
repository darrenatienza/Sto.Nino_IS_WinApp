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
    public class BrgyClearanceRepo : Repository<BrgyClearance>, IBrgyClearanceRepo
    {
        public BrgyClearanceRepo(DataContext context)
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


       

        public IEnumerable<BrgyClearance> GetAllBy(string criteria)
        {
            return DataContext.BrgyClearances.Include(x => x.Resident).Where(x => x.Resident.FullName.Contains(criteria)).ToList();
        }


        public int GetBy()
        {
            throw new NotImplementedException();
        }

        public BrgyClearance GetBy(int brgyClearanceID)
        {
            return DataContext.BrgyClearances.Include(x => x.Resident).FirstOrDefault(x => x.BrgyClearanceID == brgyClearanceID);
        }
    }

   
}
