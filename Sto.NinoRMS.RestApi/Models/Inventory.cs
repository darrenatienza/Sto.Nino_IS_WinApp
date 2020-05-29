using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi.Jwt.Models
{
    public class Inventory
    {
        public Inventory()
        {

        }
        public int InventoryID { get; set; }
        public string Description { get; set; }
        public string PropertyNumber { get; set; }
    }
}