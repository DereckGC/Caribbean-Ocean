using CaribbeanOcean.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace CaribbeanOcean.API.Controllers
{
    // Sets the base URL for this controller to: https://localhost:{{port}}/api/Hero
    [Route("api/[controller]")]
    [ApiController]
    public class ContactUsController(HotelContactService hotelContactService) : Controller
    {
        // GET: api/ContactUs
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var hotelContacts = await hotelContactService.GetAll();
            return Ok(hotelContacts); // Returns HTTP 200 OK
        }
    }
}
