using ProjectName.Queries.Core.Domain;
using ProjectName.Queries.Core.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectName.Queries.Persistence.Repositories
{
    public class PermissionRepository : Repository<Permission>, IPermissionRepository
    {
        public PermissionRepository(DataContext context)
            :base(context)
        {

        }
        public IEnumerable<Permission> GetPermissions()
        {
            return DataContext.Permissions.ToList();
        }
        public DataContext DataContext
        {
            get
            {
                return Context as DataContext;
            }
        }


        public IEnumerable<Permission> GetPermissionsByUserID(int userID)
        {
            return DataContext.Permissions
                .Where(u => u.Users.Any(p => p.UserID == userID));
        }


        public IEnumerable<Permission> GetAvailablePermission(IEnumerable<Permission> excludePermissions)
        {
            //StringBuilder excludedIDs = new StringBuilder();
            //foreach (Permission item in excludePermissions)
            //{
            //    excludedIDs.AppendFormat("{0},", item.PermissionID);
            //}
            //string[] excludeIDArray = excludedIDs.ToString().Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            var exPermissions = excludePermissions.Select(ex => ex.PermissionID).ToList();
            return DataContext.Permissions
               .Where(p => !exPermissions.Any(ex => ex == p.PermissionID));
        }


        public IEnumerable<Permission> GetAvailablePermission(string[] permissionIDs)
        {
            return DataContext.Permissions
               .Where(p => !permissionIDs.Any(p1 => p1 == p.PermissionID.ToString()));
        }


        public IEnumerable<Permission> GetValidPermissionsList(IEnumerable<Permission> permissionList)
        {
            List<Permission> newPermissions = new List<Permission>();
            permissionList.ToList().ForEach(p => newPermissions.Add(DataContext.Permissions.Find(p.PermissionID)));
            return newPermissions;
        }
    }
}
