using Fiap.App.Aluno.Application.Interfaces;
using Fiap.App.Aluno.Application.Model;
using Fiap.App.Aluno.Application.Utils;
using Fiap.App.Aluno.Domain.Entidades;
using Fiap.App.Aluno.Domain.Interfaces;

public class AlunoTurmaService : IAlunoTurmaService
{
    private readonly IAlunoTurmaRepository _alunoTurmaRepository;
    private readonly IAlunoRepository _alunoRepository;
    private readonly ITurmaRepository _turmaRepository;

    public AlunoTurmaService(
        IAlunoTurmaRepository alunoTurmaRepository,
        IAlunoRepository alunoRepository,
        ITurmaRepository turmaRepository)
    {
        _alunoTurmaRepository = alunoTurmaRepository;
        _alunoRepository = alunoRepository;
        _turmaRepository = turmaRepository;
    }

    public async Task<ResultadoOperacao> AddAlunoTurmaAsync(AlunoTurmaDto alunoTurmaDto)
    {
        // Verifica se o aluno existe
        var aluno = await _alunoRepository.GetAlunoByIdAsync(alunoTurmaDto.AlunoId);
        if (aluno == null)
        {
            return new ResultadoOperacao(false, "Aluno não encontrado.");
        }

        // Verifica se a turma existe
        var turma = await _turmaRepository.GetTurmaByIdAsync(alunoTurmaDto.TurmaId);
        if (turma == null)
        {
            return new ResultadoOperacao(false, "Turma não encontrada.");
        }

        // Verifica se a relação já existe
        var alunoTurmaExistente = await _alunoTurmaRepository.GetAlunoTurmaByAlunoIdAndTurmaIdAsync(alunoTurmaDto.AlunoId, alunoTurmaDto.TurmaId);
        if (alunoTurmaExistente != null)
        {
            return new ResultadoOperacao(false, "Essa relação já existe.");
        }

        // Cria a nova relação
        var alunoTurma = new AlunoTurma
        {
            AlunoId = alunoTurmaDto.AlunoId,
            TurmaId = alunoTurmaDto.TurmaId,
            DataCriacao = DateTime.Now,
            Ativo = true
        };

        await _alunoTurmaRepository.AddAlunoTurmaAsync(alunoTurma);
        return new ResultadoOperacao(true, "Relação criada com sucesso.");
    }

    public async Task<IEnumerable<AlunoTurmaDto>> GetAllAlunosTurmasAsync()
    {
        var alunosTurmas = await _alunoTurmaRepository.GetAllAlunosTurmasAsync();
        return alunosTurmas.Select(at => new AlunoTurmaDto
        {
            AlunoId = at.AlunoId,
            TurmaId = at.TurmaId
        });
    }

    public async Task<IEnumerable<AlunoDto>> GetAlunosByTurmaIdAsync(Guid turmaId)
    {
        var alunosTurmas = await _alunoTurmaRepository.GetAlunoTurmasByTurmaIdAsync(turmaId);

        if (!alunosTurmas.Any())
        {
            throw new KeyNotFoundException("Nenhum aluno encontrado para essa turma.");
        }

        var alunos = new List<AlunoDto>();
        foreach (var alunoTurma in alunosTurmas)
        {
            var aluno = await _alunoRepository.GetAlunoByIdAsync(alunoTurma.AlunoId);
            if (aluno != null)
            {
                alunos.Add(new AlunoDto
                {
                    Id = aluno.Id,
                    Nome = aluno.Nome,
                    Usuario = aluno.Usuario,
                    Ativo = aluno.Ativo,
                    DataCriacao = aluno.DataCriacao,
                    DataModificacao = aluno.DataModificacao
                });
            }
        }

        return alunos;
    }

    public async Task<IEnumerable<TurmaDto>> GetTurmasByAlunoIdAsync(Guid alunoId)
    {
        var alunosTurmas = await _alunoTurmaRepository.GetAlunoTurmasByAlunoIdAsync(alunoId);

        if (!alunosTurmas.Any())
        {
            throw new KeyNotFoundException("Nenhuma turma encontrada para esse aluno.");
        }

        var turmas = new List<TurmaDto>();
        foreach (var alunoTurma in alunosTurmas)
        {
            var turma = await _turmaRepository.GetTurmaByIdAsync(alunoTurma.TurmaId);
            if (turma != null)
            {
                turmas.Add(new TurmaDto
                {
                    Id = turma.Id,
                    Nome = turma.Nome,
                    Ano = turma.Ano,
                    Ativo = turma.Ativo,
                    DataCriacao = turma.DataCriacao,
                    DataModificacao = turma.DataModificacao
                });
            }
        }

        return turmas;
    }

    public async Task<ResultadoOperacao> DeactivateAlunoTurmaAsync(Guid alunoId, Guid turmaId)
    {
        var alunoTurma = await _alunoTurmaRepository.GetAlunoTurmaByAlunoIdAndTurmaIdAsync(alunoId, turmaId);
        if (alunoTurma == null)
        {
            return new ResultadoOperacao(false, "Relação não encontrada.");
        }

        await _alunoTurmaRepository.DeactivateAsync(alunoId, turmaId);
        return new ResultadoOperacao(true, "Relação inativada com sucesso.");
    }
}