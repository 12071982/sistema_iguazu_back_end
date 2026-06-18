using Modelos;
using Repositorio.Interface;

namespace Repositorio
{
    public class ClienteRepositorio : ICRUD<ClienteModel>
    {
        _dbContext db = new _dbContext();

        public ClienteModel ActualizarRegistro(ClienteModel input)
        {
            db.Cliente.Update(input);
            db.SaveChanges();
            return input;
        }

        public ClienteModel CrearRegistro(ClienteModel input)
        {
            db.Cliente.Add(input);
            db.SaveChanges();
            return input;
        }

        public int deleteRegistro(int id)
        {

            ClienteModel cliente = db.Cliente.Find(id);
            db.Cliente.Remove(cliente);
            return db.SaveChanges();
        }

        public List<ClienteModel> ListarTodo()
        {
            List<ClienteModel> lista = db.Cliente.ToList();
            return lista;
        }

        public ClienteModel ObtenerPorId(int id)
        {
            ClienteModel cliente = db.Cliente.Find(id);
            return cliente;
        }

    }
}
