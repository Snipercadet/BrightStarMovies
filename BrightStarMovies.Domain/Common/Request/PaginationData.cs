using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrightStarMovies.Domain.Common.Request
{
    public class PaginationData
    {
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 20;
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public string SearchTerm { get; set; }

        //// Sorting
        //public ESortOrder SortOrder { get; set; }
        //public string SortBy { get; set; }
    }
}
