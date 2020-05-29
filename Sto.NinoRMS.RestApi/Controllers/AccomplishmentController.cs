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
    public class AccomplishmentController : ApiController
    {
        [AllowAnonymous]
        [HttpGet]
        [Route("api/accomplishments")]
        public IHttpActionResult Get()
        {
            try
            {
                using (var uow = new UnitOfWork(new DataContext()))
                {
                    var objs = uow.Accomplishments.GetAll();
                   
                    var models = new List<AccomplishmentModel>();
                    foreach (var item in objs)
                    {
                        var model = new AccomplishmentModel();
                        model.ID = item.AccomplishmentID;
                        model.Title = item.Title;
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
        [Route("api/accomplishments/{criteria?}")]
        public IHttpActionResult Get(string criteria = "")
        {
            try
            {
                using (var uow = new UnitOfWork(new DataContext()))
                {
                    criteria = criteria == "All" ? "" : criteria;
                  
                    var objs = uow.Accomplishments.GetAllBy(criteria: criteria);
                    var models = new List<AccomplishmentModel>();
                    foreach (var item in objs)
                    {
                        var model = new AccomplishmentModel();
                        model.ID = item.AccomplishmentID;
                        model.Title = item.Title;
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
        [Route("api/accomplishment/{id}")]
        public IHttpActionResult Get(int id)
        {
            try
            {
                using (var uow = new UnitOfWork(new DataContext()))
                {
                    var obj = uow.Accomplishments.Get(id);
                    var model = new AccomplishmentModel();
                    model.ID = obj.AccomplishmentID;
                    model.Title = obj.Title;
                    return Ok(model);
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/accomplishment")]
        public IHttpActionResult Post(AccomplishmentModel model)
        {
            try
            {
                using (var uow = new UnitOfWork(new DataContext()))
                {
                    var obj = new Accomplishment();
                    obj.Title = model.Title;
                    uow.Accomplishments.Add(obj);
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
        [Route("api/accomplishment/{id}")]
        public IHttpActionResult Put(int id, AccomplishmentModel model)
        {
            try
            {
                using (var uow = new UnitOfWork(new DataContext()))
                {
                    var obj = uow.Accomplishments.Get(id);
                    obj.Title = model.Title;
                    uow.Accomplishments.Edit(obj);
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
        [HttpDelete]
        [Route("api/accomplishment/{id}")]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                using (var uow = new UnitOfWork(new DataContext()))
                {
                    var obj = uow.Accomplishments.Get(id);
                    uow.Accomplishments.Remove(obj);
                    uow.Complete();
                    return Ok();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
