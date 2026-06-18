using Logica.Interface;
using Modelos;
using Repositorio;

namespace Logica
{
    public class DestinoLogica : ICRUDLogica<DestinoModel>
    {
        DestinoRepositorio repo = new DestinoRepositorio();

        public DestinoModel ActualizarRegistro(DestinoModel input)
        {
            input = repo.ActualizarRegistro(input);
            return input;
        }

        public DestinoModel CrearRegistro(DestinoModel input)
        {
            input = repo.CrearRegistro(input);
            return input;
        }

        public int deleteRegistro(int id)
        {
            id = repo.deleteRegistro(id);
            return id;
        }

        public List<DestinoModel> ListarTodo()
        {
            List<DestinoModel> lista = repo.ListarTodo();
            return lista;
        }

        public DestinoModel ObtenerPorId(int id)
        {
            DestinoModel resultado = repo.ObtenerPorId(id);
            return resultado;
        }

    }
}
