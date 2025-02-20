using System.Net;
using Hotels.BusinessLogic.Interface;
using Hotels.Entity.DTO;
using Hotels.Entity.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hotels.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelController : ControllerBase
    {
        private readonly IHotel IHotelBL;

        public HotelController(IHotel hotel)
        {
            IHotelBL = hotel;
        }

        [HttpPost]
        [Authorize(Roles = "Agency")]
        [Route("Create")]
        public async Task<IActionResult> CreateHotel([FromBody] CreateHotelDTO hotelDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var entity = new Hotel
                {
                    Name = hotelDto.Name,
                    Location = hotelDto.Location,
                    IsEnabled = true
                };

                var hotel = IHotelBL.Create(entity);
                return Ok(hotel);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        [HttpPut]
        [Authorize(Roles = "Agency")]
        public async Task<IActionResult> UpdateHotel([FromBody] Hotel hotelDto)
        {
            var result = IHotelBL.Update(hotelDto);
            return Ok(result);
        }

        private ObjectResult HandleException(Exception ex)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
        }
    }
}
