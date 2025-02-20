using System.Net;
using Hotels.BusinessLogic.Interface;
using Hotels.Entity.Entities;
using Hotels.WebApi.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hotels.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBooking IBookingBL;

        public BookingController(IBooking booking)
        {
            IBookingBL = booking;
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create([FromBody] Booking booking)
        {
            try
            {
                var result = IBookingBL.Create(booking);
                await Email.SendEmail(booking);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        [HttpGet]
        [Route("FindAllByAgent")]
        public IActionResult FindAllByAgent(int agentId)
        {
            try
            {
                var result = IBookingBL.FindAllByAgent(agentId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        [HttpGet]
        [Route("FindById")]
        public IActionResult FindById(int id)
        {
            try
            {
                var result = IBookingBL.FindById(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        private ObjectResult HandleException(Exception ex)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
        }
    }
}
