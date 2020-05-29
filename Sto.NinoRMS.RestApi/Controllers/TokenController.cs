using Sto.NinoRMS.Queries.Persistence;
using System;
using System.Net;
using System.Web.Http;
using WebApi.Jwt.Filters;
using WebApi.Jwt.Models;

namespace WebApi.Jwt.Controllers
{
    public class TokenController : ApiController
    {
        // THis is naive endpoint for demo, it should use Basic authentication to provide token or POST request
        [AllowAnonymous]
        [HttpPost]
        public IHttpActionResult Auth(AuthModel user)
        {
            try
            {
                if (CheckUser(user.UserName, user.Password))
                {
                    return Ok(JwtManager.GenerateToken(user.UserName));
                }
                else
                {
                    return NotFound();
                }

            }
            catch (Exception ex)
            {

                InternalServerError(ex);
            }
           

            throw new HttpResponseException(HttpStatusCode.Unauthorized);
        }
        [JwtAuthentication]
        [HttpGet]
        public IHttpActionResult Test()
        {
           
                return Ok("this is test");
           
        }
        public bool CheckUser(string username, string password)
        {
            var crypto = new SimpleCrypto.PBKDF2();
            using (var uow = new UnitOfWork(new DataContext()))
            {
                var user = uow.Users.GetUser(username);
                if (user != null)
                {
                    if (user.Password != crypto.Compute(password, user.PasswordSalt))
                    {
                        return false;
                    }
                   
                }
                else
                {
                    return false;
                }
            }
            return true;
        }
    }
   
}
