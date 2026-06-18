using Microsoft.EntityFrameworkCore;
using Modelos;
using Repositorio.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Repositorio
{
    public class DestinoRepositorio : ICRUD<DestinoModel>
    {
        _dbContext db = new _dbContext();

        public DestinoModel ActualizarRegistro(DestinoModel input)
        {
            db.Destino.Update(input);
            db.SaveChanges();
            return input;
        }

        public DestinoModel CrearRegistro(DestinoModel input)
        {
            db.Destino.Add(input);
            db.SaveChanges();
            return input;
        }

        public int deleteRegistro(int id)
        {

            DestinoModel categoria = db.Destino.Find(id);
            db.Destino.Remove(categoria);
            return db.SaveChanges();
        }

        public List<DestinoModel> ListarTodo()
        {
            List<DestinoModel> lista = db.Destino.ToList();
            return lista;
        }

        public DestinoModel ObtenerPorId(int id)
        {
            DestinoModel categoria = db.Destino.Find(id);
            return categoria;
        }
    }
}
