using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using my_books.Controllers;
using my_books.Data;
using my_books.Data.Models;
using my_books.Data.Services;
using my_books.Data.Services.ViewModels;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace my_books_tests
{
    class PublishersControllerTests
    {
        private static DbContextOptions<AppDbContext> _options =
            new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase("BookDbControllerTest").Options;
        AppDbContext context;
        PublishersService publishersService;
        PublishersController publishersController;
        [OneTimeSetUp]
        public void Setup()
        {
            context = new AppDbContext(_options);
            context.Database.EnsureCreated();

            seedDataBase();

            publishersService = new PublishersService(context);
            publishersController =
                new PublishersController(publishersService, new NullLogger<PublishersController>());
        }
        [Test,Order(1)]
        public void Get_All_Publishers_withsort_search_pagenr()
        {
            IActionResult actionResult1 = 
                publishersController.GetAllPublishers("name_desc","publisher",1);
            Assert.That(actionResult1, Is.TypeOf<OkObjectResult>());
            var data1 = (actionResult1 as OkObjectResult).Value as List<Publisher>;
            Assert.That(data1.First().Name, Is.EqualTo("Publisher 6"));
            Assert.That(data1.First().Id, Is.EqualTo(6));
            Assert.That(data1.Count, Is.EqualTo(5));

            IActionResult actionResult2 =
                publishersController.GetAllPublishers("name_desc", "publisher", 2);
            Assert.That(actionResult2, Is.TypeOf<OkObjectResult>());
            var data2 = (actionResult2 as OkObjectResult).Value as List<Publisher>;
            Assert.That(data2.First().Name, Is.EqualTo("Publisher 1"));
            Assert.That(data2.First().Id, Is.EqualTo(1));
            Assert.That(data2.Count, Is.EqualTo(1));
        }
        [Test,Order(2)]
        public void GetPublisherBYId_ok_test()
        {
            IActionResult actionResult1 =
                publishersController.GetPublisherById(1);
            Assert.That(actionResult1, Is.TypeOf<OkObjectResult>());
            var data1 = (actionResult1 as OkObjectResult).Value as Publisher;
            Assert.That(data1.Id, Is.EqualTo(1));
            //Assert.That(data1.Name, Is.EqualTo("Publisher 1"));
            Assert.That(data1.Name, Is.EqualTo("publisher 1").IgnoreCase);
        }
        [Test, Order(3)]
        public void GetPublisherBYId_Not_found_Test()
        {
            IActionResult actionResult1 =
                publishersController.GetPublisherById(99);
            Assert.That(actionResult1, Is.TypeOf<NotFoundResult>());
            
        }
        [Test, Order(4)]
        public void Addpublisher_Created_Test()
        {
            var publisher = new PublisherVM()
            {
                Name="new publisher"
            };
            IActionResult actionResult1 =
                publishersController.Addpublisher(publisher);
            Assert.That(actionResult1, Is.TypeOf<CreatedResult>());

        }
        [Test, Order(5)]
        public void Addpublisher_BadRequest_Test()
        {
            var publisher = new PublisherVM()
            {
                Name = "123 new publisher"
            };
            IActionResult actionResult1 =
                publishersController.Addpublisher(publisher);
            Assert.That(actionResult1, Is.TypeOf<BadRequestObjectResult>());

        }
        [Test, Order(6)]
        public void DeletePublisherById_Ok_Test()
        {
            
            IActionResult actionResult1 =
                publishersController.DeletePublisherById(6);
            Assert.That(actionResult1, Is.TypeOf<OkResult>());

        }
        [Test, Order(7)]
        public void DeletePublisherById_BadRequest_Test()
        {

            IActionResult actionResult1 =
                publishersController.DeletePublisherById(999);
            Assert.That(actionResult1, Is.TypeOf<BadRequestObjectResult>());

        }
        [Test,Order(8)]
        public void GetPublisherWithBooksAndAuthors_Ok_Test()
        {
            IActionResult actionResult =
                publishersController.GetPublisherWithBooksAndAuthors(1);
            Assert.That(actionResult, Is.TypeOf<OkObjectResult>());
        }
        [OneTimeTearDown]
        public void CleanUp()
        {
            context.Database.EnsureDeleted();
        }
        private void seedDataBase()
        {
            var publishers = new List<Publisher>
            {
                    new Publisher() {
                        Id = 1,
                        Name = "Publisher 1"
                    },
                    new Publisher() {
                        Id = 2,
                        Name = "Publisher 2"
                    },
                    new Publisher() {
                        Id = 3,
                        Name = "Publisher 3"
                    },
                    new Publisher() {
                        Id = 4,
                        Name = "Publisher 4"
                    },
                    new Publisher() {
                        Id = 5,
                        Name = "Publisher 5"
                    },
                    new Publisher() {
                        Id = 6,
                        Name = "Publisher 6"
                    },
            };
            context.Publishers.AddRange(publishers);
            context.SaveChanges();
        }
    }
}
