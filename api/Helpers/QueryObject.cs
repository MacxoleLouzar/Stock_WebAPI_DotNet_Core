using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Helpers
{
    public class QueryObject
    {
        //Filter
        public string? Symbol { get; set; } = null;
        public string? CompanyName { get; set; } = null;

        //Sorting
        public string? SortBy { get; set; } = null;
       
        public bool IsSorAscending { get; set; } = true;
        public bool IsSortDescending { get; set; } = false;
      
      //Pagination
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}