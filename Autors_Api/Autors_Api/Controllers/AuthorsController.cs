using Autors_Api.Models;
using Autors_Api.Services.Authors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Autors_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthors _authorsService;

        public AuthorsController(IAuthors authorsService)
        {
            _authorsService = authorsService;
        }

        [HttpGet("GetAllAuthors")]
        public async Task<IActionResult> GetAllAuthors()
        {
            var authors = await _authorsService.GetAllAuthorsAsync();
            if (authors == null)
            {
                return NotFound();
            }
            if (authors.ToList().Count == 0)
            {
                return Ok("There is no  Authors");

            }
            return Ok(authors);
        }

        [HttpGet("GetAuthor/{id}")]
        public async Task<IActionResult> GetAuthorById(int id)
        {
            var author = await _authorsService.GetAuthorByIdAsync(id);
            if (author == null)
            {
                return NotFound();
            }
            return Ok(author);
        }

        [HttpPost("AddAuthor")]
        public async Task<IActionResult> AddAuthor([FromBody] Author author)
        {
            if (author == null)
            {
                return BadRequest();
            }

            try
            {
                await _authorsService.AddAuthorAsync(author);
                return Ok(author);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpPut("EditAuthor/{id}")]
        public async Task<IActionResult> EditAuthor(int id, [FromBody] Author author)
        {
            if (author == null || id == null )
            {
                return BadRequest("UserNotFound");
            }

            try
            {
                await _authorsService.UpdateAuthorAsync(id,author);
                return Ok(author);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpDelete("DeleteAuthor/{id}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            try
            {
                await _authorsService.DeleteAuthorAsync(id);
                return Ok("Deleted");
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
    }
}
