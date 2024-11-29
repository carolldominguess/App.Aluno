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

        /// <summary>
        /// Adiciona uma nova relação entre Aluno e Turma.
        /// </summary>
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

        /// <summary>
        /// Retorna todas as relações de Aluno e Turma.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAllAlunosTurmas()
        {
            var alunosTurmas = await _alunoTurmaService.GetAllAlunosTurmasAsync();
            return Ok(alunosTurmas);
        }

        /// <summary>
        /// Retorna todos os Alunos relacionados a uma Turma específica.
        /// </summary>
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

        /// <summary>
        /// Retorna todas as Turmas relacionadas a um Aluno específico.
        /// </summary>
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

        /// <summary>
        /// Inativa uma relação entre Aluno e Turma.
        /// </summary>
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
    }
}