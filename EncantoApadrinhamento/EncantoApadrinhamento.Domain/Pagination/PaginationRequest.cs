namespace EncantoApadrinhamento.Domain.Pagination
{
    public class PaginationRequest
    {
        private string search = string.Empty;

        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string? Search
        {
            get => search;
            set => search = value?.Trim() ?? string.Empty;
        }
        public string SortField { get; set; } = string.Empty;
        public string SortOrder { get; set; } = "ASC";
        public bool SkipPagination { get; set; } = false;
    }
}
