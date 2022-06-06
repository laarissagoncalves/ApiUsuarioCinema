using AutoMapper;
using FilmesApi.Data.Dtos.Gerente;
using FilmesApi.Models;
using System.Linq;

namespace FilmesApi.Profiles
{
    public class GerenteProfile : Profile
    {
        public GerenteProfile()
        {
            CreateMap<CreateGerenteDto, Gerente>();
            //estamos mapeando de Gerente para ReadGerenteDto
            CreateMap<Gerente, ReadGerenteDto>()
                //e para membros (ForMember) de Cinemas dentro do meu gerente (para esse membro, para essa propriedade) eu estou definindo as seguintes opções (opts => opts)
                .ForMember(gerente => gerente.Cinemas, opts => opts
                //mapeie desse gerente o seguinte: retorne aqui para mim nesse campo de gerente.Cinemas o seguinte
                .MapFrom(gerente => gerente.Cinemas.Select
                //selecione apenas: c.Id, c.Nome, c.Endereco, c.EnderecoId deste cinema (gerente.Cinemas) nesse objeto anonimo (c)
                (c => new {c.Id, c.Nome, c.Endereco, c.EnderecoId})));
        }
    }
}
