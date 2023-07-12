using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Text.Json.Serialization;
using System.Text.Json;
using Task_2Api.Models;
using Task_2Api.Services.ApprovalServices;

namespace Task_2Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApprovalController : ControllerBase
    {
        private readonly IApproval _service;
        public ApprovalController(IApproval service)
        {
            _service = service;
        }

        private static readonly JsonSerializerOptions jsonOptions = new JsonSerializerOptions
        {
            ReferenceHandler = ReferenceHandler.Preserve
        };
        [HttpPost("CreateApproval")]
        public async Task<IActionResult> CreateApproval([FromForm] Approvaldto approvaldto)
        {
            if (approvaldto == null)
            {
                return BadRequest("Model Not Found");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Validate attachments
            foreach (var attachment in approvaldto.Attachments)
            {
                if (attachment.Length > 5 * 1024 * 1024) // 5 MB
                {
                    ModelState.AddModelError("Attachments", "Attachment file size cannot exceed 5 MB.");
                    return BadRequest(ModelState);
                }
            }

            await _service.CreateApprovalModel(approvaldto);
            return Ok();
        }

        [HttpGet("GetAllApprovals")]
        public async  Task<IActionResult> GetaaGetAllApprovals()
        {

           var Approvals=  _service.GetAllApprovals();

            if (Approvals == null)
            {
                return BadRequest();
            }
            return Ok(Approvals);
        }
        [HttpGet("GetApprovalById")]
        public async Task<IActionResult> GetCraftById(int id)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var Approval = await _service.GetApprovalById(id);
            if (Approval != null)
            {
                return Ok(Approval);
            }
            return BadRequest("Approval Not Found ");


        }
        [HttpPut("UpdateApproval")]
        public async Task<IActionResult> UpdateApproval(int ApprovalId,[FromForm] Approvaldto approvaldto)
        {
            if (approvaldto == null)
            {
                return BadRequest("Model Not Found");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Validate attachments
            foreach (var attachment in approvaldto.Attachments)
            {
                if (attachment.Length > 5 * 1024 * 1024) // 5 MB
                {
                    ModelState.AddModelError("Attachments", "Attachment file size cannot exceed 5 MB.");
                    return BadRequest(ModelState);
                }
            }

            await _service.UpdateApproval(ApprovalId, approvaldto);
            return Ok();
        }

        [HttpDelete("DeleteApproval")]
        public async Task<IActionResult> DeleteApprovalById(int id)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            await _service.DeleteApproval(id);

                return Ok();
           


        }
    }
}