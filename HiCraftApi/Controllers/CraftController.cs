using System.Data;
using System.Numerics;
using System.Security.Claims;
using HiCraftApi.Models;
using HiCraftApi.Services;
using HiCraftApi.Services.CraftManServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HiCraftApi.Controllers
{
   // [Authorize(Roles = "CraftMan")]
    [Route("api/[controller]")]
    [ApiController]
    public class CraftController : ControllerBase
    {
        private readonly ICraftMan _service;

        public CraftController (ICraftMan Service)
        {
            _service = Service;
        }
        [HttpGet("GetAllCrafts")]
        public async Task<IActionResult> GetAllCrafts(int catid)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var crafts = await _service.GetAllCrafts(catid);
            if(crafts.Count > 0)
            {
                return Ok(crafts);
            }
            return BadRequest("Specialization Not Found ");

            
        }
        [HttpGet("GetCraftByCategoryName")]
        public async Task<IActionResult> GetCraftbyCategoryName(string CategoryName)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var crafts = await _service.GetCraftbyCategoryName(CategoryName);
            if (crafts.Count > 0)
            {
                return Ok(crafts);
            }
            return BadRequest("CraftMan Not Found ");


        }
        [HttpGet("GetCraftById")]
        public async Task<IActionResult> GetCraftById(string id)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var crafts = await _service.GetCraftbyId(id);
            if (crafts.Count > 0)
            {
                return Ok(crafts);
            }
            return BadRequest("CraftMan Not Found ");


        }
        [HttpGet("GetCustmerById")]
        public async Task<IActionResult> GetCustmerById(string id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var Cust = await _service.GetCustmerById(id);
            if (Cust.Count > 0)
            {
                return Ok(Cust);
            }
            return BadRequest("Cystomer Not Found ");


        }
        [HttpPost("EditCraft")]
        public async Task<IActionResult> EditCraft([FromForm] Craftdto model)
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _service.EditCraft(userId, model);
                return Ok();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest("An error occurred while updating the craft: " + ex.Message);
            }
        }

        [HttpGet("GetAllSpecializations")]
        public async Task<IActionResult> GetAllSpecializations()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var crafts = await _service.GetAllSpecializations();
            if (crafts.Count > 0)
            {
                return Ok(crafts);
            }
            return BadRequest("Specialization Not Found ");


        }


    }
}
