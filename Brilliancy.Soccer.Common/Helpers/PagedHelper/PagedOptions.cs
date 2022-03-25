using System;
using System.Collections.Generic;
using System.Text;

namespace Brilliancy.Soccer.Common.Helpers.PagedHelper
{
    public class PagedOptions
    {
        public PagedOptions()
        {
            Page = 1;
            PerPage = 5;
            SortAsc = true;
        }

        public int CurrentEntries { get; set; }

        public int Page { get; set; }

        public int PerPage { get; set; }

        public bool ShowAll { get; set; }

        public bool SortOnly { get; set; }

        public bool SortAsc { get; set; }

        public string SortBy { get; set; }
    }
}
