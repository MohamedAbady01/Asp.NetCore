using HiCraftApi.Services.CraftManServices;
using System.Security.Claims;
using HiCraftApi.Services.Custmers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using HiCraftApi.Models;

namespace HiCraftApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
  //  [Authorize (Roles ="Customer")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustmers _service;

        public CustomerController(ICustmers custmer)
        {
            _service = custmer;
        }
        [HttpGet("GetAllCrafts")]
        public async Task<IActionResult> GetCraftbyCategoryName(string CategoryName,string City)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var crafts = await _service.GetCraftbyCategoryNameOrCraftName(CategoryName, City);
            if (crafts.Count > 0)
            {
                return Ok(crafts);
            }
            return BadRequest("Specialization Not Found ");


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
        [HttpGet("GetCraftbyCategoryNameOrCraftName")]
        public async Task<IActionResult> GetCraftbyCategoryNames(string CategoryName,String City)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var crafts = await _service.GetCraftbyCategoryNameOrCraftName(CategoryName, City);
            if (crafts.Count > 0)
            {
                return Ok(crafts);
            }
            return BadRequest("CraftMan Not Found ");


        }
        [HttpGet("GetCustomerById")]
        public async Task<IActionResult> GetCustmerById(string id)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var crafts = await _service.GetCustmerById(id);
            if (crafts != null)
            {
                return Ok(crafts);
            }
            return BadRequest("GetCustomer Not Found ");


        }
        [HttpPut("EditCustomer")]
        public async Task<IActionResult> EditCustmer(string? CustomerId, [FromForm] Custmrdto model)
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _service.EditCustmer(CustomerId,model);
                return Ok("Updated");
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
        [HttpPost("CreateReview")]
        public async Task<IActionResult> CreateReview(Reviewsdto Model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var model = await _service.CreateReview(Model);
            if (model != null)
            {
                return Ok(model);
            }
            return BadRequest("Not   Authorize ");


        }
        [HttpPost("MakeRequest")]
        public async Task<IActionResult> MakeRequest(Requestsdto Model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var model = await _service.MakeRequest(Model);
            if (model != null)
            {
                return Ok(model);
            }
            return BadRequest("Request hasn't been  sent  ");


        }
        [HttpGet("GetAllRequests")]
        public async Task<IActionResult> GetAllRequests(string? UserId )
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var model = await _service.GetallRequests(UserId);
            if (model.Count> 0)
            {
                return Ok(model);
            }
            return BadRequest("Requests Not Found   ");


        }
        [HttpDelete("DeleteRequest")]
        public async Task<IActionResult> DeleteRequest(string? CustomerId, int reqid)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var model = await _service.DeleteRequest(CustomerId, reqid);
            if (model != null)
            {
                return Ok(model);
            }
            return BadRequest("Not effected  ");


        }

    }
}
