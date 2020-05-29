﻿using Sto.NinoRMS.Queries.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sto.NinoRMS.Queries.Core.IRepositories
{
    public interface IQuarterlyReportRepo : IRepository<QuarterlyReport>
    {


        IEnumerable<QuarterlyReport> GetAllBy(string criteria);

        IEnumerable<QuarterlyReport> GetAllBy(string criteria, int year, int quarter);
    }
}
