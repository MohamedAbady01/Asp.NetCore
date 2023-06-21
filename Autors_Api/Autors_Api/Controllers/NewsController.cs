using Autors_Api.Models;
using Autors_Api.Services.News;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Autors_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly INews _newsService;

        public NewsController(INews newsService)
        {
            _newsService = newsService;
        }

        [HttpGet("GetAllNews")]
        public async Task<ActionResult<IEnumerable<NewsModel>>> GetAllNews()
        {
            var news = await _newsService.GetAllNewsAsync();
            return Ok(news);
        }

        [HttpGet("GetDetails/{id}")]
        public async Task<ActionResult<NewsModel>> GetDetails(int id)
        {
            var news = await _newsService.GetNewsByIdAsync(id);
            if (news == null)
            {
                return NotFound();
            }
            return Ok(news);
        }

        [HttpPost("AddNews")]
        public async Task<ActionResult> AddNews([FromForm] NewsDto news)
        {
            try
            {
                await _newsService.AddNewsAsync(news, news.Image);
                return Ok( news);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("EditNews/{id}")]
        public async Task<IActionResult> UpdateNews(int id, [FromForm] NewsDto newsDto)
        {
   

            try
            {

                await _newsService.UpdateNewsAsync(id,newsDto, newsDto.Image);
                return Ok("Updated");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpDelete("DeleteNews/{id}")]
        public async Task<ActionResult> DeleteNews(int id)
        {
            await _newsService.DeleteNewsAsync(id);
            return NoContent();
        }
    }
}
