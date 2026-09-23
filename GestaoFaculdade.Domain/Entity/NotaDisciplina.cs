
namespace GestaoFaculdade.Domain.Entity
{
    public class NotaDisciplina
    {
        public Disciplina Disciplina { get; }
        public double Nota { get; private set; }

        public NotaDisciplina(Disciplina disciplina, double nota)
        {
            Disciplina = disciplina ?? throw new ArgumentException("A disciplina precisa ser informada");
            AtualizarNota(nota);
        }

        public void AtualizarNota(double nota)
        {
            if (nota <= 0 || nota > 10) 
            { 
                throw new ArgumentException("A nota deve estar entre 0 e 10"); 
            }

            Nota = nota;
        }

        public string ObterSituacao(Curso curso)
        {
            return Nota >= curso.ObterNotaMinimaAprovacao() ? "Aprovado" : "Reprovado";
        }
    }
}
