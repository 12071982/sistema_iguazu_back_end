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

    public class ClienteController : ControllerBase
    {
        /*ctrl + r + r */
        ClienteLogica clientelog = new ClienteLogica();

        [HttpGet]
        public IActionResult get()
        {
            List<ClienteModel> listaResultado = new List<ClienteModel>();
            listaResultado = clientelog.ListarTodo();
            return Ok(listaResultado);
        }

        [HttpGet("{id}")]
        public IActionResult getId(int id)
        {
            ClienteModel res = new ClienteModel();
            res = clientelog.ObtenerPorId(id);
            return Ok(res);
        }

        [HttpGet("email")]
        public IActionResult VerificarCorreo(string correo)
        {
            List<ClienteModel> clientes = clientelog.ListarTodo();

            var existe = clientes.Any(c => c.Correo.Equals(correo, StringComparison.OrdinalIgnoreCase));

            if (existe)
                return Ok(); // El correo ya existe

            return NotFound(); // El correo no existe aún
        }


        [HttpPost]
        public IActionResult post(ClienteModel request)
        {
            ClienteModel response = clientelog.CrearRegistro(request);
            return Ok(response);
        }

        [HttpPut]
        public IActionResult put(ClienteModel request)
        {
            ClienteModel response = clientelog.ActualizarRegistro(request);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public IActionResult delete(int id)
        {
            int response = clientelog.deleteRegistro(id);
            return Ok(response);
        }

    }
}
