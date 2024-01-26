using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrightStarMovies.Domain.Common.Response
{
    public class PaginatedResponse<T>
    {
        public List<T> Items { get; set; }

        public int TotalRecords { get; set; }

        public int CurrentPage { get; set; }

        public int PageSize { get; set; }

    }
}
