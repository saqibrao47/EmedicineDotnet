using Microsoft.AspNetCore.Connections;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using EMedicineBE.Models;

namespace EMedicineBE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public UsersController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        [Route("registration")]

        public Response register(Users users)
        {
            Response response = new Response();
            DataAccessLayer dal = new DataAccessLayer();
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("EMedCS").ToString());
            response = dal.register(users, connection);
            return response;
        }

        [HttpPost]
        [Route("login")]
        public Response login(Users users)
        { 
        DataAccessLayer dal = new DataAccessLayer();
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("EMedCS").ToString());
            Response response = dal.login(users, connection);
            return response;
        }

        [HttpPost]
        [Route("viewUsers")]
        public Response viewUser(Users users)
        {
            DataAccessLayer dal = new DataAccessLayer();
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("EMedCS").ToString());
            Response response = dal.viewUser(users, connection);
            return response;
        }

        [HttpPost]
        [Route("updateUserProfile")]
        public Response updateUserProfile(Users users)
        {
            DataAccessLayer dal = new DataAccessLayer();
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("EMedCS").ToString());
            Response response = dal.updateUserProfile(users, connection);
            return response;
        }
     }
}
