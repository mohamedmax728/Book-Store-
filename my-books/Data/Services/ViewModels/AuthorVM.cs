using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace my_books.Data.Services.ViewModels
{
    public class AuthorVM
    {
        public string FullName { get; set; }
    }
    public class AuthorsWithBookVM
    {
        public string FullName { get; set; }
        public List<string> BookTitles { get; set; }
    }
}
