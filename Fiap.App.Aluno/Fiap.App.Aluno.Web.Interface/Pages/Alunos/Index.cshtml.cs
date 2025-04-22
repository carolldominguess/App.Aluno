using Fiap.App.Aluno.Web.Interface.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace Fiap.App.Aluno.Web.Interface.Pages.Alunos
{
    public class IndexModel : PageModel
    {
        private readonly IHttpClientFactory _clientFactory;

        public IndexModel(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public List<AlunoDto> Alunos { get; set; } = new List<AlunoDto>();
        public string Mensagem { get; set; }
        public bool IsSuccess { get; set; }

        public async Task OnGetAsync(string mensagem = null, bool sucesso = false)
        {
            Mensagem = mensagem;
            IsSuccess = sucesso;

            var client = _clientFactory.CreateClient("FiapAppAlunoApi");
            var response = await client.GetAsync("Aluno");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                Alunos = JsonSerializer.Deserialize<List<AlunoDto>>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }
        }

        public async Task<IActionResult> OnPostDeactivateAsync(Guid id)
        {
            var client = _clientFactory.CreateClient("FiapAppAlunoApi");
            var response = await client.DeleteAsync($"Aluno/{id}");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("/Alunos/Index", new { mensagem = "Aluno desativado com sucesso!", sucesso = true });
            }

            Mensagem = "Erro ao desativar o aluno.";
            IsSuccess = false;

            await OnGetAsync();
            return Page();
        }
    }
}