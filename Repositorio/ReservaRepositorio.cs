using Modelos;
using Repositorio.Interface;

namespace Repositorio
{
    public class ReservaRepositorio : ICRUD<ReservaModel>
    {
        _dbContext db = new _dbContext();

        public ReservaModel ActualizarRegistro(ReservaModel input)
        {
            db.Reserva.Update(input);
            db.SaveChanges();
            return input;
        }

        public ReservaModel CrearRegistro(ReservaModel input)
        {
            db.Reserva.Add(input);
            db.SaveChanges();
            return input;
        }

        public int deleteRegistro(int id)
        {
            ReservaModel proveedor = db.Reserva.Find(id);
            db.Reserva.Remove(proveedor);
            return db.SaveChanges();
        }

        public List<ReservaModel> ListarTodo()
        {
            List<ReservaModel> lista = db.Reserva.ToList();
            return lista;
        }

        public ReservaModel ObtenerPorId(int id)
        {
            ReservaModel proveedor = db.Reserva.Find(id);
            return proveedor;
        }

    }
}
