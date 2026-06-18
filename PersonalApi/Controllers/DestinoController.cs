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
    public class DestinoController : ControllerBase
    {
        DestinoLogica destinologica = new DestinoLogica();

        [HttpGet]
        public IActionResult get()
        {
            List<DestinoModel> listaResultado = new List<DestinoModel>();
            listaResultado = destinologica.ListarTodo();
            return Ok(listaResultado);
        }

        [HttpGet("{id}")]
        public IActionResult getId(int id)
        {
            DestinoModel res = new DestinoModel();
            res = destinologica.ObtenerPorId(id);
            return Ok(res);
        }

        [HttpPost]
        public IActionResult post(DestinoModel request)
        {
            DestinoModel response = destinologica.CrearRegistro(request);
            return Ok(response);
        }

        [HttpPut]
        public IActionResult put(DestinoModel request)
        {
            DestinoModel response = destinologica.ActualizarRegistro(request);
            return Ok(response);
        }


        [HttpDelete("{id}")]
        public IActionResult delete(int id)
        {
            int response = destinologica.deleteRegistro(id);
            return Ok(response);
        }
    }
}
