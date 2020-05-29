using OERS.Queries.Core.Domain;
using OERS.Queries.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OERS.Queries.Core.IRepositories
{
    public interface IReservationLogRepository: IRepository<ReservationLog>
    {
        DataContext DataContext { get; }

        IEnumerable<ReservationLog> GetReservationLogs(DateTime date, string employeeName);
    }
}
