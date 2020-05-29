using ProjectName.Queries.Core.Domain;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectName.Queries.Core.IRepositories
{
    public interface IPermissionRepository : IRepository<Permission>
    {
        IEnumerable<Permission> GetPermissions();
        /// <summary>
        /// Get Users Permission as Many to Many relationship
        /// </summary>
        /// <param name="userID">ID of the User</param>
        /// <returns></returns>
        IEnumerable<Permission> GetPermissionsByUserID(int userID);
        IEnumerable<Permission> GetAvailablePermission(IEnumerable<Permission> permissions);

        IEnumerable<Permission> GetAvailablePermission(string[] permissionIDs);

        IEnumerable<Permission> GetValidPermissionsList(IEnumerable<Permission> permissionList);
    }
}
