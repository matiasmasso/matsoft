namespace DTO
{
    public class DTODatagridviewCell
    {
        public int FirstDisplayedScrollingRowIndex { get; set; }
        public int RowIndex { get; set; }
        public int ColumnIndex { get; set; }
        public int SortedColumn { get; set; }
        public SortOrders SortOrder { get; set; }

        public enum SortOrders
        {
            None,
            Ascending,
            Descending
        }
    }
}
