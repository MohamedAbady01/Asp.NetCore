﻿using System.Data;
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
        public async Task<IActionResult> GetAllCrafts(int catid,string City)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var crafts = await _service.GetAllCrafts(catid, City);

            if(crafts.Count > 0)
            {
                return Ok(crafts);
            }
            return BadRequest("Craftmans Not Found ");

            
        }
        [HttpGet("GetCraftbyCategoryId")]
        public async Task<IActionResult> GetCraftbyCategoryId(int CategoryId,string City)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var crafts = await _service.GetCraftbyCategoryId(CategoryId, City);
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
            if (crafts != null)
            {
                return Ok(crafts);
            }
            return BadRequest("CraftMan Not Found ");


        }
        [HttpGet("GetCustomerById")]
        public async Task<IActionResult> GetCustomerById(string id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var Cust = await _service.GetCustmerById(id);
            if (Cust.Count > 0)
            {
                return Ok(Cust);
            }
            return BadRequest("Customer Not Found ");


        }
        [HttpPut("EditCraft")]
        public async Task<IActionResult> EditCraft(string? CraftManId,[FromForm] Craftdto model)
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _service.EditCraft(CraftManId, model);
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
        [HttpDelete("DeleteImage")]
        public async Task<IActionResult> DeleteImage(int id)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var deltedImage = await _service.DeleteImage(id);
            if (deltedImage != null)
            {
                return Ok("Deleted");
            }
            return BadRequest("Image Not Found ");


        }
        [HttpGet("GetAllRequests")]
        public async Task<IActionResult> GetAllRequests(string? UserId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var model = await _service.GetAllRequests(UserId);
            if (model.Count > 0)
            {
                return Ok(model);
            }
            return BadRequest("Requests Not Found   ");


        }
        [HttpGet("GetAllReviews")]
        public async Task<IActionResult> GetAllReviews(string? UserId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var model = await _service.GetAllReviews(UserId);
            if (model.Count > 0)
            {
                return Ok(model);
            }
            return BadRequest("Reviews Not Found   ");


        }
        [HttpPost("AcceptRequest")]
        public async Task<IActionResult> AcceptRequest(string? CraftManId,int RequestId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var request = await _service.AcceptRequest(CraftManId,RequestId);
            if (request == null)
            {
                return BadRequest("Error While Accepting the Request     ");
                
            }
            return Ok(request);

        }
        [HttpPost("DeclineRequest")]
        public async Task<IActionResult> DeclineRequest(string? CraftManId,int RequestId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var request = await _service.DeclineRequest(CraftManId, RequestId);
            if (request == null)
            {
                return BadRequest("Error While Decline the Request  ");

            }
            return Ok(request);

        }


    }
    }
