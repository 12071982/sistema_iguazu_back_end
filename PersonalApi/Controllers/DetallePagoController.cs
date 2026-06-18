using Logica;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Modelos;

namespace PersonalApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    //[Authorize]
    public class DetallePagoController : ControllerBase
    {
        DetallePagoLogica detallepagoLogica = new DetallePagoLogica();

        [HttpGet]
        public IActionResult get()
        {
            List<DetallePagoModel> listaResultado = new List<DetallePagoModel>();
            listaResultado = detallepagoLogica.ListarTodo();
            return Ok(listaResultado);
        }

        [HttpGet("{id}")]
        public IActionResult getId(int id)
        {
            DetallePagoModel res = new DetallePagoModel();
            res = detallepagoLogica.ObtenerPorId(id);
            return Ok(res);
        }

        [HttpPost]
        public IActionResult post(DetallePagoModel request)
        {
            DetallePagoModel response = detallepagoLogica.CrearRegistro(request);
            return Ok(response);
        }

        [HttpPut]
        public IActionResult put(DetallePagoModel request)
        {
            DetallePagoModel response = detallepagoLogica.ActualizarRegistro(request);
            return Ok(response);
        }


        [HttpDelete("{id}")]
        public IActionResult delete(int id)
        {
            int response = detallepagoLogica.deleteRegistro(id);
            return Ok(response);
        }
    }
}
