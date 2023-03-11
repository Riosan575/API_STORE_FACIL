using ApiStore.DB;
using ApiStore.Models;
using Microsoft.Data.SqlClient;
using System.Data;

public class ClienteDao : ConexionDB
{
    public List<Cliente> listCliente()
    {
        SqlCommand cmd = null;
        List<Cliente> lista = new List<Cliente>();

        try
        {
            SqlConnection cn = GetConnection();
            cmd = new SqlCommand("lisCliente", cn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Cliente enCliente = new Cliente();
                enCliente.IdCliente = Convert.ToInt32(dr["IdCliente"]);
                enCliente.Documento = dr["Documento"].ToString();
                enCliente.NombreCompleto = dr["NombreCompleto"].ToString();
                enCliente.Correo = dr["Correo"].ToString();
                enCliente.Telefono = dr["Telefono"].ToString();
                enCliente.Estado = Convert.ToBoolean(dr["Estado"]);
                enCliente.FechaRegistro = Convert.ToDateTime(dr["FechaRegistro"]);
                lista.Add(enCliente);

            }
        }
        catch (Exception e)
        {
            throw e;
        }
        finally
        {
            cmd.Connection.Close();
        }
        return lista;
    }
    public int Registrar(Cliente obj, out string Mensaje)
    {
        int idClientegenerado = 0;
        Mensaje = string.Empty;
        try
        {

            using (var conexion = GetConnection())
            {

                SqlCommand cmd = new SqlCommand("SP_RegistrarCiente", conexion);
                cmd.Parameters.AddWithValue("Documento", obj.Documento);
                cmd.Parameters.AddWithValue("NombreCompleto", obj.NombreCompleto);
                cmd.Parameters.AddWithValue("Correo", obj.Correo);
                cmd.Parameters.AddWithValue("Telefono", obj.Telefono);
                cmd.Parameters.AddWithValue("Estado", obj.Estado);
                cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                cmd.CommandType = CommandType.StoredProcedure;

                conexion.Open();

                cmd.ExecuteNonQuery();

                idClientegenerado = Convert.ToInt32(cmd.Parameters["Resultado"].Value);
                Mensaje = cmd.Parameters["Mensaje"].Value.ToString();

            }

        }
        catch (Exception ex)
        {
            idClientegenerado = 0;
            Mensaje = ex.Message;
        }



        return idClientegenerado;
    }
}
