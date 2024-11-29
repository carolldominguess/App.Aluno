using Fiap.App.Aluno.Web.Interface.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace Fiap.App.Aluno.Web.Interface.Pages.Turmas
{
    public class DetailsModel : PageModel
    {
        private readonly IHttpClientFactory _clientFactory;

        public DetailsModel(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public TurmaDto Turma { get; set; }
        public List<AlunoDto> Alunos { get; set; } = new();
        public string Mensagem { get; set; }
        public bool IsSuccess { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            var client = _clientFactory.CreateClient("FiapAppAlunoApi");

            var turmaResponse = await client.GetAsync($"Turma/{id}");
            if (turmaResponse.IsSuccessStatusCode)
            {
                var turmaJson = await turmaResponse.Content.ReadAsStringAsync();
                Turma = JsonSerializer.Deserialize<TurmaDto>(turmaJson, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }
            else
            {
                Mensagem = "Erro ao carregar os dados da turma.";
                IsSuccess = false;
                return Page();
            }

            var alunosResponse = await client.GetAsync($"AlunoTurma/Turma/{id}");
            if (alunosResponse.IsSuccessStatusCode)
            {
                var alunosJson = await alunosResponse.Content.ReadAsStringAsync();
                Alunos = JsonSerializer.Deserialize<List<AlunoDto>>(alunosJson, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }
            else
            {
                Mensagem = "Erro ao carregar a lista de alunos.";
                IsSuccess = false;
            }

            return Page();
        }
    }
}