
namespace GestaoFaculdade.Domain.Entity
{
    public class Matricula
    {
        public Aluno Aluno { get; }
        public Curso Curso { get; }
        public Boletim Boletim { get; }

        public Matricula(Aluno aluno, Curso curso)
        {
            Aluno = aluno ?? throw new ArgumentNullException("O aluno não foi informado");
            Curso = curso ?? throw new ArgumentNullException("O curso não foi informado");
            Boletim = new Boletim();
        }

        public string ExibirDados()
        {
            return $"Aluno: {Aluno.Nome}" + Environment.NewLine +
            $"Matricula: {Aluno.Matricula}" + Environment.NewLine +
            $"Curso: {Curso.Nome}" + Environment.NewLine +
            $"Tipo: {(Curso.TipoCurso == TipoCurso.Graduacao ? "Graduação" : "Pós-Graduação")}" + Environment.NewLine;
        }
    }
}
