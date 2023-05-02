using Entidades;
using System;
using System.Data;
using System.Data.SqlClient;


namespace DAO
{
    public class UsuarioDAO
    {
        public bool AgregarUsuario(UsuarioEntidad usuario)
        {
            AccesoDatos acceso = new AccesoDatos();
            SqlCommand command = new SqlCommand();
            ArmarParametrosAgregar(ref command, usuario);
            if (acceso.ejecutarSP(command, "AgregarUsuario") == 0)
            { return false; }
            else
            {
                return true;
            }
        }

        public bool EliminaUsuario(string id)
        {
            AccesoDatos datos = new AccesoDatos();
            SqlCommand comando = new SqlCommand();
            SqlParameter parameter = new SqlParameter();
            parameter = comando.Parameters.Add("@Dni", SqlDbType.VarChar);
            parameter.Value = id;
            return Convert.ToBoolean(datos.ejecutarSP(comando, "EliminarUsuario"));
        }

        public void ArmarParametrosAgregar(ref SqlCommand command, UsuarioEntidad usuarios)
        {
            SqlParameter parametro = new SqlParameter();
            parametro = command.Parameters.Add("@dni", SqlDbType.VarChar);
            parametro.Value = usuarios.DniUsuario;
            parametro = command.Parameters.Add("@nombre", SqlDbType.VarChar);
            parametro.Value = usuarios.NombreUsuario;
            parametro = command.Parameters.Add("@apellido", SqlDbType.VarChar);
            parametro.Value = usuarios.ApellidoUsuario;
            parametro = command.Parameters.Add("@mail", SqlDbType.VarChar);
            parametro.Value = usuarios.EmailUsuario;
            parametro = command.Parameters.Add("@direccion", SqlDbType.VarChar);
            parametro.Value = usuarios.DireccionUsuario;
            parametro = command.Parameters.Add("@contra", SqlDbType.VarChar);
            parametro.Value = usuarios.Contra;
        }

        public bool EditarUsuario(UsuarioEntidad usuario, bool bol)
        {
            AccesoDatos datos = new AccesoDatos();
            SqlCommand comando = new SqlCommand();
            SqlParameter parameter = new SqlParameter();
            parameter = comando.Parameters.Add("@NuevaDireccion", SqlDbType.VarChar);
            parameter.Value = usuario.DireccionUsuario;
            parameter = comando.Parameters.Add("@NuevaContra", SqlDbType.NVarChar);
            parameter.Value = usuario.Contra; 
            parameter = comando.Parameters.Add("@NuevoNombre", SqlDbType.VarChar);
            parameter.Value = usuario.NombreUsuario;
            parameter = comando.Parameters.Add("@NuevoApellido", SqlDbType.VarChar);
            parameter.Value = usuario.ApellidoUsuario;
            parameter = comando.Parameters.Add("@NuevoMail", SqlDbType.NVarChar);
            parameter.Value = usuario.EmailUsuario;
            parameter = comando.Parameters.Add("@dni", SqlDbType.VarChar);
            parameter.Value = usuario.DniUsuario;
            return Convert.ToBoolean(datos.ejecutarSP(comando, "ModificarUsuario"));
        }

        public bool RolAdmin(string id, bool admin)
        {
            AccesoDatos datos = new AccesoDatos();
            SqlCommand cmd = new SqlCommand();
            SqlParameter param = new SqlParameter();
            param = cmd.Parameters.Add("@dni", SqlDbType.VarChar);
            param.Value = id;
            param = cmd.Parameters.Add("@admin", SqlDbType.Bit);
            param.Value = admin;
            return Convert.ToBoolean(datos.ejecutarSP(cmd, "modificarRolUsuario"));
        } 
    }
}
