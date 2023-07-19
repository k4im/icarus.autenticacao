namespace autenticacao.service.Models.ValueObjects
{
    [Keyless]
    public class Endereco
    {
        public string Cidade { get; private set; }
        public string Bairro { get; private set; }
        public string Rua { get; private set; }
        public string Cep { get; private set; }
        public int Numero { get; private set; }


        protected Endereco()
        { }

        public Endereco(string cidade, string bairro, string rua, string cep, int numero)
        {

            Cidade = VerificarCidade(cidade);
            Bairro = VerificarBairro(bairro);
            Rua = VerificarRua(rua);
            Cep = cep;
            Numero = numero;
        }


        string VerificarCidade(string cidade)
        {
            if (string.IsNullOrEmpty(cidade)) throw new CampoVazio("A cidade não pode estar vazia!");
            if (!Regex.IsMatch(cidade, @"^[a-zA-ZzáàâãéèêíïóôõöúçñÁÀÂÃÉÈÍÏÓÔÕÖÚÇÑ ]+$")) throw new CaracterInvalido("A cidade não pode conter caracteres especiais");
            return cidade;
        }

        string VerificarBairro(string bairro)
        {
            if (string.IsNullOrEmpty(bairro)) throw new CampoVazio("O bairro não pode estar vazio!");
            if (!Regex.IsMatch(bairro, @"^[a-zA-ZzáàâãéèêíïóôõöúçñÁÀÂÃÉÈÍÏÓÔÕÖÚÇÑ ]+$")) throw new CaracterInvalido("O bairro não pode conter caracteres especiais");
            return bairro;
        }
        string VerificarCep(string cep)
        {
            if (string.IsNullOrEmpty(cep)) throw new CampoVazio("O cep não pode estar vazio!");
            if (!Regex.IsMatch(cep, @"^[a-zA-Z]+$")) throw new CaracterInvalido("O cep não pode conter caracteres especiais");
            return cep;
        }
        string VerificarRua(string rua)
        {
            if (string.IsNullOrEmpty(rua)) throw new CampoVazio("A rua não pode estar vazia!");
            // if (!Regex.IsMatch(rua, @"$[\\p{L}\\s]+$")) throw new CaracterInvalido("A rua não pode conter caracteres especiais");
            return rua;
        }

        public void atualizarEndereco(Endereco novoEndereco)
        {
            if (novoEndereco.Cidade != this.Cidade) this.Cidade = novoEndereco.Cidade;
            if (novoEndereco.Bairro != this.Bairro) this.Bairro = novoEndereco.Bairro;
            if (novoEndereco.Rua != this.Rua) this.Rua = novoEndereco.Rua;
            if (novoEndereco.Cep != this.Cep) this.Cep = novoEndereco.Cep;
            if (novoEndereco.Numero != this.Numero) this.Numero = novoEndereco.Numero;
        }

    }
}