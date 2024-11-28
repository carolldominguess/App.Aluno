using Fiap.App.Aluno.Application.Interfaces;
using Fiap.App.Aluno.Application.Model;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class TurmaController : ControllerBase
{
    private readonly ITurmaService _turmaService;

    public TurmaController(ITurmaService turmaService)
    {
        _turmaService = turmaService;
    }

    // Adiciona uma nova turma
    [HttpPost]
    public async Task<IActionResult> AddTurma([FromBody] TurmaDto turmaDto)
    {
        var resultado = await _turmaService.AddTurmaAsync(turmaDto);
        if (!resultado.Sucesso)
        {
            return BadRequest(resultado.Mensagem);
        }
        return Ok(resultado.Mensagem);
    }

    // Obtém todas as turmas
    [HttpGet]
    public async Task<IActionResult> GetAllTurmas()
    {
        var turmas = await _turmaService.GetAllTurmasAsync();
        return Ok(turmas);
    }

    // Obtém uma turma específica pelo ID
    [HttpGet("{id}")]
    public async Task<IActionResult> GetTurmaById(Guid id)
    {
        var turma = await _turmaService.GetTurmaByIdAsync(id);
        if (turma == null)
        {
            return NotFound("Turma não encontrada.");
        }
        return Ok(turma);
    }

    // Atualiza uma turma existente
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTurma(Guid id, [FromBody] TurmaDto turmaDto)
    {
        var resultado = await _turmaService.UpdateTurmaAsync(id, turmaDto);
        if (!resultado.Sucesso)
        {
            return BadRequest(resultado.Mensagem);
        }
        return Ok(resultado.Mensagem);
    }

    // Desativa uma turma
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeactivateTurma(Guid id)
    {
        var resultado = await _turmaService.DeactivateTurmaAsync(id);
        if (!resultado.Sucesso)
        {
            return BadRequest(resultado.Mensagem);
        }
        return Ok(resultado.Mensagem);
    }
}