using ApiStore.Models;

public class ClienteModelo
{
    private ClienteDao clie = new ClienteDao();
    public List<Cliente> ListCliente()
    {
        return clie.listCliente();
    }
    public int Registrar(Cliente obj, out string Mensaje)
    {
        Mensaje = string.Empty;

        if (obj.Documento == "")
        {
            Mensaje += "Es necesario el documento del Cliente\n";
        }

        if (obj.NombreCompleto == "")
        {
            Mensaje += "Es necesario el nombre completo del Cliente\n";
        }

        if (obj.Correo == "")
        {
            Mensaje += "Es necesario el correo del Cliente\n";
        }

        if (Mensaje != string.Empty)
        {
            return 0;
        }
        else
        {
            return clie.Registrar(obj, out Mensaje);
        }
    }
}
