using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi.Jwt.Models
{
    public class HealthDataBoardModel
    {

        public int ID { get; set; }
        public string  UserName { get; internal set; }
        public int UserID { get; set; }
        public int Year { get; set; }
        public int Count { get; set; }

        public int CommonHealthProfileID { get; set; }
        public string CommonHealthProfileTitle { get; set; }

    }

    public class HealthDataBoardModel1
    {

        

    }
}