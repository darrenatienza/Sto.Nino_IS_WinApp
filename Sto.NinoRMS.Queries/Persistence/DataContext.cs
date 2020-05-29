using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Sto.NinoRMS.Queries.Core.Domain;
using Sto.NinoRMS.Queries.Migrations;
using Sto.NinoRMS.Queries.EntityConfiguration;
namespace Sto.NinoRMS.Queries.Persistence
{
    public class DataContext : DbContext
    {
        public DataContext()
            : base("DataContext")
            //: base("ProductionContext") /** C:\...**/
            //: base("ServerContext") /** laptop to use in defense **/
            //: base("ServerContext2") /** migration using team viewer only **/
        {
            this.Configuration.LazyLoadingEnabled = false;
        }

        public virtual DbSet<User> Users { get; set; }
        //public virtual DbSet<Permission> Permissions { get; set; }

        public virtual DbSet<BrgyBizClearance> BrgyBizClearances { get; set; }
        public virtual DbSet<BrgyClearance> BrgyClearances { get; set; }
        public virtual DbSet<Indigency> Indigencies { get; set; }
        public virtual DbSet<Official> Officials { get; set; }
        public virtual DbSet<OfficialPosition> OfficialPositions { get; set; }
        public virtual DbSet<Residency> Residencies { get; set; }
        public virtual DbSet<Accomplishment> Accomplishments { get; set; }
        public virtual DbSet<CommonHealthProfile> CommonHealthProfiles { get; set; }
        public virtual DbSet<HealthDataBoard> HealthDataBoard { get; set; }
        public virtual DbSet<QuarterlyReport> QuarterlyReports { get; set; }
        public virtual DbSet<Resident> Residents { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Children> Childrens { get; set; }
        public virtual DbSet<Education> Educations { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new UserConfiguration());
            
        }
    }
}
