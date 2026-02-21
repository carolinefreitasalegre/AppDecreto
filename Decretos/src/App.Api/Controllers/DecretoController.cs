using App.Application;
using App.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace App.Api.Controllers
{
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
        public async Task<ActionResult<List<DecretosDto>>> Decretos()
        {
            var decretos = await _service.ListarDecretos();
            return Ok(decretos);
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

        [HttpPut("atualizar-decreto/{id}")]
        public async Task<ActionResult<DecretosDto>> EditarDecreto(AtualizarDecretoDto dto, int id)
        {
            var decretoEditado = await _service.EditarDecreto(dto);
            return Created("", decretoEditado);
        }

       
    }
    
}
