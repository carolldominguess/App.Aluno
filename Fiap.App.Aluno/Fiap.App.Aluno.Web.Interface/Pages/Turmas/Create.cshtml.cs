using Fiap.App.Aluno.Web.Interface.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text;
using System.Text.Json;

namespace Fiap.App.Aluno.Web.Interface.Pages.Turmas
{
    public class CreateModel : PageModel
    {
        private readonly IHttpClientFactory _clientFactory;

        public CreateModel(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        [BindProperty]
        public TurmaDto Turma { get; set; }

        public string Mensagem { get; set; }
        public bool IsSuccess { get; set; }


        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var client = _clientFactory.CreateClient("FiapAppAlunoApi");
            var json = JsonSerializer.Serialize(Turma);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("Turma", content);

            Mensagem = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {                
                IsSuccess = true;
                return RedirectToPage("/Turmas/Index", new { mensagem = "Turma salva com sucesso!", sucesso = true });
            }

            return Page();
        }
    }
}