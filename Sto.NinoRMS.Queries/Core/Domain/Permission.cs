using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectName.Queries.Core.Domain
{
    public class Permission
    {
        public Permission()
        {
            Users = new HashSet<User>();
        }
        public int PermissionID { get; set; }
        public string Description { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
