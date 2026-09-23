
namespace GestaoFaculdade.Domain.Entity
{
    public class Curso
    {
        public string Codigo { get; }
        public string Nome { get; }
        public TipoCurso TipoCurso { get; set; }
        public List<Disciplina> Disciplinas { get; } = new List<Disciplina>();

        public Curso(string codigo, string nome, TipoCurso tipoCurso)
        {
            if (string.IsNullOrWhiteSpace(codigo)) throw new ArgumentException("O código deve ser informado.");
            if (string.IsNullOrWhiteSpace(nome)) throw new ArgumentException("O nome deve ser informado.");

            Codigo = codigo;
            Nome = nome;
            TipoCurso = tipoCurso;
        }

        public bool AdicionarDisciplina(Disciplina disciplina)
        {
            if(Disciplinas.Any(x => x.Codigo.Trim().ToLower() == disciplina.Codigo.Trim().ToLower()))
                return false;

            Disciplinas.Add(disciplina);
            return true;
        }

        public double ObterNotaMinimaAprovacao() => TipoCurso == TipoCurso.Graduacao ? 7 : 8;

        public string ExibirDados()
        {
            var dados = $"Curso: {Codigo} - {Nome}" + Environment.NewLine +
            $"Tipo: {(TipoCurso == TipoCurso.Graduacao ? "Graduação" : "Pós-Graduação")}";

            if (Disciplinas.Count == 0)
            {
                return "Disciplinas: nenhuma vinculada.";
                
            }

            dados += "Disciplinas:" + Environment.NewLine;
            foreach (var disciplina in Disciplinas)
            {
                dados += disciplina.ExibirDados();
            }

            return dados;
        }
    }
}
