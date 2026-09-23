using GestaoFaculdade.Domain.Entity;
using GestaoFaculdade.Domain.Interface;

List<Pessoa> pessoas = new List<Pessoa>();
List<Curso> cursos = new List<Curso>();
List<Disciplina> disciplinas = new List<Disciplina>();
List<Matricula> matriculas = new List<Matricula>();

string opcao;

do
{
    Console.Clear();
    ExibirMenu();
    Console.WriteLine("Escolha uma opção: ");
    opcao = Console.ReadLine();

    Console.Clear();

    try
    {
        switch (opcao)
        {
            case "1":
                CadastrarCurso(cursos);
                break;
            case "2":
                CadastrarProfessor(pessoas);
                break;
            case "3":
                CadastrarAluno(pessoas);
                break;
            case "4":
                CadastrarDisciplina(pessoas, disciplinas);
                break;
            case "5":
                VincularDisciplinaAoCurso(cursos, disciplinas);
                break;
            case "6":
                MatricularAlunoCurso(pessoas, cursos, matriculas);
                break;
            case "7":
                LancarNota(matriculas);
                break;
            case "8":
                ConsultarPessoas(pessoas, matriculas);
                break;
            case "9":
                ConsultarCursos(cursos, matriculas);
                break;
            case "10":
                ConsultarMatriculas(matriculas);
                break;
            case "11":
                ConsultarBoletim(pessoas, matriculas);
                break;
            case "12":
                EnviarNotificacao(pessoas);
                break;
            case "0":
                Console.WriteLine("Sistema encerrado.");
                break;
            default:
                Console.WriteLine("Opção inválida");
                break;
        }

    }    
    catch (Exception ex)
    {
        Console.WriteLine($"Erro: {ex.Message}");
    }

    if(opcao != "0")
    {
        Console.WriteLine();
        Console.WriteLine("Pressione ENTER para continuar...");
        Console.ReadLine();
    }

} while (opcao != "0");

static void ExibirMenu()
{
    Console.WriteLine("===== GESTÃO DE FACULDADE =====");
    Console.WriteLine("1 - Cadastrar curso");
    Console.WriteLine("2 - Cadastrar professor");
    Console.WriteLine("3 - Cadastrar aluno");
    Console.WriteLine("4 - Cadastrar disciplina");
    Console.WriteLine("5 - Vincular disciplina a um curso");
    Console.WriteLine("6 - Matricular aluno em curso");
    Console.WriteLine("7 - Lançar nota");
    Console.WriteLine("8 - Consultar pessoas");
    Console.WriteLine("9 - Consultar cursos");
    Console.WriteLine("10 - Consultar matrículas");
    Console.WriteLine("11 - Consultar boletim");
    Console.WriteLine("12 - Enviar notificação");
    Console.WriteLine("0 - Sair");
    Console.WriteLine("=============================");
}

static void CadastrarCurso(List<Curso> cursos)
{
    Console.WriteLine("Código: ");
    string codigo = Console.ReadLine();

    if(cursos.Any(x => x.Codigo.Trim().ToLower() == codigo.Trim().ToLower()))
    {
        Console.WriteLine("Já existe um curso com esse código");
        return;
    }

    Console.WriteLine("Nome: ");
    string nome = Console.ReadLine();

    Console.WriteLine("1 - Graduação");
    Console.WriteLine("2 - Pós-Graduação");
    int tipoCurso = LerInteiro("Tipo: ");

    if(tipoCurso is not 1 and not 2)
    {
        Console.WriteLine("Tipo de curso inválido.");
        return;
    }
    cursos.Add(new Curso(codigo, nome, (TipoCurso)tipoCurso));
    Console.WriteLine("Curso cadastrado.");
}

static void CadastrarProfessor(List<Pessoa> pessoas)
{
    int id = LerInteiro("Id: ");
    if (pessoas.Any(p => p.Id == id))
    {
        Console.WriteLine("Id já utilizado.");
        return;
    }

    Console.WriteLine("Nome: ");
    string nome = Console.ReadLine();

    Console.WriteLine("CPF: ");
    string cpf = Console.ReadLine();

    if (pessoas.Any(p => p.CPF.Trim().ToLower().Replace(".", "").Replace("-", "") == cpf.Trim().ToLower().Replace(".", "").Replace("-", "")))
    {
        Console.WriteLine("Id já utilizado.");
        return;
    }

    Console.WriteLine("Email: ");
    string email = Console.ReadLine();

    Console.WriteLine("Registro: ");
    var registro = Console.ReadLine();

    Console.WriteLine("Especialidade: ");
    var especialidade = Console.ReadLine();

    pessoas.Add(new Professor(id, nome, cpf, email, registro, especialidade));
    Console.WriteLine("Professor cadastrado");

}

static void CadastrarAluno(List<Pessoa> pessoas)
{
    int id = LerInteiro("Id: ");
    if (pessoas.Any(p => p.Id == id))
    {
        Console.WriteLine("Id já utilizado.");
        return;
    }

    Console.WriteLine("Nome: ");
    string nome = Console.ReadLine();

    Console.WriteLine("CPF: ");
    string cpf = Console.ReadLine();

    if (pessoas.Any(p => p.CPF.Trim().ToLower().Replace(".", "").Replace("-", "") == cpf.Trim().ToLower().Replace(".", "").Replace("-", "")))
    {
        Console.WriteLine("Id já utilizado.");
        return;
    }

    Console.WriteLine("Email: ");
    string email = Console.ReadLine();

    Console.WriteLine("Matricula: ");
    var matricula = Console.ReadLine();

    if (pessoas.OfType<Aluno>().Any(a => a.Matricula.Trim().ToLower() == matricula.Trim().ToLower()))
    {
        Console.WriteLine("Matricula já cadastrada.");
        return;
    }

    pessoas.Add(new Aluno(id, nome, cpf, email, matricula));
    Console.WriteLine("Aluno cadastrado");

}

static void CadastrarDisciplina(List<Pessoa> pessoas, List<Disciplina> disciplinas)
{
    var professores = pessoas.OfType<Professor>().ToList();

    if (professores.Count == 0)
    {
        Console.WriteLine("Cadastre um professor primeiro.");
        return;
    }

    Console.WriteLine("Código: ");
    var codigo = Console.ReadLine();

    if (disciplinas.Any(d => d.Codigo.Trim().ToLower() == codigo.Trim().ToLower()))
    {
        Console.WriteLine("Código já utilizado.");
        return;
    }

    Console.WriteLine("Nome: ");
    var nome = Console.ReadLine();

    Console.WriteLine("Carga horária: ");
    var cargaHoraria = LerInteiro("Carga horária: ");


    foreach (var p in professores)
    {
        Console.WriteLine($"{p.Registro} - {p.Nome}");
    }

    Console.WriteLine("Registro do professor: ");
    string registro = Console.ReadLine();

    var professor = professores.FirstOrDefault(p => p.Registro == registro);
    if (professor == null)
    {
        Console.WriteLine("Professor não encontrado");
        return;
    }

    disciplinas.Add(new Disciplina(codigo, nome, cargaHoraria, professor));
    Console.WriteLine("Disciplina cadastrada com sucesso");
}

static void VincularDisciplinaAoCurso(List<Curso> cursos, List<Disciplina> disciplinas)
{
    if(cursos.Count == 0 || disciplinas.Count == 0)
    {
        Console.WriteLine("Cadastre cursos e disciplinas");
        return;
    }

    foreach (var c in cursos)
        Console.WriteLine($"{c.Codigo} - {c.Nome}");

    Console.WriteLine("Código do curso: ");
    var codigoCurso = Console.ReadLine();

    var curso = cursos.FirstOrDefault(x => x.Codigo == codigoCurso);
    if (curso == null)
    {
        Console.WriteLine("Curso não encontrado.");
        return;
    }

    foreach (var d in disciplinas)
        Console.WriteLine($"{d.Codigo} - {d.Nome}");

    Console.WriteLine("Código da disciplina: ");
    var codigoDisciplina = Console.ReadLine();

    var disciplina = disciplinas .FirstOrDefault(x => x.Codigo == codigoDisciplina);
    if (disciplina == null)
    {
        Console.WriteLine("Disciplina não encontrada.");
        return;
    }

    Console.WriteLine($"{(curso.AdicionarDisciplina(disciplina) ? 
        "Disciplina vinculada" 
        : "Disciplina já vinculada ao curso")}" );

}

static void MatricularAlunoCurso(List<Pessoa> pessoas, List<Curso> cursos, List<Matricula> matriculas)
{
    var alunos = pessoas.OfType<Aluno>().ToList();

    if(alunos.Count == 0 || cursos.Count == 0)
    {
        Console.WriteLine("Cadastre alunos e cursos primeiro.");
        return;
    }

    foreach (var a in alunos)
    {
        Console.WriteLine($"{a.Matricula} - {a.Nome}");
    }

    Console.WriteLine("Matricula do aluno: ");
    string matricula = Console.ReadLine();

    var aluno = alunos.FirstOrDefault(p => p.Matricula == matricula);
    if (aluno == null)
    {
        Console.WriteLine("Aluno não encontrado");
        return;
    }

    foreach (var c in cursos)
    {
        Console.WriteLine($"{c.Codigo} - {c.Nome}");
    }

    Console.WriteLine("Codigo do curso: ");
    string codigoCurso = Console.ReadLine();

    var curso = cursos.FirstOrDefault(p => p.Codigo == codigoCurso);
    if (curso == null)
    {
        Console.WriteLine("Curso não encontrado");
        return;
    }

    if(matriculas.Any(x => x.Aluno.Matricula == matricula && x.Curso.Codigo == codigoCurso))
    {
        Console.WriteLine("O aluno já está matriculado neste curso");
        return;
    }

    matriculas.Add(new Matricula(aluno, curso));
    Console.WriteLine("Matricula realizada e boletim criado");

}

static void LancarNota(List<Matricula> matriculas)
{
    if(matriculas.Count == 0)
    {
        Console.WriteLine("Nenhuma matricula encontrada.");
        return;
    }

    foreach (var m in matriculas)
        Console.WriteLine($"{m.Aluno.Matricula} - {m.Aluno.Nome} | {m.Curso.Codigo} - {m.Curso.Nome}");

    Console.WriteLine("Matricula do aluno: ");
    string numeroMatricula = Console.ReadLine();

    Console.WriteLine("Código do curso: ");
    string codigoCurso = Console.ReadLine();

    var matricula = matriculas.FirstOrDefault(x => x.Aluno.Matricula == numeroMatricula && x.Curso.Codigo == codigoCurso);

    if(matricula == null)
    {
        Console.WriteLine("Matricula no curso não encontrada.");
        return;
    }

    if(matricula.Curso.Disciplinas.Count == 0)
    {
        Console.WriteLine("O curso não possui disciplinas");
        return;
    }

    foreach (var d in matricula.Curso.Disciplinas)
    {
        Console.WriteLine(d.ExibirDados());
    }

    Console.WriteLine("Código da disciplina: ");
    string codigoDisc = Console.ReadLine();

    var disciplina = matricula.Curso.Disciplinas.FirstOrDefault(x => x.Codigo == codigoDisc);

    if (disciplina == null)
    {
        Console.WriteLine("Disciplina não pertence ao curso");
        return;
    }

    double nota = LerDouble("Nota: ");
    matricula.Boletim.LancarNota(matricula.Curso, disciplina, nota);
    Console.WriteLine("Nota lançada.");

}

static void ConsultarPessoas(List<Pessoa> pessoas, List<Matricula> matriculas)
{
    if(pessoas.Count == 0)
    {
        Console.WriteLine("Nenhuma pessoa encontrada.");
        return;
    }

    foreach(var pessoa in pessoas)
    {
        Console.WriteLine();
        Console.WriteLine($"--- {pessoa.ObterTipo()} ---");
        Console.WriteLine(pessoa.ExibirDados());

        if(pessoa is Aluno aluno)
        {
            var cursos = matriculas.Where(m => m.Aluno == aluno).Select(m => m.Curso).ToList();

            Console.WriteLine("Cursos matriculados: ");
            if(cursos.Count == 0)
                Console.WriteLine("- Nenhum");
            else
                foreach (var curso in cursos)
                    Console.WriteLine($"- {curso.Nome}");
        }
    }
}

static void ConsultarCursos(List<Curso> cursos, List<Matricula> matriculas)
{
    if (cursos.Count == 0) 
    {
        Console.WriteLine("Nenhum curso cadastrado.");
        return;
    }

    foreach (var curso in cursos)
    {
        Console.WriteLine();
        Console.WriteLine(curso.ExibirDados());

        var alunos = matriculas.Where(c => c.Curso == curso).Select(a => a.Aluno).ToList();
        Console.WriteLine("Alunos matriculados: ");
        if(alunos.Count == 0)
            Console.WriteLine("- Nenhum");
        else
        {
            foreach (var aluno in alunos)
            {
                Console.WriteLine($"- {aluno.Nome} ({aluno.Matricula})");
            }
        }
    }
}

static void ConsultarMatriculas(List<Matricula> matriculas)
{
    if(matriculas.Count == 0)
    {
        Console.WriteLine("Nenhuma matricula cadastrada");
        return;
    }

    foreach (var matricula in matriculas)
    {
        Console.WriteLine();
        Console.WriteLine(matricula.ExibirDados());
    }
}

static void ConsultarBoletim(List<Pessoa> pessoas, List<Matricula> matriculas)
{
    var alunos = pessoas.OfType<Aluno>().ToList();
    if(alunos.Count == 0 || matriculas.Count == 0)
    {
        Console.WriteLine("Não há boletins para consultar");
        return;
    }

    foreach(var a in alunos)
        Console.WriteLine($"{a.Matricula} - {a.Nome}");

    Console.WriteLine("Matricula do aluno: ");
    string matricula = Console.ReadLine();

    var matriculasAlunos = matriculas.Where(m => m.Aluno.Matricula.Trim().ToLower() == matricula.Trim().ToLower()).ToList();

    if(matriculasAlunos.Count == 0)
    {
        Console.WriteLine("Aluno sem matricula em curso");
        return;
    }

    foreach (var item in matriculasAlunos)
    {
        Console.WriteLine($"{item.Curso.Codigo} - {item.Curso.Nome}");
    }


    Console.WriteLine("Codigo curso: ");
    var codigo = Console.ReadLine();

    var matriculaCurso = matriculasAlunos.FirstOrDefault(m => m.Curso.Codigo == codigo);

    if(matriculaCurso is null)
    {
        Console.WriteLine("Curso não encontrado para este aluno");
        return;
    }

    Console.WriteLine(matriculaCurso.Boletim.Exibir(matriculaCurso.Aluno, matriculaCurso.Curso));
}

static void EnviarNotificacao(List<Pessoa> pessoas)
{
    if(pessoas.Count == 0)
    {
        Console.WriteLine("Nenhuma pessoa cadastrada");
        return;
    }

    foreach (var pessoa in pessoas)
    {
        Console.WriteLine($"{pessoa.Id} - {pessoa.Nome} - {pessoa.ObterTipo()}");
    }

    int id = LerInteiro("Id da pessoa: ");
    var pessoaSelecionada = pessoas.FirstOrDefault(p => p.Id == id);

    if(pessoaSelecionada is null)
    {
        Console.WriteLine("Pessoa não encontrada");
        return;
    }

    Console.WriteLine("Mensagem: ");
    string mensagem = Console.ReadLine();

    if (pessoaSelecionada is INotificavel notificavel)
        notificavel.EnviarNotificacao(mensagem);
}

static int LerInteiro(string mensagem)
{
    while (true)
    {
        Console.WriteLine(mensagem);
        if(int.TryParse(Console.ReadLine(), out int valor))
            return valor;

        Console.WriteLine("Informe um número inteiro válido");
    }
}

static double LerDouble(string mensagem)
{
    while (true)
    {
        Console.WriteLine(mensagem);
        if (double.TryParse(Console.ReadLine(), out double valor))
            return valor;

        Console.WriteLine("Informe um número válido");
    }
}

