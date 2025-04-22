using Fiap.App.Aluno.Application.Interfaces;
using Fiap.App.Aluno.Application.Model;
using Microsoft.AspNetCore.Mvc;

namespace Fiap.App.Aluno.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlunoTurmaController : ControllerBase
    {
        private readonly IAlunoTurmaService _alunoTurmaService;

        public AlunoTurmaController(IAlunoTurmaService alunoTurmaService)
        {
            _alunoTurmaService = alunoTurmaService;
        }

        [HttpPost]
        public async Task<IActionResult> AddAlunoTurma([FromBody] AlunoTurmaDto alunoTurmaDto)
        {
            var resultado = await _alunoTurmaService.AddAlunoTurmaAsync(alunoTurmaDto);

            if (!resultado.Sucesso)
            {
                return BadRequest(resultado.Mensagem);
            }

            return Ok(resultado.Mensagem);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAlunosTurmas()
        {
            var alunosTurmas = await _alunoTurmaService.GetAllAlunosTurmasAsync();
            return Ok(alunosTurmas);
        }

        [HttpGet("Turma/{turmaId}")]
        public async Task<IActionResult> GetAlunosByTurmaId(Guid turmaId)
        {
            try
            {
                var alunos = await _alunoTurmaService.GetAlunosByTurmaIdAsync(turmaId);
                return Ok(alunos);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("Aluno/{alunoId}")]
        public async Task<IActionResult> GetTurmasByAlunoId(Guid alunoId)
        {
            try
            {
                var turmas = await _alunoTurmaService.GetTurmasByAlunoIdAsync(alunoId);
                return Ok(turmas);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{alunoId}/{turmaId}")]
        public async Task<IActionResult> DeactivateAlunoTurma(Guid alunoId, Guid turmaId)
        {
            var resultado = await _alunoTurmaService.DeactivateAlunoTurmaAsync(alunoId, turmaId);

            if (!resultado.Sucesso)
            {
                return BadRequest(resultado.Mensagem);
            }

            return Ok(resultado.Mensagem);
        }

        [HttpPut("{alunoId}/{turmaId}")]
        public async Task<IActionResult> UpdateAlunoTurma(Guid alunoId, Guid turmaId, [FromBody] AlunoTurmaDto alunoTurmaDto)
        {
            var resultado = await _alunoTurmaService.UpdateAlunoTurmaAsync(alunoId, turmaId, alunoTurmaDto);

            if (!resultado.Sucesso)
            {
                return BadRequest(resultado.Mensagem);
            }

            return Ok(resultado.Mensagem);
        }

        [HttpGet("{alunoId}/{turmaId}")]
        public async Task<IActionResult> GetAlunoTurma(Guid alunoId, Guid turmaId)
        {
            var alunoTurma = await _alunoTurmaService.GetAlunoTurmaByAlunoIdAndTurmaIdAsync(alunoId, turmaId);
            if (alunoTurma == null)
            {
                return NotFound("Relação Aluno-Turma não encontrada.");
            }

            return Ok(alunoTurma);
        }
    }
}