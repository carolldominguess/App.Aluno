using Fiap.App.Aluno.Web.Interface.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text;
using System.Text.Json;

namespace Fiap.App.Aluno.Web.Interface.Pages.Turmas
{
    public class EditModel : PageModel
    {
        private readonly IHttpClientFactory _clientFactory;

        public EditModel(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        [BindProperty]
        public TurmaDto Turma { get; set; }

        public string Mensagem { get; set; }
        public bool IsSuccess { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            var client = _clientFactory.CreateClient("FiapAppAlunoApi");
            var response = await client.GetAsync($"Turma/{id}");           

            if (!response.IsSuccessStatusCode)
            {
                Mensagem = "Turma não encontrada.";
                IsSuccess = false;
                return Page();
            }

            var json = await response.Content.ReadAsStringAsync();
            Turma = JsonSerializer.Deserialize<TurmaDto>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var client = _clientFactory.CreateClient("FiapAppAlunoApi");
            var json = JsonSerializer.Serialize(Turma);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PutAsync($"Turma/{id}", content);
            Mensagem = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {                
                IsSuccess = true;
                return RedirectToPage("/Turmas/Index", new { mensagem = "Turma salva com sucesso!", sucesso = true });
            }
            else
            {
                IsSuccess = false;
            }

            return Page();
        }
    }
}