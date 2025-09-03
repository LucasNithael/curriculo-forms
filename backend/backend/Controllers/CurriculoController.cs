using backend.Models;
using backend.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace backend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CurriculoController(ICurriculoRepository curriculoRepository) : BaseController
    {
        private readonly ICurriculoRepository _curriculoRepository = curriculoRepository;


        [HttpGet("", Name = "[controller]_Listar")]
        public async Task<IActionResult> Listar()
        {
            var curriculos = await _curriculoRepository.Listar();
            return CustomResponse(curriculos, (int)HttpStatusCode.OK);
        }

        [HttpPost("", Name = "[controller]_Adicionar")]
        public async Task<IActionResult> Adicionar(CurriculoInput input)
        {
            await _curriculoRepository.Adicionar(input);
            return CustomResponse(null, (int)HttpStatusCode.OK);
        }

        
    }
}
