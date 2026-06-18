using Logica.Interface;
using Modelos;
using Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica
{
    public class ReservaLogica : ICRUDLogica<ReservaModel>
    {
        ReservaRepositorio repo = new ReservaRepositorio();
        public ReservaModel ActualizarRegistro(ReservaModel input)
        {
            input = repo.ActualizarRegistro(input);
            return input;
        }

        public ReservaModel CrearRegistro(ReservaModel input)
        {
            input = repo.CrearRegistro(input);
            return input;
        }

        public int deleteRegistro(int id)
        {
            id = repo.deleteRegistro(id);
            return id;
        }

        public List<ReservaModel> ListarTodo()
        {
            List<ReservaModel> lista = repo.ListarTodo();
            return lista;
        }

        public ReservaModel ObtenerPorId(int id)
        {
            ReservaModel resultado = repo.ObtenerPorId(id);
            return resultado;
        }
    }
}
