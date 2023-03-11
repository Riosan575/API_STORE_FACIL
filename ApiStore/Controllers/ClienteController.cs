using ApiStore.DB;
using ApiStore.Models;
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

        [HttpPost]
        [Route("registrar")]
        public dynamic RegistrarCliente(Cliente cliente)
        {
            string mensaje = string.Empty;
            int clientes = new ClienteModelo().Registrar(cliente, out mensaje);

            return clientes;
        }
    }
}
