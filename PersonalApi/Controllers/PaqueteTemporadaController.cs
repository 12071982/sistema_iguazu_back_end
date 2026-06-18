using Logica;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Modelos;
using System.Collections.Generic;

namespace PersonalApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class PaqueteTemporadaController : ControllerBase
    {
        PaqueteTemporadaLogica paqueteTemporadaLogica = new PaqueteTemporadaLogica();

        [HttpGet]
        public IActionResult Get()
        {
            List<PaqueteTemporadaModel> lista = paqueteTemporadaLogica.ListarTodo();
            return Ok(lista);
        }

        [HttpGet("{id}")]
        public IActionResult GetId(int id)
        {
            PaqueteTemporadaModel resultado = paqueteTemporadaLogica.ObtenerPorId(id);
            return Ok(resultado);
        }

        [HttpPost]
        public IActionResult Post(PaqueteTemporadaModel request)
        {
            PaqueteTemporadaModel response = paqueteTemporadaLogica.CrearRegistro(request);
            return Ok(response);
        }

        [HttpPut]
        public IActionResult Put(PaqueteTemporadaModel request)
        {
            PaqueteTemporadaModel response = paqueteTemporadaLogica.ActualizarRegistro(request);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            int response = paqueteTemporadaLogica.deleteRegistro(id);
            return Ok(response);
        }

        // Métodos adicionales útiles
        [HttpGet("por-paquete/{idPaquete}")]
        public IActionResult GetPorPaquete(int idPaquete)
        {
            List<PaqueteTemporadaModel> lista = paqueteTemporadaLogica.ListarPorPaquete(idPaquete);
            return Ok(lista);
        }

        [HttpGet("por-temporada/{idTemporada}")]
        public IActionResult GetPorTemporada(int idTemporada)
        {
            List<PaqueteTemporadaModel> lista = paqueteTemporadaLogica.ListarPorTemporada(idTemporada);
            return Ok(lista);
        }

        [HttpDelete("paquete/{idPaquete}")]
        public IActionResult DeletePorPaquete(int idPaquete)
        {
            bool success = paqueteTemporadaLogica.EliminarPorPaquete(idPaquete);
            return Ok(success);
        }
    }
}