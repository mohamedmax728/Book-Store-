using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace my_books.Exceptions
{
    public class PublisherNameException:Exception
    {
        public string PublisherName { get; set; }
        public PublisherNameException()
        {

        }
        public PublisherNameException(string message):base(message)
        {

        }
        public PublisherNameException(string message, Exception Inner) : base(message, Inner)
        {

        }
        public PublisherNameException(string message, string publishername) : this(message)
        {
            this.PublisherName = publishername;
        }
    }
}
