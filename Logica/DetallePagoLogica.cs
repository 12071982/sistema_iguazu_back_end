using Logica.Interface;
using Modelos;
using Repositorio;

namespace Logica
{
    public class DetallePagoLogica : ICRUDLogica<DetallePagoModel>
    {
    
        DetallePagoRepositorio repo = new DetallePagoRepositorio();

        public DetallePagoModel ActualizarRegistro(DetallePagoModel input)
        {
            input = repo.ActualizarRegistro(input);
            return input;
        }

        public DetallePagoModel CrearRegistro(DetallePagoModel input)
        {
            input = repo.CrearRegistro(input);
            return input;
        }

        public int deleteRegistro(int id)
        {
            id = repo.deleteRegistro(id);
            return id;
        }

        public List<DetallePagoModel> ListarTodo()
        {
            List<DetallePagoModel> lista = repo.ListarTodo();
            return lista;
        }

        public DetallePagoModel ObtenerPorId(int id)
        {
            DetallePagoModel resultado = repo.ObtenerPorId(id);
            return resultado;
        }
    }
}
