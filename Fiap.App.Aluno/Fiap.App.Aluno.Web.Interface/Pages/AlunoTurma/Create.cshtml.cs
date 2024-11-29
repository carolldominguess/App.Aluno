using Fiap.App.Aluno.Web.Interface.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using System.Text;

namespace Fiap.App.Aluno.Web.Interface.Pages.AlunoTurma
{
    public class CreateModel : PageModel
    {
        private readonly IHttpClientFactory _clientFactory;

        public CreateModel(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        [BindProperty]
        public AlunoTurmaDto AlunoTurma { get; set; }

        public List<AlunoDto> Alunos { get; set; } = new List<AlunoDto>();
        public List<TurmaDto> Turmas { get; set; } = new List<TurmaDto>();
        public string Mensagem { get; set; }
        public bool IsSuccess { get; set; }

        public async Task OnGetAsync()
        {
            var client = _clientFactory.CreateClient("FiapAppAlunoApi");

            var alunosResponse = await client.GetAsync("Aluno");
            if (alunosResponse.IsSuccessStatusCode)
            {
                var alunosJson = await alunosResponse.Content.ReadAsStringAsync();
                Alunos = JsonSerializer.Deserialize<List<AlunoDto>>(alunosJson, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }

            var turmasResponse = await client.GetAsync("Turma");
            if (turmasResponse.IsSuccessStatusCode)
            {
                var turmasJson = await turmasResponse.Content.ReadAsStringAsync();
                Turmas = JsonSerializer.Deserialize<List<TurmaDto>>(turmasJson, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                await OnGetAsync();
                return Page();
            }

            var client = _clientFactory.CreateClient("FiapAppAlunoApi");
            var json = JsonSerializer.Serialize(AlunoTurma);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("AlunoTurma", content);
            Mensagem = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("./Index");
            }

            IsSuccess = false;

            await OnGetAsync();
            return Page();
        }
    }
}