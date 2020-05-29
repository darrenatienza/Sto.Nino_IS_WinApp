using Sto.NinoRMS.Queries.Core.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sto.NinoRMS.Queries.Core
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository Users { get;}
        //IPermissionRepository Permissions { get;}
        IBrgyBizClearanceRepo BrgyBizClearances { get; }
        IBrgyClearanceRepo BrgyClearances { get; }
        IIndigencyRepo Indigencies { get; }
        IOfficialRepo Officials { get; }
        IOfficialPositionRepo OfficialPositions { get; }
        IResidencyRepo Residencies { get; }
        IAccomplishmentRepo Accomplishments { get; }
        ICommonHealthProfileRepo CommonHealthProfiles { get; }
        IHealthDataBoardRepo HealthDataBoard { get; }
        IQuarterlyReportRepo QuarterlyReports { get; }
        IRoleRepo Roles { get; }
        IChildrenRepo Childrens { get; }
        IEducationRepo Educations { get; }
        int Complete();
    }
}
