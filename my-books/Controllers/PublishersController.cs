using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using my_books.ActionResult;
using my_books.Data.Models;
using my_books.Data.Services;
using my_books.Data.Services.ViewModels;
using my_books.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace my_books.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class PublishersController : ControllerBase
    {
        public PublishersService _publisherservice;
        private readonly ILogger<PublishersController> _Ilogger;
        public PublishersController(PublishersService publisherservice,ILogger<PublishersController> Ilogger)
        {
            _publisherservice = publisherservice;
            _Ilogger = Ilogger;
        }
        [HttpPost("add-publisher")]
        public IActionResult Addpublisher([FromBody] PublisherVM publisher)
        {
            try
            {
                return Created(nameof(Addpublisher), _publisherservice.AddPublisher(publisher));
            }
            catch(PublisherNameException ex)
            {
                return BadRequest($"{ex.Message},{ex.PublisherName}");
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
            
        }
        [HttpGet("get-all-publishers")]
        public IActionResult GetAllPublishers(string sortBy,string searchString,int PageNumber)
        {
           // throw new Exception("this is an exception thrown from GetAllPublishers");
            try
            {
                _Ilogger.LogInformation("this is just a log in getallpublishers");
                var publishers = _publisherservice.GetAllPublishers(sortBy, searchString, PageNumber);
                return Ok(publishers);
            }
            catch (Exception)
            {
                return BadRequest("sorry!!there is no publishers data");
            }

        }
        [HttpGet("get-publisher-by-id/{id}")]
        public IActionResult GetPublisherById(int id)
        {
            var _publisher = _publisherservice.GetPublisherById(id);
            if (_publisher != null)
            {
                /*var response = new CustomActionResultVM()
                {
                    publisher = _publisher
                };
                return new  CustomActionResult(response);*/
                return Ok(_publisher);
            }
            else
            {
                /*var response = new CustomActionResultVM()
                {
                    Exception = new Exception("this is coming from publisher controller ")
                };
                return new CustomActionResult(response);*/
                //return null;
                return NotFound();
            }
            
        }
        [HttpGet("get-publisher-with-book-and-author/{id}")]
        public IActionResult GetPublisherWithBooksAndAuthors(int id)
        {
            return Ok(_publisherservice.GetPublisherWithBooksAndAuthors(id));
        }
        [HttpDelete("delete-publisher-by-id/{id}")]
        public IActionResult DeletePublisherById(int id)
        {
            try
            {

                _publisherservice.DeletePublisherById(id);
                return Ok();


            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
