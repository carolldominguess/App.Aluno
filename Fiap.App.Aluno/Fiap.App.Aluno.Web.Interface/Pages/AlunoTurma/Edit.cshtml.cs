using Fiap.App.Aluno.Web.Interface.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text;
using System.Text.Json;

namespace Fiap.App.Aluno.Web.Interface.Pages.AlunoTurma
{
    public class EditModel : PageModel
    {
        private readonly IHttpClientFactory _clientFactory;

        public EditModel(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        [BindProperty]
        public AlunoTurmaViewModel AlunoTurma { get; set; } = new();

        public List<AlunoDto> Alunos { get; set; } = new();
        public List<TurmaDto> Turmas { get; set; } = new();
        public string Mensagem { get; set; }
        public bool IsSuccess { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid alunoId, Guid turmaId)
        {
            var client = _clientFactory.CreateClient("FiapAppAlunoApi");

            var alunoTurmaResponse = await client.GetAsync($"AlunoTurma/{alunoId}/{turmaId}");
            if (alunoTurmaResponse.IsSuccessStatusCode)
            {
                var alunoTurmaJson = await alunoTurmaResponse.Content.ReadAsStringAsync();
                AlunoTurma = JsonSerializer.Deserialize<AlunoTurmaViewModel>(alunoTurmaJson, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }
            else
            {
                Mensagem = "Erro ao carregar os dados da relação.";
                IsSuccess = false;
                return Page();
            }

            var alunoResponse = await client.GetAsync("Aluno");
            if (alunoResponse.IsSuccessStatusCode)
            {
                var alunoJson = await alunoResponse.Content.ReadAsStringAsync();
                Alunos = JsonSerializer.Deserialize<List<AlunoDto>>(alunoJson, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }

            var turmaResponse = await client.GetAsync("Turma");
            if (turmaResponse.IsSuccessStatusCode)
            {
                var turmaJson = await turmaResponse.Content.ReadAsStringAsync();
                Turmas = JsonSerializer.Deserialize<List<TurmaDto>>(turmaJson, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid alunoId, Guid turmaId)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var client = _clientFactory.CreateClient("FiapAppAlunoApi");
            var json = JsonSerializer.Serialize(new AlunoTurmaDto
            {
                AlunoId = AlunoTurma.AlunoId,
                TurmaId = AlunoTurma.TurmaId
            });
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PutAsync($"AlunoTurma/{alunoId}/{turmaId}", content);
            Mensagem = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                IsSuccess = true;
            }
            else
            {
                IsSuccess = false;
            }

            return Page();
        }

        public class AlunoTurmaViewModel
        {
            public Guid AlunoId { get; set; }
            public Guid TurmaId { get; set; }
            public string AlunoNome { get; set; } = "Aluno Não Encontrado";
            public string TurmaNome { get; set; } = "Turma Não Encontrada";
            public bool Ativo { get; set; }
        }
    }
}
