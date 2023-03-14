using ApiStore.DB;
using ApiStore.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiStore.Controllers
{
    [ApiController]
    [Route("producto")]
    public class ProductoController : ControllerBase
    {
        private readonly StoreContext context;
        public ProductoController(StoreContext context)
        {
            this.context = context;
        }
        [HttpGet]
        [Route("listar")]
        public async Task<ActionResult<List<Producto>>> ListProducto()
        {
            var producto = await context.Productos.ToListAsync();

            return producto;
        }
        [HttpGet]
        [Route("listId")]
        public async Task<ActionResult<Producto>> ListId(int id)
        {
            var pro = await context.Productos.FindAsync(id);
            return pro;
        }
        [HttpPost]
        [Route("update")]
        public async Task<IActionResult> updateProducto(Producto pro, int id)
        {
            var product = await context.Productos.FindAsync(id);
            product.IdProducto = pro.IdProducto;
            product.Codigo = pro.Codigo;
            product.Nombre = pro.Nombre;
            product.Descripcion = pro.Descripcion;
            product.IdCategoria = pro.IdCategoria;
            product.Stock = pro.Stock;
            product.PrecioVenta = pro.PrecioVenta;
            product.PrecioCompra = pro.PrecioCompra;
            product.NombreProveedor = pro.NombreProveedor;
            product.Ruc = pro.Ruc;
            product.NumProveedor = pro.NumProveedor;
            product.Estado = true;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return NotFound(e);

            }

            return  Ok( new {message = "Data actualizada"});
        }
        [HttpDelete]
        [Route("delete")]
        public async Task<ActionResult<Producto>> DeleteProducto(int id)
        {
            var pro = await context.Productos.FindAsync(id);
            context.Productos.Remove(pro);
            context.SaveChanges();

            return pro;
        }
    }
}
