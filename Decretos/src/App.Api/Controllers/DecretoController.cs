using App.Application;
using App.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace App.Api.Controllers
{
    [Authorize]
    [Route("/[controller]")]
    [ApiController]
    public class DecretoController : ControllerBase
    {
        private readonly IDecretoService _service;

        public DecretoController(IDecretoService service)
        {
            _service = service;
        }

        [HttpGet("lista-decretos")]
        public async Task<ActionResult> Decretos(int page = 1, int pageSize = 10)
        {
            var (decretos, total) = await _service.ListarDecretos(page, pageSize);

            return Ok(new { data = decretos, total, page, pageSize });
        }

        [HttpGet("buscar-decreto/{decreto}")]
        public async Task<ActionResult<DecretosDto>> BuscarDecreto(int decreto)
        {
            var num = await _service.BuscarViaNumero(decreto);
            return Ok(num);
        }
        
        [HttpGet("decreto/{id}")]
        public async Task<ActionResult<DecretosDto>> BuscarDecretoPorId(int id)
        {
            var decreto = await _service.BuscarViaId(id);
            return Ok(decreto);
        }

        [HttpPost]
        public async Task<ActionResult<DecretosDto>> AdicionarDecreto(CriarDecretoDto dto)
        {
            var novoDecreto = await _service.AdicionarDecretos(dto);
            return Created("", novoDecreto);
        }

        [HttpPut("atualizar-decreto/{numeroDecreto}")]
        public async Task<ActionResult<DecretosDto>> EditarDecreto(AtualizarDecretoDto dto,  int numeroDecreto)
        {
            var decretoEditado = await _service.EditarDecreto(numeroDecreto, dto);
            return NoContent();
        }

       
    }
    
}
