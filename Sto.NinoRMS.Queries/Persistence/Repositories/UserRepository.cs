using Sto.NinoRMS.Queries.Core.Domain;
using Sto.NinoRMS.Queries.Core.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Sto.NinoRMS.Queries.Persistence.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(DataContext context)
            : base(context)
        {
        }
        //public IEnumerable<User> GetUsers()
        //{
        //    return DataContext.Users.ToList();
        //}
        public DataContext DataContext
        {
            get
            {
                return Context as DataContext;
            }
        }


        //public User GetUser(string userName)
        //{

        //    return DataContext.Users.Include(u => u.Permissions).FirstOrDefault(user => user.UserName == userName);
        //}


        //public void RemoveUsersPermissions(int userID)
        //{

        //    var a = DataContext.Users.Include(i => i.Permissions).Where(u => u.UserID == userID).FirstOrDefault<User>();
        //    var p = a.Permissions.ToList();
        //    p.ForEach(pe => a.Permissions.Remove(pe));
        //}


        //public User GetUser(int userID)
        //{
        //    return DataContext.Users.Include(i => i.Permissions).Where(u => u.UserID == userID).FirstOrDefault<User>();
        //}

        public User GetUser(string userName)
        {
            return DataContext.Users.Include(x => x.Role).FirstOrDefault(x => x.UserName == userName);
        }


        public IEnumerable<User> GetAllBy(string criteria)
        {
            return DataContext.Users.Where(x => x.UserName.Contains(criteria) && x.FirstName.Contains(criteria)
                && x.MiddleName.Contains(criteria)
                && x.LastName.Contains(criteria)).ToList();
        }


        public User GetBy(int userID)
        {
            return DataContext.Users.Include(x => x.Role).FirstOrDefault(x => x.UserID == userID);
        }
    }

   
}
