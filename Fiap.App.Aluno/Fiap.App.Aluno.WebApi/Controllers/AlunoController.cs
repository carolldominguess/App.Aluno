using Fiap.App.Aluno.Application.Interfaces;
using Fiap.App.Aluno.Application.Model;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class AlunoController : ControllerBase
{
    private readonly IAlunoService _alunoService;

    public AlunoController(IAlunoService alunoService)
    {
        _alunoService = alunoService;
    }

    [HttpPost]
    public async Task<IActionResult> AddAluno([FromBody] AlunoComTurmaDto alunoComTurmaDto)    
    {
        var resultado = await _alunoService.AddAlunoAsync(alunoComTurmaDto.Aluno, alunoComTurmaDto.AlunoTurma);
        if (!resultado.Sucesso)
        {
            return BadRequest(resultado.Mensagem);
        }

        return Ok(resultado.Mensagem);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAluno(Guid id, [FromBody] AlunoDto alunoDto)
    {
        var resultado = await _alunoService.UpdateAlunoAsync(id, alunoDto);
        if (!resultado.Sucesso)
        {
            return BadRequest(resultado.Mensagem);
        }

        return Ok(resultado.Mensagem);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAlunos()
    {
        try
        {
            var alunos = await _alunoService.GetAllAlunosAsync();
            return Ok(alunos);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAlunoById(Guid id)
    {
        try
        {
            var aluno = await _alunoService.GetAlunoByIdAsync(id);
            return Ok(aluno);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeactivateAluno(Guid id)
    {
        var resultado = await _alunoService.DeactivateAlunoAsync(id);
        if (!resultado.Sucesso)
        {
            return BadRequest(resultado.Mensagem);
        }

        return Ok(resultado.Mensagem);
    }
}