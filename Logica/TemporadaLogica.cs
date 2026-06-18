using Logica.Interface;
using Modelos;
using Repositorio;

namespace Logica
{
    public class TemporadaLogica : ICRUDLogica<TemporadaModel>
    {
        TemporadaRepositorio repo = new TemporadaRepositorio();

        public TemporadaModel ActualizarRegistro(TemporadaModel input)
        {
            input = repo.ActualizarRegistro(input);
            return input;
        }

        public TemporadaModel CrearRegistro(TemporadaModel input)
        {
            input = repo.CrearRegistro(input);
            return input;
        }

        public int deleteRegistro(int id)
        {
            return repo.deleteRegistro(id);
        }

        public List<TemporadaModel> ListarTodo()
        {
            return repo.ListarTodo();
        }

        public TemporadaModel ObtenerPorId(int id)
        {
            return repo.ObtenerPorId(id);
        }
    }
}