using Modelos;
using Repositorio.Interface;

namespace Repositorio
{
    public class PaqueteTemporadaRepositorio : ICRUD<PaqueteTemporadaModel>
    {
        _dbContext db = new _dbContext();

        public PaqueteTemporadaModel ActualizarRegistro(PaqueteTemporadaModel input)
        {
            db.Paquete_Temporada.Update(input);
            db.SaveChanges();
            return input;
        }

        public PaqueteTemporadaModel CrearRegistro(PaqueteTemporadaModel input)
        {
            db.Paquete_Temporada.Add(input);
            db.SaveChanges();
            return input;
        }

        public int deleteRegistro(int id)
        {
            PaqueteTemporadaModel relacion = db.Paquete_Temporada.Find(id);
            if (relacion != null)
            {
                db.Paquete_Temporada.Remove(relacion);
                return db.SaveChanges();
            }
            return 0;
        }

        public List<PaqueteTemporadaModel> ListarTodo()
        {
            return db.Paquete_Temporada.ToList();
        }

        public PaqueteTemporadaModel ObtenerPorId(int id)
        {
            return db.Paquete_Temporada.Find(id);
        }

        // Métodos útiles adicionales (opcionales)
        public List<PaqueteTemporadaModel> ListarPorPaquete(int idPaquete)
        {
            return db.Paquete_Temporada.Where(pt => pt.ID_Paquete == idPaquete).ToList();
        }

        public List<PaqueteTemporadaModel> ListarPorTemporada(int idTemporada)
        {
            return db.Paquete_Temporada.Where(pt => pt.ID_Temporada == idTemporada).ToList();
        }

        public bool EliminarPorPaquete(int idPaquete)
        {
            var relaciones = db.Paquete_Temporada.Where(pt => pt.ID_Paquete == idPaquete);
            db.Paquete_Temporada.RemoveRange(relaciones);
            return db.SaveChanges() >= 0;
        }
    }
}