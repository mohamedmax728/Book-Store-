using my_books.Data.Models;
using my_books.Data.Paging;
using my_books.Data.Services.ViewModels;
using my_books.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace my_books.Data.Services
{
    public class PublishersService
    {
        private AppDbContext _Context;
        public PublishersService(AppDbContext context)
        {
            _Context = context;
        }
        public Publisher AddPublisher(PublisherVM Publisher)
        {
            if (StringStartsWithNumber(Publisher.Name))
            {
                throw new PublisherNameException("this name start with number",Publisher.Name);
            }
            var _Publisher = new Publisher()
            {
                Name = Publisher.Name
            };
            _Context.Publishers.Add(_Publisher);
            _Context.SaveChanges();
            return _Publisher;
        }
        public List<Publisher> GetAllPublishers(string sortBy,string searchString,int? PageNumber)
        {

            var Publishers=_Context.Publishers.OrderBy(n=>n.Name).ToList();
            if(sortBy=="name_desc")
            {
                Publishers = _Context.Publishers.OrderByDescending(n => n.Name).ToList();
            }
            if(!string.IsNullOrEmpty(searchString))
            {
                Publishers = Publishers.Where(n => n.Name.Contains(searchString,
                    StringComparison.CurrentCultureIgnoreCase)).ToList();
            }
            //paging 
            int pagesize = 5;
            Publishers = PaginatedList<Publisher>.Create(Publishers.AsQueryable(), PageNumber??1, pagesize);
            return Publishers;
        }

        /*StringComparison.CurrentCultureIgnoreCase to ignore uppercase and lowercase in filtering and show all case of 
           one character*/
        public Publisher GetPublisherById(int id)
        {
            return _Context.Publishers.FirstOrDefault(n => n.Id == id);
        }
        public PublisherWithBooksAndAuthorsVM GetPublisherWithBooksAndAuthors(int id)
        {
            PublisherWithBooksAndAuthorsVM publisherWithBooksAndAuthorsVM =
                _Context.Publishers.Where(n => n.Id == id).Select(n => new PublisherWithBooksAndAuthorsVM()
                {
                    Name=n.Name,
                    BookAuthors=n.Books.Select(n=>new BookAuthorsVM()
                    {
                        BookName=n.Title,
                        BookAuthors=n.Book_Authors.Select(n=>n.Author.FullName).ToList()
                    }).ToList()
                }).FirstOrDefault();
            return publisherWithBooksAndAuthorsVM;
        }
        public void DeletePublisherById(int id)
        {
            var _publisher = _Context.Publishers.FirstOrDefault(n => n.Id == id);
            if(_publisher!=null)
            {
                _Context.Publishers.Remove(_publisher);
                _Context.SaveChanges();
            }else
            {
                throw new Exception($"the publisher with id : {id} does not exist");
            }
        }
        private bool StringStartsWithNumber(string name) => Regex.IsMatch(name,@"^\d");
        

        
    }
}
