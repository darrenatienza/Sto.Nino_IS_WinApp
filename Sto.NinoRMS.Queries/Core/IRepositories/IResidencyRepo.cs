using Sto.NinoRMS.Queries.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sto.NinoRMS.Queries.Core.IRepositories
{
    public interface IResidencyRepo : IRepository<Residency>
    {

        IEnumerable<Residency> GetAllBy(string criteria);

        Residency GetBy(int residencyID);
    }
}
