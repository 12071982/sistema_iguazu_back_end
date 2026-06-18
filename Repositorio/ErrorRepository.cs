using Modelos;
using Modelos.NoDatabase;
using Repositorio.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Repositorio
{
    public class ErrorRepository : GenericRepository<ErrorModel>, IDisposable
    {
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }


        public List<ErrorModel> erroresPorUsuario(int id_user)
        {
            try
            {
                List<ErrorModel> lista = db.Error.Where(x => x.id_user == id_user).ToList();
                return lista;
            }
            catch (Exception ex)
            {
                CustomException exx = new CustomException("Error al filtrar los errores por usuario", (int)HttpStatusCode.InternalServerError, 500, "No Controlado", ex);
                throw exx;
            }

        }
    }
}
