
namespace GestaoFaculdade.Domain.Entity
{
    public abstract class Pessoa
    {
        public int Id { get; }
        public string Nome { get; private set; }
        public string CPF { get; }
        public string Email { get; private set; }

        protected Pessoa(int id, string nome, string cpf, string email)
        {
            ValidarCampos(id, nome, cpf, email);
            Id = id;
            Nome = nome;
            CPF = cpf;
            Email = email;
        }

        private void ValidarCampos(int id, string nome, string cpf, string email)
        {
            if (id <= 0) throw new ArgumentException("O id deve ser maior que zero");
            if (string.IsNullOrWhiteSpace(nome)) throw new ArgumentException("O nome deve ser preenchido");
            if (string.IsNullOrWhiteSpace(cpf)) throw new ArgumentException("O CPF deve ser preenchido");
            if (string.IsNullOrWhiteSpace(email)) throw new ArgumentException("O email deve ser preenchido");
        }

        public virtual string ExibirDados()
        {
            return $"Id: {Id}" + Environment.NewLine +
            $"Nome: {Nome}" + Environment.NewLine +
            $"CPF: {CPF}" + Environment.NewLine +
            $"E-mail: {Email}";
        }

        public abstract string ObterTipo();
    }
}
