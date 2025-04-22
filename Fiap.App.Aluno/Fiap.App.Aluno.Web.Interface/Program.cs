var builder = WebApplication.CreateBuilder(args);

// Registrar HttpClient para a API
builder.Services.AddHttpClient("FiapAppAlunoApi", client =>
{
    client.BaseAddress = new Uri("https://localhost:44308/api/"); // Ajuste conforme a configuração da API
});

// Registrar Razor Pages
builder.Services.AddRazorPages();

var app = builder.Build();

// Configurar o pipeline
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage(); // Exibir página de erro detalhada no desenvolvimento
}
else
{
    app.UseExceptionHandler("/Error"); // Página de erro genérica para produção
    app.UseHsts();
}

// Redirecionar para HTTPS
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthorization();

app.MapRazorPages();

app.Run();