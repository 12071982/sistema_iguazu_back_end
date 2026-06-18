using Modelos;
using Repositorio.Interface;

namespace Repositorio
{
    public class TemporadaRepositorio : ICRUD<TemporadaModel>
    {
        _dbContext db = new _dbContext();

        public TemporadaModel ActualizarRegistro(TemporadaModel input)
        {
            db.Temporada.Update(input);
            db.SaveChanges();
            return input;
        }

        public TemporadaModel CrearRegistro(TemporadaModel input)
        {
            db.Temporada.Add(input);
            db.SaveChanges();
            return input;
        }

        public int deleteRegistro(int id)
        {
            TemporadaModel temporada = db.Temporada.Find(id);
            if (temporada != null)
            {
                db.Temporada.Remove(temporada);
                return db.SaveChanges();
            }
            return 0;
        }

        public List<TemporadaModel> ListarTodo()
        {
            return db.Temporada.ToList();
        }

        public TemporadaModel ObtenerPorId(int id)
        {
            return db.Temporada.Find(id);
        }
    }
}