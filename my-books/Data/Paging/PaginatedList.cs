using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace my_books.Data.Paging
{
    public class PaginatedList<T>:List<T>
    {
        public int PageIndex { get;private set; }
        public int TotalPages { get;private set; }
        public PaginatedList(List<T> list,int count,int Index,int Pagesize)
        {
            PageIndex = Index;
            TotalPages = (int)Math.Ceiling(count / (double)Pagesize);
            this.AddRange(list);
        }
        public bool HasPrevPage(int index) => index > 1;
        public bool HasNextPage(int index) => index < TotalPages;
        public static PaginatedList<T> Create(IQueryable<T> source,int index,int pagesize)
        {
            var count = source.Count();
            var list = source.Skip(( index-1) * pagesize).Take(pagesize).ToList();
            return new PaginatedList<T>(list, count, index, pagesize);
        }
    }
}
