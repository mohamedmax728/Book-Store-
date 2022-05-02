using my_books.Data.Models;
using my_books.Data.Services.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace my_books.Data.Services
{
    public class BooksService
    {
        private AppDbContext _Context;
        public BooksService(AppDbContext context)
        {
            _Context = context;
        }
        public void AddBookWithAuthors(BookVM book)
        {
            var _book = new Book()
            {
                Title = book.Title,
                Description = book.Description,
                IsRead = book.IsRead,
                CoverUrl = book.CoverUrl,
                DateRead = book.IsRead ? book.DateRead : null,
                Rate = book.IsRead ? book.Rate : null,
                Genre = book.Genre,
                DateAdded = DateTime.Now,
                PublisherId = book.PublisherId
            };
            _Context.Books.Add(_book);
            _Context.SaveChanges();
            foreach(var _id in book.AuthorIds)
            {
                var _book_author = new Book_Author()
                {
                    BookId = _book.Id,
                    AuthorId = _id
                };
                _Context.Book_Authors.Add(_book_author);
                _Context.SaveChanges();
            }
        }
        public List<Book> GetAllBooks()
        {
            return _Context.Books.ToList();
        }
        public BookWithAuthorsVM GetBookById(int bookid)
        {
            BookWithAuthorsVM _BookWithAuthorsVM =
                _Context.Books.Where(book => book.Id == bookid).Select(book => new BookWithAuthorsVM()
                {
                    Title = book.Title,
                    Description = book.Description,
                    IsRead = book.IsRead,
                    CoverUrl = book.CoverUrl,
                    DateRead = book.IsRead ? book.DateRead : null,
                    Rate = book.IsRead ? book.Rate : null,
                    Genre = book.Genre,
                    PublisherName = book.publisher.Name,
                    AuthorName = book.Book_Authors.Select(n => n.Author.FullName).ToList()
                }).FirstOrDefault();
            return _BookWithAuthorsVM;
        }
        public Book UpdateBookById(int bookid,BookVM book)
        {
            var _book=_Context.Books.FirstOrDefault(n => n.Id == bookid);
            if (_book!=null)
            {
                _book.Title = book.Title;
                _book.Description = book.Description;
                _book.IsRead = book.IsRead;
                _book.CoverUrl = book.CoverUrl;
                _book.DateRead = book.IsRead ? book.DateRead : null;
                _book.Rate = book.IsRead ? book.Rate : null;
                _book.Genre = book.Genre;
                _book.DateAdded = DateTime.Now;
                _Context.SaveChanges();
            }
            return _book;
        }
        public void DeleteBookById(int bookid)
        {
            var _book = _Context.Books.FirstOrDefault(n => n.Id == bookid);
            if(_book!=null)
            {
                _Context.Books.Remove(_book);
                _Context.SaveChanges();
            }
        }
    }
}
