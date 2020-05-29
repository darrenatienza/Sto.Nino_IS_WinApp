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
    public class QuarterlyReportRepo : Repository<QuarterlyReport>, IQuarterlyReportRepo
    {
        public QuarterlyReportRepo(DataContext context)
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






        public IEnumerable<QuarterlyReport> GetAllBy(string criteria)
        {
            return DataContext.QuarterlyReports.Include(x => x.User).Where(x => x.User.FirstName.Contains(criteria) && x.User.LastName.Contains(criteria) && x.Accomplishment.Title.Contains(criteria));
        }


        public IEnumerable<QuarterlyReport> GetAllBy(string criteria, int year, int quarter)
        {
            return DataContext.QuarterlyReports.Include(x => x.User).Include(x => x.Accomplishment).Where(x => (x.User.UserName.Contains(criteria) || x.Accomplishment.Title.Contains(criteria)) && x.Year == year && x.Quarter == quarter).ToList();
        }
    }

   
}
