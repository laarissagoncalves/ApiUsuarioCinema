using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using UsuariosApi.Data;
using UsuariosApi.Data.Dtos;
using UsuariosApi.Models;

namespace UsuariosApi.Services
{
    public class UsuarioService
    {
        private IMapper _mapper;
        private UserDbContext _context;

        public UsuarioService(IMapper mapper,  UserDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public int? RecuperaUsuarioPorNome(string name)
        {
            var usuario = _context.Users.FirstOrDefault(usuario => usuario.UserName == name);
            if (usuario != null)
            {

                return usuario.Id;
            }
            return null;
        }
    }
}
