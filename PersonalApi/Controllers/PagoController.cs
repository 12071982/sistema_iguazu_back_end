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
    public class PagoController : ControllerBase
    {
        PagoLogica pagoLogica = new PagoLogica();

        [HttpGet]
        public IActionResult get()
        {
            List<PagoModel> listaResultado = new List<PagoModel>();
            listaResultado = pagoLogica.ListarTodo();
            return Ok(listaResultado);
        }

        [HttpGet("{id}")]
        public IActionResult getId(int id)
        {
            PagoModel res = new PagoModel();
            res = pagoLogica.ObtenerPorId(id);
            return Ok(res);
        }

        [HttpPost]
        public IActionResult post(PagoModel request)
        {
            PagoModel response = pagoLogica.CrearRegistro(request);
            return Ok(response);
        }


        [HttpPut]
        public IActionResult put(PagoModel request)
        {
            PagoModel response = pagoLogica.ActualizarRegistro(request);
            return Ok(response);
        }


        [HttpDelete("{id}")]
        public IActionResult delete(int id)
        {
            int response = pagoLogica.deleteRegistro(id);
            return Ok(response);
        }
        //[HttpGet]
        //[Route("detallado")]
        //public IActionResult getDetallado()
        //{
        //    List<VentasModel> listaResultado = new List<VentasModel>();
        //    listaResultado = ventasLogica.ListarTodoDetallado();
        //    return Ok(listaResultado);
        //}
    }
}
