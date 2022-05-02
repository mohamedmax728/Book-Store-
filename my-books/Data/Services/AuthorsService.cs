using Microsoft.EntityFrameworkCore;
using my_books.Data.Models;
using my_books.Data.Services.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace my_books.Data.Services
{
    public class AuthorsService
    {
        private AppDbContext _Context;
        public AuthorsService(AppDbContext context)
        {
            _Context = context;
        }
        public void AddAuthor(AuthorVM Author)
        {
            var _Author = new Author()
            {
                FullName = Author.FullName
            };
            _Context.Authors.Add(_Author);
            _Context.SaveChanges();
        }
        public AuthorsWithBookVM GetAuthorsWithBooksById(int id)
        {
            AuthorsWithBookVM authorsWithBookVM =
                _Context.Authors.Where(n => n.Id == id).Select(n => new AuthorsWithBookVM()
                {
                    FullName=n.FullName,
                    BookTitles=n.Book_Authors.Select(ti=>ti.Book.Title).ToList()
                }).FirstOrDefault();
            return authorsWithBookVM;
        }
    }
}
     
    

