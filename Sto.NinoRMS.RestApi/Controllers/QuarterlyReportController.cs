using Sto.NinoRMS.Queries.Core.Domain;
using Sto.NinoRMS.Queries.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi.Jwt.Models;

namespace WebApi.Jwt.Controllers
{
    public class QuarterlyReportController : ApiController
    {

        [AllowAnonymous]
        [HttpGet]
        [Route("api/quarterlyreports/{criteria?}/{year?}/{quarter?}")]
        public IHttpActionResult Get(string criteria = "", int year = 2020, int quarter =1)
        {
            try
            {
                using (var uow = new UnitOfWork(new DataContext()))
                {
                    criteria = criteria == "All" ? "" : criteria;
                    var objs = uow.QuarterlyReports.GetAllBy(criteria: criteria, year: year, quarter: quarter);
                    var models = new List<QuarterlyReportModel>();
                    foreach (var item in objs)
                    {
                        var model = new QuarterlyReportModel();
                        model.ID = item.QuarterlyReportID;
                        model.Quarter = item.Quarter;
                        model.Count = item.Count;
                        model.UserFullName = item.User.UserName;
                        model.AccomplishmentTitle = item.Accomplishment.Title;
                        model.Year = item.Year;
                        models.Add(model);
                    }
                    return Ok(models);
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        [AllowAnonymous]
        [HttpGet]
        [Route("api/quarterlyreports/{criteria}")]
        public IHttpActionResult Get(string criteria)
        {
            try
            {
                using (var uow = new UnitOfWork(new DataContext()))
                {
                    var objs = uow.QuarterlyReports.GetAllBy(criteria: criteria);
                    var models = new List<QuarterlyReportModel>();
                    foreach (var item in objs)
                    {
                        var model = new QuarterlyReportModel();
                        model.ID = item.AccomplishmentID;
                        model.Quarter = item.Quarter;
                        model.Count = item.Count;
                        model.UserFullName = item.User.UserName;
                        model.AccomplishmentTitle = item.Accomplishment.Title;
                        model.Year = item.Year;
                        models.Add(model);
                    }
                    return Ok(models);
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("api/quarterlyreport/{id}")]
        public IHttpActionResult Get(int id)
        {
            try
            {
                using (var uow = new UnitOfWork(new DataContext()))
                {
                    var obj = uow.QuarterlyReports.Get(id);
                    if (obj != null)
                    {
                        var model = new QuarterlyReportModel();
                        model.ID = obj.QuarterlyReportID;
                        model.AccomplishmentID = obj.AccomplishmentID;
                        model.Quarter = obj.Quarter;
                        model.Count = obj.Count;
                        model.Year = obj.Year;
                        model.Gender = obj.Gender;
                        return Ok(model);
                    }
                    return NotFound();
                   
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/quarterlyreport")]
        public IHttpActionResult Post(QuarterlyReportModel model)
        {
            try
            {
                using (var uow = new UnitOfWork(new DataContext()))
                {
                    var obj = new QuarterlyReport();
                    obj.Quarter = model.Quarter;
                    obj.Count = model.Count;
                    obj.UserID = model.UserID;
                    obj.AccomplishmentID = model.AccomplishmentID;
                    obj.Year = model.Year;
                    obj.Gender = model.Gender;
                    uow.QuarterlyReports.Add(obj);
                    uow.Complete();
                    return Ok();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        [AllowAnonymous]
        [HttpPut]
        [Route("api/quarterlyreport/{id}")]
        public IHttpActionResult Put(int id, QuarterlyReportModel model)
        {
            try
            {
                using (var uow = new UnitOfWork(new DataContext()))
                {
                    var obj = uow.QuarterlyReports.Get(id);
                    if (obj != null)
                    {
                        obj.Quarter = model.Quarter;
                        obj.Count = model.Count;
                        obj.UserID = model.UserID;
                        obj.AccomplishmentID = model.AccomplishmentID;
                        obj.Year = model.Year;
                        obj.Gender = model.Gender;
                        uow.QuarterlyReports.Edit(obj);
                        uow.Complete();
                        return Ok();
                        
                    }
                    return NotFound();
                    
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        [AllowAnonymous]
        [HttpDelete]
        [Route("api/quarterlyreport/{id}")]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                using (var uow = new UnitOfWork(new DataContext()))
                {
                    var obj = uow.QuarterlyReports.Get(id);
                    if (obj != null)
                    {
                        uow.QuarterlyReports.Remove(obj);
                        uow.Complete();
                        return Ok();
                    }
                    return NotFound();
                    
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
