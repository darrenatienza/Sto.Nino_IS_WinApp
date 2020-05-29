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
    public class BrgyBizClearanceRepo : Repository<BrgyBizClearance>, IBrgyBizClearanceRepo
    {
        public BrgyBizClearanceRepo(DataContext context)
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




        public IEnumerable<BrgyBizClearance> GetAllBy(string criteria)
        {
            return DataContext.BrgyBizClearances.Include(x => x.Resident).Where(x => x.Resident.FullName.Contains(criteria) || x.BizName.Contains(criteria));
        }


        public BrgyBizClearance GetBy(int bizClearanceID)
        {
            return DataContext.BrgyBizClearances.Include(x => x.Resident).FirstOrDefault(x => x.BrgyBizClearanceID == bizClearanceID);
        }
    }

   
}
