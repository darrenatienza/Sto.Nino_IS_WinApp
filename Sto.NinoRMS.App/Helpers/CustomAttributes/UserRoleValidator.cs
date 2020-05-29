using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sto.NinoRMS.App.Helpers.CustomAttributes
{
    [AttributeUsage(AttributeTargets.All)]
    class UserRoleValidator : Attribute
    {
        // Provides name of the member 
        private string name;

        // Provides description of the member 
        private string action;

        // Constructor 
        public UserRoleValidator(string name, string action)
        {
            this.name = name;
            this.action = action;
            Console.WriteLine(this.name);
           


        }
        
       
        // property to get name 
        public string Name
        {
            get { return name; }
        }

        // property to get description 
        public string Action
        {
            get { return action; }
        }
    }
}
