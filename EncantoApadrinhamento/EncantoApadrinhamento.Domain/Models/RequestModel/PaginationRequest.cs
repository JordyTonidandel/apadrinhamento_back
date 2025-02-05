namespace EncantoApadrinhamento.Domain.RequestModel
{
    public class PaginationRequest
    {
        private string pesquisa = string.Empty;

        public int Pagina { get; set; } = 1;
        public int ItensPorPagina { get; set; } = 10;
        public string? Pesquisa { get => pesquisa; set => pesquisa = value?.Trim()!; }
        public string CampoOrdenacao { get; set; } = string.Empty;
        public string TipoOrdenacao { get; set; } = "ASC";
        public bool IgnorarPaginacao { get; set; } = false;
    }
}
