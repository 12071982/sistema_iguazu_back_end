using Logica;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Modelos;

namespace PersonalApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class TemporadaController : ControllerBase
    {
        TemporadaLogica temporadaLogica = new TemporadaLogica();

        [HttpGet]
        public IActionResult Get()
        {
            List<TemporadaModel> listaResultado = temporadaLogica.ListarTodo();
            return Ok(listaResultado);
        }

        [HttpGet("{id}")]
        public IActionResult GetId(int id)
        {
            TemporadaModel resultado = temporadaLogica.ObtenerPorId(id);
            return Ok(resultado);
        }

        [HttpPost]
        public IActionResult Post(TemporadaModel request)
        {
            TemporadaModel response = temporadaLogica.CrearRegistro(request);
            return Ok(response);
        }

        [HttpPut]
        public IActionResult Put(TemporadaModel request)
        {
            TemporadaModel response = temporadaLogica.ActualizarRegistro(request);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            int response = temporadaLogica.deleteRegistro(id);
            return Ok(response);
        }
    }
}