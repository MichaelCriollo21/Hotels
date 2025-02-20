using System.Net;
using Hotels.BusinessLogic.Class;
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
    public class RoomController : ControllerBase
    {
        private readonly IRoom IRoomBL;

        public RoomController(IRoom room)
        {
            IRoomBL = room;
        }

        [HttpPost]
        [Route("Create")]
        [Authorize(Roles = "Agency")]
        public async Task<IActionResult> CreateRoom([FromBody] Room entity)
        {
            try
            {
                var room = IRoomBL.Create(entity);
                return Ok(room);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        [HttpPut]
        [Route("Update")]
        [Authorize(Roles = "Agency")]
        public async Task<IActionResult> UpdateRoom([FromBody] Room entity)
        {
            try
            {
                var result = IRoomBL.Update(entity);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        [HttpPut]
        [Authorize(Roles = "Agency")]
        [Route("ChangeStatus")]
        public async Task<IActionResult> ChangeStatus(int Id)
        {
            try
            {
                var result = IRoomBL.ChangeStatus(Id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        [HttpGet]
        [Route("SearchRooms")]
        public async Task<IActionResult> SearchRooms(DateTime checkIn, DateTime checkOut, int guests, string city)
        {
            try
            {
                var result = await IRoomBL.SearchRooms(checkIn, checkOut, guests, city);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        [HttpGet]
        [Route("GetRoomsByHotel")]
        public async Task<IActionResult> GetRoomsByHotel(int hotelId)
        {
            try
            {
                var result = await IRoomBL.GetRoomsByHotel(hotelId);
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
