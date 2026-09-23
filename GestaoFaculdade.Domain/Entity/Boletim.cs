
namespace GestaoFaculdade.Domain.Entity
{
    public class Boletim
    {
        List<NotaDisciplina> Notas { get; } = new List<NotaDisciplina>();

        public void LancarNota(Curso curso, Disciplina disciplina, double nota)
        {
            if (!curso.Disciplinas.Any(d => d.Codigo.Trim().ToLower() == disciplina.Codigo.ToLower()))
                throw new InvalidOperationException("A disciplina não pertence ao curso");

            var notaExistente = Notas.FirstOrDefault(n => n.Disciplina.Codigo.Trim().ToLower()
            == disciplina.Codigo.ToLower());

            if (notaExistente is null)
                Notas.Add(new NotaDisciplina(disciplina, nota));
            else
                notaExistente.AtualizarNota(nota);
        }

        public string Exibir(Aluno aluno, Curso curso)
        {
            var dados = "=== Boletim ===" + Environment.NewLine +
            $"Aluno: {aluno.Nome}" + Environment.NewLine +
            $"Matricula: {aluno.Matricula}" + Environment.NewLine +
            $"Curso: {curso.Nome}" + Environment.NewLine +
            $"Tipo: {(curso.TipoCurso == TipoCurso.Graduacao ? "Graduação" : "Pós-Graduação")}" + Environment.NewLine +
            Environment.NewLine;

            if(Notas.Count == 0)
            {
                return "Nenhuma nota lançada";                
            }

            foreach (var nota in Notas)
            {
                dados += $"Disciplina: {nota.Disciplina.Nome}" + Environment.NewLine +
                $"Nota: {nota.Nota:0.0}" + Environment.NewLine +
                $"Situação: {nota.ObterSituacao(curso)}" + Environment.NewLine +
                Environment.NewLine;
            }

            return dados;
        }
    }
}
