using System;

namespace DTO
{
    public class DTOPagination
    {
        public int ItemsCount { get; set; }
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
        public Guid Guid { get; set; }

        public DTOPagination(int iItemsCount, int iPageSize, int iPageIndex, Guid oGuid = default(Guid)) : base()
        {
            ItemsCount = iItemsCount;
            PageSize = iPageSize;
            PageIndex = iPageIndex;
            Guid = oGuid;
        }
    }
}
