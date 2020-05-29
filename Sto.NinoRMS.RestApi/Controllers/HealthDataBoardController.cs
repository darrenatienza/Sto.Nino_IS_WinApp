using Sto.NinoRMS.Queries.Core.Domain;
using Sto.NinoRMS.Queries.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi.Jwt.Filters;
using WebApi.Jwt.Models;

namespace WebApi.Jwt.Controllers
{
    public class HealthDataBoardController : ApiController
    {
        [AllowAnonymous]
        [HttpGet]
        [Route("api/healthdataboards/{criteria?}/{year?}")]
        public IHttpActionResult Get(string criteria  = "", int year = 2020)
        {
            try
            {
                using (var uow = new UnitOfWork(new DataContext()))
                {
                    criteria = criteria == "All" ? "" : criteria;
                    var currentUser = RequestContext.Principal.Identity.Name;
                    var objs = uow.HealthDataBoard.GetAllBy(criteria: criteria, year: year);
                   
                    var models = new List<HealthDataBoardModel>();
                    foreach (var item in objs)
                    {
                        var model = new HealthDataBoardModel();
                        model.ID = item.HealthDataBoardID;
                        model.Count = item.Count;
                        model.UserName = item.User.UserName;
                        model.Year = item.Year;
                        model.CommonHealthProfileTitle = item.CommonHealthProfile.Title;
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

        [JwtAuthentication]
        [HttpGet]
        [Route("api/v2/healthdataboards/{criteria?}/{year?}")]
        public IHttpActionResult GetV2(string criteria = "", int year = 2020)
        {
            try
            {
                using (var uow = new UnitOfWork(new DataContext()))
                {
                    criteria = criteria == "All" ? "" : criteria;
                    var currentUser = RequestContext.Principal.Identity.Name;
                    var objs = uow.HealthDataBoard.GetAllBy(criteria: criteria, year: year, currentUser: currentUser);

                    var models = new List<HealthDataBoardModel>();
                    foreach (var item in objs)
                    {
                        var model = new HealthDataBoardModel();
                        model.ID = item.HealthDataBoardID;
                        model.Count = item.Count;
                        model.UserName = item.User.UserName;
                        model.Year = item.Year;
                        model.CommonHealthProfileTitle = item.CommonHealthProfile.Title;
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
        [Route("api/healthdataboard/{id}")]
        public IHttpActionResult Get(int id)
        {
            try
            {
                using (var uow = new UnitOfWork(new DataContext()))
                {
                    var obj = uow.HealthDataBoard.Get(id);
                   
                    if (obj != null)
                    {
                        var model = new HealthDataBoardModel();
                        model.ID = obj.HealthDataBoardID;
                        model.Count = obj.Count;
                        model.Year = obj.Year;
                        model.CommonHealthProfileID = obj.CommonHealthProfileID;
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
        [Route("api/healthdataboard")]
        public IHttpActionResult Post(HealthDataBoardModel model)
        {
            try
            {
                using (var uow = new UnitOfWork(new DataContext()))
                {
                    var obj = new HealthDataBoard();
                    obj.Count = model.Count;
                    obj.CommonHealthProfileID = model.CommonHealthProfileID;
                    obj.UserID = model.UserID;
                    obj.Year = model.Year;
                    uow.HealthDataBoard.Add(obj);
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
        [Route("api/healthdataboard/{id}")]
        public IHttpActionResult Put(int id, HealthDataBoardModel model)
        {
            try
            {
                using (var uow = new UnitOfWork(new DataContext()))
                {
                    var obj = uow.HealthDataBoard.Get(id);
                    if (obj != null)
                    {
                        obj.Count = model.Count;
                        obj.CommonHealthProfileID = model.CommonHealthProfileID;
                        obj.UserID = model.UserID;
                        obj.Year = model.Year;
                        uow.HealthDataBoard.Edit(obj);
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
        [Route("api/healthdataboard/{id}")]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                using (var uow = new UnitOfWork(new DataContext()))
                {
                    var obj = uow.HealthDataBoard.Get(id);
                    if (obj != null)
                    {
                        uow.HealthDataBoard.Remove(obj);
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
