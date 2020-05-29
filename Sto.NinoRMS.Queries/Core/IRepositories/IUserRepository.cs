using Sto.NinoRMS.Queries.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sto.NinoRMS.Queries.Core.IRepositories
{
    public interface IUserRepository : IRepository<User>
    {
        //IEnumerable<User> GetUsers();

        //User GetUser(string userName);

        //User GetUser(int userID);
        //void RemoveUsersPermissions(int userID);
        User GetUser(string userName);

        IEnumerable<User> GetAllBy(string criteria);

        User GetBy(int userID);
    }
}
