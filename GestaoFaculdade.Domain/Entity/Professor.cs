using GestaoFaculdade.Domain.Interface;

namespace GestaoFaculdade.Domain.Entity
{
    public class Professor : Pessoa, INotificavel
    {
        public string Registro { get; }
        public string Especialidade { get; }

        public Professor(int id, string nome, string cpf, string email, string registro, string espec) 
            : base(id, nome, cpf, email)
        {
            if (string.IsNullOrWhiteSpace(registro))
                throw new ArgumentException("O registro deve ser informado");

            if (string.IsNullOrWhiteSpace(espec))
                throw new ArgumentException("A especialidade deve ser informada");

            Registro = registro;
            Especialidade = espec;
        }

        public string EnviarNotificacao(string mensagem)
        {
            return "Notificação enviada para o(a) professor(a)" + Environment.NewLine;
        }

        public override string ObterTipo() => "Professor";

        public override string ExibirDados()
        {
            return base.ExibirDados() + Environment.NewLine +
            $"Registro: {Registro}" + Environment.NewLine +
            $"Especialidade: {Especialidade}" + Environment.NewLine;
        }
    }
}
