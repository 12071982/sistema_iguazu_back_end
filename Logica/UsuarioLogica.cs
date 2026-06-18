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
    public class UsuarioLogica : ICRUDLogica<UsuarioModel>
    {
        UsuarioRepositorio repo = new UsuarioRepositorio();
        public UsuarioModel ActualizarRegistro(UsuarioModel input)
        {
            input = repo.ActualizarRegistro(input);
            return input;
        }

        public UsuarioModel CrearRegistro(UsuarioModel input)
        {
            input = repo.CrearRegistro(input);
            return input;
        }

        public int deleteRegistro(int id)
        {
            id = repo.deleteRegistro(id);
            return id;
        }

        public List<UsuarioModel> ListarTodo()
        {
            List<UsuarioModel> lista = repo.ListarTodo();
            return lista;
        }

        public UsuarioModel ObtenerPorId(int id)
        {
            UsuarioModel resultado = repo.ObtenerPorId(id);
            return resultado;
        }

        public UsuarioModel ObtenerUsuarioPorUserName(string username)
        {
            // sintaxis lambd
            UsuarioModel user = repo.ObtenerUsuarioPorUserName(username);
            return user;
        }
    }
}
