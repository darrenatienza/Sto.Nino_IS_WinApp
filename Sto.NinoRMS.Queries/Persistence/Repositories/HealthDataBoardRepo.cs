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
    public class HealthDataBoardRepo : Repository<HealthDataBoard>, IHealthDataBoardRepo
    {
        public HealthDataBoardRepo(DataContext context)
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






        public IEnumerable<HealthDataBoard> GetAllBy(string criteria, int year)
        {
            return DataContext.HealthDataBoard.Include(x => x.User).Include(x => x.CommonHealthProfile).Where(x => (x.User.UserName.Contains(criteria) || x.CommonHealthProfile.Title.Contains(criteria)) && x.Year == year).ToList();
        }


        public IEnumerable<HealthDataBoard> GetAllBy(string criteria, int year, string currentUser)
        {
            return DataContext.HealthDataBoard.Include(x => x.User).Include(x => x.CommonHealthProfile).Where(x => (x.User.UserName == currentUser && x.CommonHealthProfile.Title.Contains(criteria)) && x.Year == year).ToList();
        }
    }

   
}
