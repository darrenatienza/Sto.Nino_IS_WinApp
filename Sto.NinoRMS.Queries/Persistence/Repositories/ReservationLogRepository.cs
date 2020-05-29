using OERS.Queries.Core.Domain;
using OERS.Queries.Core.IRepositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OERS.Queries.Persistence.Repositories
{
    public class ReservationLogRepository : Repository<ReservationLog>, IReservationLogRepository
    {
        public ReservationLogRepository(DataContext context)
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


        public IEnumerable<ReservationLog> GetReservationLogs(DateTime date, string employeeName)
        {
            return DataContext.ReservationLogs.Where(rl => DbFunctions.TruncateTime(rl.DateTimeLog) == DbFunctions.TruncateTime(date) && rl.EmployeeName.Contains(employeeName));
        }
    }
}
