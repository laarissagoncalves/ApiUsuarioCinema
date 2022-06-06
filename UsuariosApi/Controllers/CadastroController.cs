using FluentResults;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsuariosApi.Data.Dtos;
using UsuariosApi.Request;
using UsuariosApi.Services;

namespace UsuariosApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CadastroController : ControllerBase
    {
        private CadastroService _cadastroService;
        private UsuarioService _usuarioService;

        public CadastroController(CadastroService cadastroService, UsuarioService usuarioService)
        {
            _cadastroService = cadastroService;
            _usuarioService = usuarioService;
        }

        [HttpPost]
        public IActionResult CadastrarUsuario(CreateUsuarioDto createDto)
        {
           var resultadoCadastro = _cadastroService.CadastraUsuario(createDto);

            if (resultadoCadastro.IsSuccess)
            {
                var resultadoRole = _cadastroService.IncluiNaRegraRegular(resultadoCadastro.Value);
                if (resultadoRole.IsSuccess)
                {
                    var resultadoToken = _cadastroService.GeraTokenEmail(resultadoRole.Value);
                    return Ok();
                }
                return StatusCode(500);
            }
            return StatusCode(500);
        }

        [HttpGet("/ativa")]
        public IActionResult AtivaContaUsuario([FromQuery] AtivaContaRequest request)
        {
            Result resultado = _cadastroService.AtivaContaUsuario(request);
            if (resultado.IsFailed) return StatusCode(500);
            return Ok(resultado);
        }

        [HttpGet("{name}")]
        public IActionResult RecuperaUsuarioPorNome(string name)
        {
            var readDto = _usuarioService.RecuperaUsuarioPorNome(name);
            if (readDto != null) return Ok(readDto);
            return NotFound();
        }

    }
}
