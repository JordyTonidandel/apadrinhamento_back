namespace EncantoApadrinhamento.Domain.Pagination
{
    public class PaginationResult<T> : PaginationResultBase
    {
        public List<T> Results { get; set; } = new List<T>();


        public PaginationResult()
        {
            Results = [];
        }

        public PaginationResult(int currentPage, int pageSize, int totalItems, List<T> results)
        {
            CurrentPage = currentPage;
            PageSize = pageSize;
            Total = totalItems;
            Results = results;
        }
    }
}
