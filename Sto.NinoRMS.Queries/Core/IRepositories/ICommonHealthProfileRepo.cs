using Sto.NinoRMS.Queries.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sto.NinoRMS.Queries.Core.IRepositories
{
    public interface ICommonHealthProfileRepo : IRepository<CommonHealthProfile>
    {


        CommonHealthProfile Get(string title);

        IEnumerable<CommonHealthProfile> GetAllBy(string title);
    }
}
