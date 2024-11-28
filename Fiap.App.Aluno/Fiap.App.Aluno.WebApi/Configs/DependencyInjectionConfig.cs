using Fiap.App.Aluno.Application.Interfaces;
using Fiap.App.Aluno.Application.Services;
using Fiap.App.Aluno.Domain.Interfaces;
using Fiap.App.Aluno.Infra.Context;
using Fiap.App.Aluno.Infra.Data;
using Fiap.App.Aluno.Infra.Data.Repository;

namespace Fiap.App.Aluno.WebApi.Configs
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            #region Infra
            services.AddScoped<ApplicationDbContext>();
            services.AddScoped<DesignTimeDbContextFactory>();
            services.AddSingleton<DapperContext>();
            services.AddScoped<IAlunoRepository, AlunoRepository>();
            services.AddScoped<ITurmaRepository, TurmaRepository>();
            services.AddScoped<IAlunoTurmaRepository, AlunoTurmaRepository>();
            #endregion

            #region Services
            services.AddScoped<IAlunoService, AlunoService>();
            services.AddScoped<ITurmaService, TurmaService>();
            #endregion

            return services;
        }
    }
}