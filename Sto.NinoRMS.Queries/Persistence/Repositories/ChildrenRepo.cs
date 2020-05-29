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
    public class ChildrenRepo : Repository<Children>, IChildrenRepo
    {
        public ChildrenRepo(DataContext context)
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






        public IEnumerable<Children> GetAll(int residentID)
        {
            return DataContext.Childrens.Where(x => x.ResidentID == residentID).ToList();
        }
    }

   
}
