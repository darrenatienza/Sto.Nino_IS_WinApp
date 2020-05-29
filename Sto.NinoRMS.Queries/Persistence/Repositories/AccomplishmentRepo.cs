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
    public class AccomplishmentRepo : Repository<Accomplishment>, IAccomplishmentRepo
    {
        public AccomplishmentRepo(DataContext context)
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






        public IEnumerable<Accomplishment> GetAllBy(string criteria)
        {
            return DataContext.Accomplishments.Where(x => x.Title.Contains(criteria));
        }
    }

   
}
