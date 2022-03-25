using System;
using System.Collections.Generic;
using System.Text;

namespace Brilliancy.Soccer.Common.Helpers.PagedHelper
{
    public class PagedInfo
    {
        public int PageNo { get; set; }

        public int PageSize { get; set; }

        public int PageCount { get; set; }

        public long TotalEntries { get; set; }
    }
}
