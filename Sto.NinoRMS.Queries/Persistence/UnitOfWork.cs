using Sto.NinoRMS.Queries.Core;
using Sto.NinoRMS.Queries.Core.IRepositories;
using Sto.NinoRMS.Queries.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sto.NinoRMS.Queries.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;

        public UnitOfWork(DataContext context)
        {
            _context = context;
            Users = new UserRepository(_context);
            BrgyBizClearances = new BrgyBizClearanceRepo(_context);
            BrgyClearances = new BrgyClearanceRepo(_context);
            //Permissions = new PermissionRepository(_context);
            Indigencies = new IndigencyRepo(_context);
            Officials = new OfficialRepo(_context);
            OfficialPositions = new OfficialPositionRepo(_context);
            Residencies = new ResidencyRepo(_context);
            Residents = new ResidentRepo(_context);
            Accomplishments = new AccomplishmentRepo(_context);
            CommonHealthProfiles = new CommonHealthProfileRepo(_context);
            HealthDataBoard = new HealthDataBoardRepo(_context);
            QuarterlyReports = new QuarterlyReportRepo(_context);
            Roles = new RoleRepo(_context);
            Childrens = new ChildrenRepo(_context);
            Educations = new EducationRepo(_context);
        }

        public IUserRepository Users { get; private set; }
        //public IPermissionRepository Permissions { get; private set; }
        
        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }







        public IBrgyBizClearanceRepo BrgyBizClearances
        {
            get;
            private set;
        }

        public IBrgyClearanceRepo BrgyClearances
        {
            get;
            private set;
        }

        public IIndigencyRepo Indigencies
        {
            get;
            private set;
        }

        public IOfficialRepo Officials
        {
            get;
            private set;
        }

        public IOfficialPositionRepo OfficialPositions
        {
            get;
            private set;
        }

        public IResidencyRepo Residencies
        {
            get;
            private set;
        }

        public IResidentRepo Residents
        {
            get;
            private set;
        }


        public IAccomplishmentRepo Accomplishments
        {
            get;
            private set;
        }

        public ICommonHealthProfileRepo CommonHealthProfiles
        {
            get;
            private set;
        }

        public IHealthDataBoardRepo HealthDataBoard
        {
            get;
            private set;
        }

        public IQuarterlyReportRepo QuarterlyReports
        {
            get;
            private set;
        }


        public IRoleRepo Roles
        {
            get;
            private set;
        }


        public IChildrenRepo Childrens
        {
            get;
            private set;
        }

        public IEducationRepo Educations
        {
            get;
            private set;
        }
    }
}
