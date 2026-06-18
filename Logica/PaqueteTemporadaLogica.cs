using Logica.Interface;
using Modelos;
using Repositorio;
using System.Collections.Generic;

namespace Logica
{
    public class PaqueteTemporadaLogica : ICRUDLogica<PaqueteTemporadaModel>
    {
        PaqueteTemporadaRepositorio repo = new PaqueteTemporadaRepositorio();

        public PaqueteTemporadaModel ActualizarRegistro(PaqueteTemporadaModel input)
        {
            return repo.ActualizarRegistro(input);
        }

        public PaqueteTemporadaModel CrearRegistro(PaqueteTemporadaModel input)
        {
            return repo.CrearRegistro(input);
        }

        public int deleteRegistro(int id)
        {
            return repo.deleteRegistro(id);
        }

        public List<PaqueteTemporadaModel> ListarTodo()
        {
            return repo.ListarTodo();
        }

        public PaqueteTemporadaModel ObtenerPorId(int id)
        {
            return repo.ObtenerPorId(id);
        }

        // Métodos adicionales útiles (si los necesitas en la lógica)
        public List<PaqueteTemporadaModel> ListarPorPaquete(int idPaquete)
        {
            return repo.ListarPorPaquete(idPaquete);
        }

        public List<PaqueteTemporadaModel> ListarPorTemporada(int idTemporada)
        {
            return repo.ListarPorTemporada(idTemporada);
        }

        public bool EliminarPorPaquete(int idPaquete)
        {
            return repo.EliminarPorPaquete(idPaquete);
        }
    }
}