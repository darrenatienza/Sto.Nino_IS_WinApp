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
    public class RoleRepo : Repository<Role>, IRoleRepo
    {
        public RoleRepo(DataContext context)
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






    }

   
}
