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
    public class BooksController : ControllerBase
    {
        public BooksService _bookservice;
        public BooksController(BooksService bookservice)
        {
            _bookservice = bookservice;
        }
        [HttpGet("get-all-books")]
        public IActionResult GetAllBooks()
        {
            
            return Ok(_bookservice.GetAllBooks());
        }
        [HttpGet("get-book-by-id/{bookid}")]
        public IActionResult GetBookById(int bookid)
        {
            var _book = _bookservice.GetBookById(bookid);
            return Ok(_book);
        }
        [HttpDelete("delete-book-by-id/{bookid}")]
        public IActionResult DeleteBookById(int bookid)
        {
            _bookservice.DeleteBookById(bookid);
            return Ok();
        }
        [HttpPut("update-book-by-id/{id}")]
        public IActionResult UpdateBookById(int id,[FromBody]BookVM book)
        {
            return Ok(_bookservice.UpdateBookById(id, book));
        }
        [HttpPost("add-book-with-authors")]
        public IActionResult AddBook([FromBody]BookVM book)
        {
            _bookservice.AddBookWithAuthors(book);
            return Ok();
        }
        

    }
}
