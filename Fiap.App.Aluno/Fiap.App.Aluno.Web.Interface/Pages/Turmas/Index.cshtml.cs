using Fiap.App.Aluno.Web.Interface.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace Fiap.App.Aluno.Web.Interface.Pages.Turmas
{
    public class IndexModel : PageModel
    {
        private readonly IHttpClientFactory _clientFactory;

        public IndexModel(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public List<TurmaDto> Turmas { get; set; } = new List<TurmaDto>();
        public string Mensagem { get; set; }
        public bool IsSuccess { get; set; }


        public async Task OnGetAsync(string mensagem = null, bool sucesso = false)
        {
            Mensagem = mensagem;
            IsSuccess = sucesso;

            var client = _clientFactory.CreateClient("FiapAppAlunoApi");
            var response = await client.GetAsync("Turma");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                Turmas = JsonSerializer.Deserialize<List<TurmaDto>>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }
        }

        public async Task<IActionResult> OnPostDeactivateAsync(Guid id)
        {
            var client = _clientFactory.CreateClient("FiapAppAlunoApi");
            var response = await client.DeleteAsync($"Turma/{id}");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("/Turmas/Index", new { mensagem = "Turma inativada com sucesso!", sucesso = true });
            }

            Mensagem = "Erro ao inativar a turma.";
            IsSuccess = false;

            await OnGetAsync();
            return Page();
        }
    }
}