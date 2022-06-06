using AutoMapper;
using FluentResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using UsuariosApi.Data.Dtos;
using UsuariosApi.Models;
using UsuariosApi.Request;
using System.Collections.Generic;

namespace UsuariosApi.Services
{
    public class CadastroService
    {
        private IMapper _mapper;
        private UserManager<CustomIdentityUser> _userManager;
        private EmailService _emailService;
        private UsuarioService _usuarioService;


        public CadastroService(IMapper mapper,
            UserManager<CustomIdentityUser> userManager,
            EmailService emailService, RoleManager<IdentityRole<int>> roleManager, UsuarioService usuarioService)
        {
            _mapper = mapper;
            _userManager = userManager;
            _emailService = emailService;
            _usuarioService = usuarioService;
        }


        //O custom vai dentro do result
        public Result<CustomIdentityUser> CadastraUsuario(CreateUsuarioDto createDto)
        {
            //Primeira parte, criação do usuário
            Usuario usuario = _mapper.Map<Usuario>(createDto);
            CustomIdentityUser usuarioIdentity = _mapper.Map<CustomIdentityUser>(usuario);
            
            Task<IdentityResult> resultadoIdentity = _userManager
                .CreateAsync(usuarioIdentity, createDto.Password);
            if (resultadoIdentity.Result.Succeeded)
            {
                return Result.Ok(usuarioIdentity);
            }
            return Result.Fail("Deu erro!");
        }

        public Result<CustomIdentityUser> GeraTokenEmail(CustomIdentityUser usuarioIdentity)
        {
           
            var code = _userManager
                    .GenerateEmailConfirmationTokenAsync(usuarioIdentity).Result;
            var encodedCode = HttpUtility.UrlEncode(code);
            _emailService.EnviarEmail(new[] { usuarioIdentity.Email },
            "Link de Ativação", usuarioIdentity.Id, encodedCode);
            return Result.Ok().WithSuccess(code);
        }

        public Result<CustomIdentityUser> IncluiNaRegraRegular(CustomIdentityUser user)
        {
            //Segunda parte, adicionando usuário a regra

            //AddToRoleAsync devolve se task fez não o objeto
            //.Result estou o objeto
            var resultadoIdentity = _userManager.AddToRoleAsync(user, "regular").Result;
            if (resultadoIdentity.Succeeded) return Result.Ok(user);
            return Result.Fail("Falha ao incrementar a regra!");
        }


        public Result AtivaContaUsuario(AtivaContaRequest request)
        {
            var identityUser = _userManager
                .Users
                .FirstOrDefault(u => u.Id == request.UsuarioId);
            var identityResult = _userManager
                .ConfirmEmailAsync(identityUser, request.CodigoAtivacao).Result;
            if (identityResult.Succeeded)
            {
                return Result.Ok();
            }
            return Result.Fail("Falha ao ativar conta de usuário");
        }
    }
}
