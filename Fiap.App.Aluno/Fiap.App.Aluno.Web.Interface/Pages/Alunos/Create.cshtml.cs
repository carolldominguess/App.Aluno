using Fiap.App.Aluno.Web.Interface.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text;
using System.Text.Json;

namespace Fiap.App.Aluno.Web.Interface.Pages.Alunos
{
    public class CreateModel : PageModel
    {
        private readonly IHttpClientFactory _clientFactory;

        public CreateModel(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        [BindProperty]
        public AlunoDto Aluno { get; set; }

        public string Mensagem { get; set; }
        public bool IsSuccess { get; set; }

        public void OnGet() { }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var client = _clientFactory.CreateClient("FiapAppAlunoApi");
            var json = JsonSerializer.Serialize(Aluno);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("Aluno", content);
            Mensagem = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                
                IsSuccess = true;
                return RedirectToPage("/Alunos/Index", new { mensagem = Mensagem, sucesso = true });
            }

            IsSuccess = false;
            return Page();
        }
    }
}