using App.Application;
using App.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace App.Api.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _service;

        public UsuarioController(IUsuarioService service)
        {
            _service = service;
        }

        [HttpGet("lista-usuarios")]
        public async Task<ActionResult<List<UsuariosDto>>> Usuarios()
        {
            var usuarios = await _service.Listar();
            return Ok(usuarios);
        }

        [HttpGet("matricula/{matricula}")]
        public async Task<ActionResult<UsuariosDto>> BuscarUsuarioPorMatricula(int matricula)
        {
            var usuario = await _service.BuscarPorMatricula(matricula);
            return Ok(usuario);
        }
        
        [HttpGet("email/{email}")]
        public async Task<ActionResult<UsuariosDto>> BuscarUsuarioPorEmail(string email)
        {
            var usuario = await _service.BuscarPorEmail(email);
            return Ok(usuario);
        }
        
        [HttpGet("id/{id}")]
        public async Task<ActionResult<UsuariosDto>> BuscarUsuarioPorId(int id)
        {
            var usuario = await _service.BuscarPorId(id);
            return Ok(usuario);
        }

        [HttpPost]
        public async Task<ActionResult<UsuariosDto>> AdicionarUsuario(CriarUsuarioDto dto)
        {
            var novoUsuario = await _service.CriarUsuario(dto);
            return Created("", novoUsuario);
        }

        [HttpPut("atualizar-usuario/{id}")]
        public async Task<ActionResult<UsuariosDto>> EditarUsuario(AtualizarUsuarioDto dto)
        {
            var usuarioEditado = await _service.EditarUsuario(dto);
            return Created("", usuarioEditado);
        }

        [HttpPatch("alterar-senha/{id}")]
        public async Task<ActionResult<UsuariosDto>> AlterarSenha(int id, AlterarSenhaDto dto)
        {
           await _service.AlterarSenha(id, dto.Senha);

           return NoContent();
        }
    }
}
