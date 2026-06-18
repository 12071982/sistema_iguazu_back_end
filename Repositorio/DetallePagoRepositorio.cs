using Modelos;
using Repositorio.Interface;

namespace Repositorio
{
    public class DetallePagoRepositorio : ICRUD<DetallePagoModel>
    {
        _dbContext db = new _dbContext();
        public DetallePagoModel ActualizarRegistro(DetallePagoModel input)
        {
            db.Detalle_Pago.Update(input);
            db.SaveChanges();
            return input;
        }

        public DetallePagoModel CrearRegistro(DetallePagoModel input)
        {
            db.Detalle_Pago.Add(input);
            db.SaveChanges();
            return input;
        }

        public int deleteRegistro(int id)
        {

            DetallePagoModel producto = db.Detalle_Pago.Find(id);
            db.Detalle_Pago.Remove(producto);
            return db.SaveChanges();
        }

        public List<DetallePagoModel> ListarTodo()
        {
            List<DetallePagoModel> lista = db.Detalle_Pago.ToList();
            return lista;
        }

        public DetallePagoModel ObtenerPorId(int id)
        {
            DetallePagoModel producto = db.Detalle_Pago.Find(id);
            return producto;
        }
    }

}
