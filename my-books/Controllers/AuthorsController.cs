using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using my_books.Data.Services;
using my_books.Data.Services.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace my_books.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        public AuthorsService _authorservice;

        public AuthorsController(AuthorsService authorservice)
        {
            _authorservice = authorservice;
        }
        [HttpPost("add-author")]
        public IActionResult Addauthor([FromBody] AuthorVM author)
        {
            _authorservice.AddAuthor(author);
            return Ok();
        }
        [HttpGet("get-author-with-books-by-id/{id}")]
        public IActionResult GetAuthorsWithBooksById(int id)
        {
           
            return Ok(_authorservice.GetAuthorsWithBooksById(id));
        }
    }
}
