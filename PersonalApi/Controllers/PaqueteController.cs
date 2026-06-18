using Logica;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Modelos;
using Modelos.NoDatabase;
using otroProyecto5.Utils;
using Repositorio;

namespace PersonalApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class PaqueteController : ControllerBase
    {
        PaqueteLogica paqueteLogica = new PaqueteLogica();
        private readonly IWebHostEnvironment _webHostEnvironment;
        //private readonly ReservasContext _db;
        private readonly PaqueteRepositorio _paqueteService;
        // private readonly ILogger<ProductoController> _logger;

        public PaqueteController(
             ILogger<PaqueteController> logger,
            IWebHostEnvironment webHostEnvironment
        // _dbContext context
        )
        {
           // _logger = logger;
            _webHostEnvironment = webHostEnvironment;
          //  _db = context;
         //   _productService = new ProductoRepositorio(context);
        }



        [HttpGet]
        public IActionResult get()
        {
            List<PaqueteModel> listaResultado = new List<PaqueteModel>();
            listaResultado = paqueteLogica.ListarTodo();
            return Ok(listaResultado);
        }

        [HttpGet("{id}")]
        public IActionResult getId(int id)
        {
            PaqueteModel res = new PaqueteModel();
            res = paqueteLogica.ObtenerPorId(id);
            return Ok(res);
        }

        [HttpPost]
        public IActionResult post(PaqueteModel request)
        {
            PaqueteModel response = paqueteLogica.CrearRegistro(request);
            return Ok(response);
        }

        [HttpPut]
        public IActionResult put(PaqueteModel request)
        {
            PaqueteModel response = paqueteLogica.ActualizarRegistro(request);
            return Ok(response);
        }


        [HttpDelete("{id}")]
        public IActionResult delete(int id)
        {
            int response = paqueteLogica.deleteRegistro(id);
            return Ok(response);
        }



        [HttpPut]
        [Produces("application/json")]
        [Route("{IdProducto}/agregar-imagen")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //[UserAuthorized]
        public async Task<ActionResult<object>> AddImageAsync(
            [FromRoute] int IdProducto, [FromForm] FileImage body)
        {
            //copiar la imagen y generar el path
            try
            {
                string path = await CopyImageAsync(body.imageFile, IdProducto);
                bool isOk = await paqueteLogica.AddImageAsync(IdProducto, path);
                return isOk ? Ok(new { imageUrl = path }) : NotFound();
            }
            catch (Exception ex)
            {
                var err = new ErrorResponse
                {
                    idError = 500,
                    mensaje = ex.Message
                };
                return Ok(err);
            }
        }

        private async Task<string> CopyImageAsync(IFormFile image, int id)
        {

            List<string> aceptedMimeTypes = new List<string> {
                "image/jpg", "image/jpeg", "image/png" };
            string mimetype = image.ContentType;
            if (aceptedMimeTypes.Where(m => m == mimetype).Count() <= 0)
            {
                throw new Exception("Tipo de archivo invalido");
            }
            long size = image.Length;
            if (size > 2048000)
            {
                throw new Exception("No archivos mayores a 2 MB");
            }
            else if (size > 0)
            {
                string ext = image.FileName.Split('.').Last();
                string wwwroot = _webHostEnvironment.WebRootPath;
                string nameImage = $"Producto-{id}.{ext}";
                string file = Path.Combine(wwwroot, "uploads", "products", nameImage);
                using (var stream = System.IO.File.Create(file))
                {
                    await image.CopyToAsync(stream);
                }
                return $"/uploads/products/{nameImage}";
            }
            throw new Exception("archivo invalido");
        }
    }

    public class FileImage
    {
        public IFormFile imageFile { get; set; }
    }


}