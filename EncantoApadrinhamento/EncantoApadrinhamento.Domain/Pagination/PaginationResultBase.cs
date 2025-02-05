namespace EncantoApadrinhamento.Domain.Pagination
{
    public class PaginationResultBase
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int Total { get; set; }
        public int TotalPages => PageSize <= 0 ? 1 : (int)Math.Ceiling((double)Total / PageSize);
        public int FirstItemOnPage => (CurrentPage - 1) * PageSize + 1;
        public int LastItemOnPage => Math.Min(CurrentPage * PageSize, Total);
        public int TotalItemsOnCurrentPage => LastItemOnPage - FirstItemOnPage + 1;

    }
}
