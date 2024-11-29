using Fiap.App.Aluno.Web.Interface.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace Fiap.App.Aluno.Web.Interface.Pages.AlunoTurma
{
    public class IndexModel : PageModel
    {
        private readonly IHttpClientFactory _clientFactory;

        public IndexModel(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public List<AlunoTurmaViewModel> AlunoTurmas { get; set; } = new List<AlunoTurmaViewModel>();
        public string Mensagem { get; set; }
        public bool IsSuccess { get; set; }

        public async Task OnGetAsync()
        {
            var client = _clientFactory.CreateClient("FiapAppAlunoApi");

            var alunoTurmaResponse = await client.GetAsync("AlunoTurma");
            if (!alunoTurmaResponse.IsSuccessStatusCode) return;

            var alunoTurmaJson = await alunoTurmaResponse.Content.ReadAsStringAsync();
            var alunoTurmas = JsonSerializer.Deserialize<List<AlunoTurmaDto>>(alunoTurmaJson, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            var alunoResponse = await client.GetAsync("Aluno");
            if (!alunoResponse.IsSuccessStatusCode) return;

            var alunoJson = await alunoResponse.Content.ReadAsStringAsync();
            var alunos = JsonSerializer.Deserialize<List<AlunoDto>>(alunoJson, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            var turmaResponse = await client.GetAsync("Turma");
            if (!turmaResponse.IsSuccessStatusCode) return;

            var turmaJson = await turmaResponse.Content.ReadAsStringAsync();
            var turmas = JsonSerializer.Deserialize<List<TurmaDto>>(turmaJson, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            AlunoTurmas = alunoTurmas.Select(relacao => new AlunoTurmaViewModel
            {
                AlunoId = relacao.AlunoId.Value,
                TurmaId = relacao.TurmaId.Value,
                AlunoNome = alunos.FirstOrDefault(a => a.Id == relacao.AlunoId)?.Nome ?? "Aluno Não Encontrado",
                TurmaNome = turmas.FirstOrDefault(t => t.Id == relacao.TurmaId)?.Nome ?? "Turma Não Encontrada",
                Ativo = relacao.Ativo
            }).ToList();
        }

        public async Task<IActionResult> OnPostInactivateAsync(Guid alunoId, Guid turmaId)
        {
            var client = _clientFactory.CreateClient("FiapAppAlunoApi");
            var response = await client.DeleteAsync($"AlunoTurma/{alunoId}/{turmaId}");

            if (response.IsSuccessStatusCode)
            {
                Mensagem = "Relação inativada com sucesso.";
                IsSuccess = true;
            }
            else
            {
                Mensagem = "Erro ao inativar a relação.";
                IsSuccess = false;
            }

            await OnGetAsync();
            return Page();
        }
    }

    public class AlunoTurmaViewModel
    {
        public Guid AlunoId { get; set; }
        public Guid TurmaId { get; set; }
        public string AlunoNome { get; set; }
        public string TurmaNome { get; set; }
        public bool Ativo { get; set; }
    }
}