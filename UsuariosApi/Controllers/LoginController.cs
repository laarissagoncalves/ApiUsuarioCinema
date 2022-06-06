using FluentResults;
using Microsoft.AspNetCore.Mvc;
using UsuariosApi.Request;
using UsuariosApi.Services;

namespace UsuariosApi.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        private LoginService _loginService;

        public LoginController(LoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpPost]
        public IActionResult LogaUsuario(LoginRequest request)
        {
            Result resultado = _loginService.LogaUsuario(request);
            if(resultado.IsFailed) return Unauthorized(resultado.Errors);
            return Ok(resultado);
        }

        [HttpPost("/solicita-reset")]
        public IActionResult SolicitaResetSenhaUsuario(SolicitaResetRequest request)
        {
            Result resultado = _loginService.SolicitaResetSenhaUsuario(request);
            if (resultado.IsFailed) return Unauthorized(resultado.Errors);
            return Ok(resultado);
        }

        [HttpPost("/efetua-reset")]
        public IActionResult ResetaSenhaUsuario(EfetuaResetRequest request)
        {
            Result resultado = _loginService.ResetaSenhaUsuario(request);
            if (resultado.IsFailed) return Unauthorized(resultado.Errors);
            return Ok(resultado);
        }
    }
}
