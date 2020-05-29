using Sto.NinoRMS.Queries.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sto.NinoRMS.Queries.Core.IRepositories
{
    public interface IHealthDataBoardRepo : IRepository<HealthDataBoard>
    {


        IEnumerable<HealthDataBoard> GetAllBy(string criteria, int year);



        IEnumerable<HealthDataBoard> GetAllBy(string criteria, int year, string currentUser);
    }
}
