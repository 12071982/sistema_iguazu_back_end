using Logica.Interface;
using Modelos;
using Repositorio;

namespace Logica
{

    public class PaqueteLogica : ICRUDLogica<PaqueteModel>
    {

        PaqueteRepositorio repo = new PaqueteRepositorio();
        

        public PaqueteModel ActualizarRegistro(PaqueteModel input)
        {
            input = repo.ActualizarRegistro(input);
            return input;
        }

        public PaqueteModel CrearRegistro(PaqueteModel input)
        {
            input = repo.CrearRegistro(input);
            return input;
        }

        public int deleteRegistro(int id)
        {
            id = repo.deleteRegistro(id);
            return id;
        }

        public List<PaqueteModel> ListarTodo()
        {
            List<PaqueteModel> lista = repo.ListarTodo();
            return lista;
        }

        public PaqueteModel ObtenerPorId(int id)
        {
            PaqueteModel resultado = repo.ObtenerPorId(id);
            return resultado;
        }

        public bool updateMultipleRegistro (List<PaqueteModel> lst)
        {
            repo.updateMultipleRegistro(lst);
            return true; 
        }

        public async Task<bool> AddImageAsync(int id, string path)
        {
           repo.AddImageAsync(id, path);

            // operador ternario
            return  true;
        }
    }
}
