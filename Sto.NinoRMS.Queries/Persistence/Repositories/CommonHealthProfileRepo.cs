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
    public class CommonHealthProfileRepo : Repository<CommonHealthProfile>, ICommonHealthProfileRepo
    {
        public CommonHealthProfileRepo(DataContext context)
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






        public CommonHealthProfile Get(string title)
        {
            return DataContext.CommonHealthProfiles.FirstOrDefault(x => x.Title == title);
        }


        public IEnumerable<CommonHealthProfile> GetAllBy(string title)
        {
            return DataContext.CommonHealthProfiles.Where(x => x.Title.Contains(title));
        }
    }

   
}
