using AutoMapper;
using Fiap.App.Aluno.Application.Model;

namespace Fiap.App.Aluno.WebApi.Configs
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<Domain.Entidades.Aluno, AlunoDto>().ReverseMap();
            CreateMap<Domain.Entidades.Turma, TurmaDto>().ReverseMap();
            CreateMap<Domain.Entidades.AlunoTurma, AlunoTurmaDto>().ReverseMap();
        }
    }
}