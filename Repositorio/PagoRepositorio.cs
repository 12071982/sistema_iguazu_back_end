using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Modelos;
using Repositorio.Interface;

namespace Repositorio
{
    public class PagoRepositorio : ICRUD<PagoModel>
    {
         _dbContext db = new _dbContext();
        public PagoModel ActualizarRegistro(PagoModel input)
        {
            db.Pago.Update(input);
            db.SaveChanges();
            return input;
        }

        public PagoModel CrearRegistro(PagoModel input)
        {
            db.Pago.Add(input);
            db.SaveChanges();
            return input;
        }

        public int deleteRegistro(int id)
        {

            PagoModel ventas = db.Pago.Find(id);
            db.Pago.Remove(ventas);
            return db.SaveChanges();
        }

        public List<PagoModel> ListarTodo()
        {
            List<PagoModel> lista = db.Pago.ToList();
            return lista;
        }

        public PagoModel ObtenerPorId(int id)
        {
            PagoModel ventas = db.Pago.Find(id);
            return ventas;
        }
        //public List<VentasModel> ListarTodoDetallado()
        //{
        //    List<VentasModel> lista =
        //        db.Ventas
        //        .Include(x => x.DetalleVentas)
       
        //        .ToList();

        //    return lista;
        //}
    }
}
