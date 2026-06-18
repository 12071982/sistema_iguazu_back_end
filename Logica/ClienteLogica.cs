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
    public class ClienteLogica : ICRUDLogica<ClienteModel>
    {
        ClienteRepositorio repo = new ClienteRepositorio();
        public ClienteModel ActualizarRegistro(ClienteModel input)
        {
            input = repo.ActualizarRegistro(input);
            return input;
        }

        public ClienteModel CrearRegistro(ClienteModel input)
        {
            input = repo.CrearRegistro(input);
            return input;
        }

        public int deleteRegistro(int id)
        {
            id = repo.deleteRegistro(id);
            return id;
        }

        public List<ClienteModel> ListarTodo()
        {
            List<ClienteModel> lista = repo.ListarTodo();
            return lista;
        }

        public ClienteModel ObtenerPorId(int id)
        {
            ClienteModel resultado = repo.ObtenerPorId(id);
            return resultado;
        }
    }
}
