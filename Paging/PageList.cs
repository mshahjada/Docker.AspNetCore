using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paging
{
    public class PageList<T>: IPage
    {
        public IList<T> Data { get; set; }

        public PageList(int page, int pageSize)
        {
            Data = new List<T>();
            CurrentPage = page;
            PageSize = pageSize;
        }
    }


    public static class PageListExtension
    {
        public static PageList<T> GetPaged<T>(this IQueryable<T> query, int page, int pageSize)
        {
            return GetPagingData<T>(query, page, pageSize);
        }


        public static async Task<PageList<T>> GetPagedAsync<T>(this IQueryable<T> query, int page, int pageSize)
        {
            return await Task.Run(() => GetPagingData<T>(query, page, pageSize));
        }


        private static PageList<T> GetPagingData<T>(IQueryable<T> query, int page, int pageSize)
        {
            var obj = new PageList<T>(page, pageSize);

            obj.RowCount = query.Count();

            obj.PageCount = (obj.RowCount / obj.PageSize) + ((obj.RowCount % obj.PageSize) == 0 ? 0 : 1);

            var skip = (page - 1) * pageSize;

            obj.Data = query.Skip(skip).Take(pageSize).ToList();

            return obj;
        }

    }

}
