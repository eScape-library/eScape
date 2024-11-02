using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eScape.UseCase
{
    public class PagingOption
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public string? OrderBy { get; set; }
        public string? WhereClause { get; set; }
    }
}
