using ApiStore.DB;
using ApiStore.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiStore.Controllers
{
    [ApiController]
    [Route("cliente")]
    public class ClienteController : ControllerBase
    {
        private readonly StoreContext context;

        public ClienteController (StoreContext context)
        {
            this.context = context;
        }

        [HttpGet]
        [Route("Listar")]
        public async Task<ActionResult<IEnumerable<Cliente>>> ListarCliente()
        {
            var cliente =  await context.Clientes.ToListAsync();

            return cliente;
        }
        [HttpPost]
        [Route("Guardar")]
        public async Task<ActionResult<Cliente>> GuardarCliente(Cliente clie)
        {
            var cliente = new Cliente()
            {
                Documento = clie.Documento,
                NombreCompleto = clie.NombreCompleto,
                Correo = clie.Correo,
                Estado = true,
                FechaRegistro = DateTime.Now,
                Telefono = clie.Telefono
            };

            await context.Clientes.AddAsync(cliente);
            await context.SaveChangesAsync();

            return cliente;

        }
        [HttpGet]
        [Route("ListId")]
        public async Task<ActionResult<Cliente>> ListarClienteId(int id)
        {
            var cliente = await context.Clientes.FindAsync(id);

            return cliente;
        }
        [HttpPut]
        [Route("Actualizar")]
        public async Task<IActionResult> UpdateCliente(int id, Cliente clie)
        {
            var cliente = await context.Clientes.FindAsync(id);

            cliente.Documento = clie.Documento;
            cliente.NombreCompleto = clie.NombreCompleto;
            cliente.Correo = clie.Correo;
            cliente.Telefono = clie.Telefono;
            cliente.Estado = true;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return NotFound(e);
            }
            

            return NotFound();
        }
    }
}
