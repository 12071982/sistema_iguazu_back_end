using Modelos;
using Repositorio.Interface;

namespace Repositorio
{
    public  class PaqueteRepositorio : ICRUD<PaqueteModel>
    {
         _dbContext db = new _dbContext();
        //private readonly _dbContext _db;

        //public ProductoRepositorio(_dbContext db)
        //{
        //    _db = db;
        //}


        public PaqueteModel ActualizarRegistro(PaqueteModel input)
        {
            db.Paquete.Update(input);
            db.SaveChanges();
            return input;
        }

        public PaqueteModel CrearRegistro(PaqueteModel input)
        {
            db.Paquete.Add(input);
            db.SaveChanges();
            return input;
        }

        public int deleteRegistro(int id)
        {
            // Buscar el paquete
            PaqueteModel paquete = db.Paquete.Find(id);
            if (paquete == null) return 0;

            // Eliminar todas las relaciones Paquete_Temporada asociadas a este paquete
            var relaciones = db.Paquete_Temporada.Where(pt => pt.ID_Paquete == id);
            db.Paquete_Temporada.RemoveRange(relaciones);

            // Eliminar el paquete
            db.Paquete.Remove(paquete);

            // Guardar ambos cambios (las relaciones y el paquete)
            return db.SaveChanges();
        }

        public List<PaqueteModel> ListarTodo()
        {
            List<PaqueteModel> lista = db.Paquete.ToList();
            return lista;
        }

        public PaqueteModel ObtenerPorId(int id)
        {
            PaqueteModel producto = db.Paquete.Find(id);
            return producto;
        }
        public bool updateMultipleRegistro(List<PaqueteModel> lst)
        {
            db.Paquete.UpdateRange(lst);
            db.SaveChanges();
            return true;
        }


        public async Task<bool> AddImageAsync(int id, string path)
        {
           PaqueteModel? producto = await db.Paquete.FindAsync(id);
            if (producto == null)
            {
                throw new Exception("No existe la marca");
            }
            producto.Imagen = path;
            int res = await db.SaveChangesAsync();

            // operador ternario
            return (res > 0) ? true : false;
        }
    }
}
