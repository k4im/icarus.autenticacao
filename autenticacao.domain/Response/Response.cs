namespace autenticacao.domain.Response
{
    public class Response<T>
    {
        public Response(List<T> data, int paginaAtual, int totalDePaginas, int totalDeItens)
        {
            Data = data;
            PaginaAtual = paginaAtual;
            TotalDePaginas = totalDePaginas;
            TotalDeItens = totalDeItens;
        }

        public List<T> Data { get; } = new List<T>();
        public int PaginaAtual { get; }
        public int TotalDePaginas { get; }
        public int TotalDeItens { get; }
    }
}