using System;
using System.Collections.Generic;
using System.Text;

namespace Brilliancy.Soccer.Common.Helpers.PagedHelper
{
    public class PagedResult<T>
    {
        public List<T> Data { get; }

        public PagedInfo Details { get; }

        public PagedResult(IEnumerable<T> items, int pageNo, int pageSize, long totalRecordCount)
        {
            Data = new List<T>(items);
            Details = new PagedInfo
            {
                PageNo = pageNo,
                PageSize = pageSize,
                TotalEntries = totalRecordCount,
                PageCount = totalRecordCount > 0
                    ? (int)Math.Ceiling(totalRecordCount / (double)pageSize)
                    : 0
            };
        }
    }
}
