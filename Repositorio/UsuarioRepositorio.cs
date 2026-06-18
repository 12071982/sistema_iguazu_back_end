using Microsoft.EntityFrameworkCore;
using Modelos;
using Repositorio.Interface;
using Utilitarios;

namespace Repositorio
{
    public class UsuarioRepositorio : ICRUD<UsuarioModel>
    {
        _dbContext db = new _dbContext();
        public UsuarioModel ActualizarRegistro(UsuarioModel input)
        {
            var usuarioActual = db.Usuario.AsNoTracking().FirstOrDefault(x => x.ID_Usuario == input.ID_Usuario);

            if (usuarioActual != null)
            {
                bool intentandoDejarDeSerAdmin = usuarioActual.Rol == "Administrador" && input.Rol != "Administrador";
                bool intentandoDesactivarse = usuarioActual.Estatus == "Activo" && input.Estatus != "Activo";

                if (intentandoDejarDeSerAdmin || intentandoDesactivarse)
                {
                    int totalAdmins = db.Usuario.Count(x => x.Rol == "Administrador" && x.Estatus == "Activo");
                    if (totalAdmins <= 1)
                    {
                        throw new Exception("Debe existir al menos un administrador activo en el sistema.");
                    }
                }

                if (input.Password != usuarioActual.Password)
                {
                    input.Password = UtilSecurity.encriptar_AES(input.Password);
                }
            }

            db.Usuario.Update(input);
            db.SaveChanges();
            return input;
        }

        public UsuarioModel CrearRegistro(UsuarioModel input)
        {
            input.Password = UtilSecurity.encriptar_AES(input.Password);

            if (string.IsNullOrEmpty(input.Fecha_Registro))
            {
                input.Fecha_Registro = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            }

            if (string.IsNullOrEmpty(input.Estatus)) input.Estatus = "Activo";
            if (string.IsNullOrEmpty(input.Rol)) input.Rol = "Empleado";

            // IMPORTANTE: No asignar el ID explícitamente
            input.ID_Usuario = null; // Asegurar que sea null para que SQL Server lo auto-genere

            db.Usuario.Add(input);
            db.SaveChanges();

            return input;
        }

        public int deleteRegistro(int id)
        {
            UsuarioModel usuario = db.Usuario.Find(id);
            if (usuario == null) return 0;

            if (usuario.Rol == "Administrador" && usuario.Estatus == "Activo")
            {
                int totalAdmins = db.Usuario.Count(x => x.Rol == "Administrador" && x.Estatus == "Activo");

                if (totalAdmins <= 1)
                {
                    // Lanzamos una excepción que capturaremos en el controlador
                    throw new Exception("No se puede eliminar al único administrador activo del sistema.");
                }
            }

            db.Usuario.Remove(usuario);
            return db.SaveChanges();
        }

        public List<UsuarioModel> ListarTodo()
        {
            List<UsuarioModel> lista = db.Usuario.ToList();
            return lista;
        }

        public UsuarioModel ObtenerPorId(int id)
        {
            UsuarioModel usuario = db.Usuario.Find(id);
            return usuario;
        }

        public UsuarioModel ObtenerUsuarioPorUserName(string username)
        {
            // sintaxis lambda
            UsuarioModel user = db.Usuario.Where(x => x.Correo_Electronico == username).FirstOrDefault();
            return user;
        }
    }
}
