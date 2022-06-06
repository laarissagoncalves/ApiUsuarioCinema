using AutoMapper;
using Microsoft.AspNetCore.Identity;
using UsuariosApi.Data.Dtos;
using UsuariosApi.Models;
using FluentResults;

namespace UsuariosApi.Profiles
{
    public class UsuarioProfile : Profile
    {
        public UsuarioProfile()
        {
            CreateMap<CreateUsuarioDto, Usuario>();
            CreateMap<Usuario, IdentityUser<int>>();
            CreateMap<Usuario, CustomIdentityUser>();
            CreateMap<Usuario, ReadUsuarioDto>();
            CreateMap<Result, Usuario>();
        }
    }
}
