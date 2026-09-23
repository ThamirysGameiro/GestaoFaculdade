
using GestaoFaculdade.Domain.Interface;

namespace GestaoFaculdade.Domain.Entity
{
    public class Aluno : Pessoa, INotificavel
    {
        public Aluno(int id, string nome, string cpf, string email, string matricula) 
            : base(id, nome, cpf, email)
        {
            if (string.IsNullOrWhiteSpace(matricula))
                throw new ArgumentException("A matricula deve ser informada");

            Matricula = matricula;
        }

        public string Matricula { get; }

        public override string ObterTipo() => "Aluno";

        public override string ExibirDados()
        {
            return base.ExibirDados() + Environment.NewLine +
            $"Matricula: {Matricula}";
        }

        public string EnviarNotificacao(string mensagem)
        {
            return $"Notificação enviada ao aluno: {Nome}: {mensagem}" + Environment.NewLine;
        }
    }
}
