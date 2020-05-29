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
    public class CommonHealthProfileController : ApiController
    {
        [AllowAnonymous]
        [HttpGet]
        [Route("api/commonhealthprofiles")]
        public IHttpActionResult Get()
        {
            try
            {
                using (var uow = new UnitOfWork(new DataContext()))
                {
                    var objs = uow.CommonHealthProfiles.GetAll();
                    var models = new List<CommonHealthProfileModel>();
                    foreach (var item in objs)
                    {
                        var model = new CommonHealthProfileModel();
                        model.ID = item.CommonHealthProfileID;
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
        [Route("api/commonhealthprofiles/{criteria?}")]
        public IHttpActionResult Get(string criteria = "")
        {
            try
            {
                using (var uow = new UnitOfWork(new DataContext()))
                {
                    criteria = criteria == "All" ? "" : criteria;
                    var objs = uow.CommonHealthProfiles.GetAllBy(title: criteria);
                    var models = new List<CommonHealthProfileModel>();
                    foreach (var item in objs)
                    {
                        var model = new CommonHealthProfileModel();
                        model.ID = item.CommonHealthProfileID;
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
        [Route("api/commonhealthprofile/{id}")]
        public IHttpActionResult Get(int id)
        {
            try
            {
                using (var uow = new UnitOfWork(new DataContext()))
                {
                    var obj = uow.CommonHealthProfiles.Get(id);
                    var model = new AccomplishmentModel();
                    model.ID = obj.CommonHealthProfileID;
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
        [Route("api/commonhealthprofile")]
        public IHttpActionResult Post(CommonHealthProfileModel model)
        {
            try
            {
                using (var uow = new UnitOfWork(new DataContext()))
                {
                    var obj = new CommonHealthProfile();
                    obj.CommonHealthProfileID = model.ID;
                    obj.Title = model.Title;
                    uow.CommonHealthProfiles.Add(obj);
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
        [Route("api/commonhealthprofile/{id}")]
        public IHttpActionResult Put(int id, CommonHealthProfileModel model)
        {
            try
            {
                using (var uow = new UnitOfWork(new DataContext()))
                {
                    var obj = uow.CommonHealthProfiles.Get(id);
                    obj.Title = model.Title;
                    uow.CommonHealthProfiles.Edit(obj);
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
        [Route("api/commonhealthprofile/{id}")]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                using (var uow = new UnitOfWork(new DataContext()))
                {
                    var obj = uow.CommonHealthProfiles.Get(id);
                    uow.CommonHealthProfiles.Remove(obj);
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
