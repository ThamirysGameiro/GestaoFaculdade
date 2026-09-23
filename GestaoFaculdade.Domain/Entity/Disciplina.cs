
namespace GestaoFaculdade.Domain.Entity
{
    public class Disciplina
    {
        public string Codigo { get; }
        public string Nome { get; }
        public int CargaHoraria { get; }
        public Professor Professor { get; set; }

        public Disciplina(string codigo, string nome, int cargaHoraria, Professor professor)
        {
            if (string.IsNullOrWhiteSpace(codigo)) throw new ArgumentException("O código deve ser informado.");
            if (string.IsNullOrWhiteSpace(nome)) throw new ArgumentException("O nome deve ser informado.");
            if (cargaHoraria <= 0) throw new ArgumentException("A carga horária deve ser maior que zero.");

            Codigo = codigo;
            Nome = nome;
            CargaHoraria = cargaHoraria;
            Professor = professor ?? throw new ArgumentException("O professor deve ser informado");
        }

        public string ExibirDados()
        {
            return $"{Codigo} - {Nome} | {CargaHoraria}h | Professor: {Professor.Nome}" + Environment.NewLine;
        }
    }
}
