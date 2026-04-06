using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using EMedicineBE.Models;
using System.Data.SqlClient;
namespace EMedicineBE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicineController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public MedicineController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpPost]
        [Route("addToCart")]
        public Response addToCart(Cart cart)
        {
            DataAccessLayer dal = new DataAccessLayer();
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("EMedCS").ToString());
            Response response = dal.addToCart(cart, connection);
            return response;
        }

        [HttpPost]
        [Route("placeorder")]
        public Response placeOrder(Users users)
        {
            DataAccessLayer dal = new DataAccessLayer();
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("EMedCS").ToString());
            Response response = dal.placeOrder(users, connection);
            return response;
        }
    }
}
