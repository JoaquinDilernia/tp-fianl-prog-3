using DAO;
using Entidades;
using System;
using System.Data;


namespace Negocio
{
    public class UsuarioNegocio
    {
        public bool AgregarUsuario(UsuarioEntidad usu)
        {
            UsuarioDAO usuario = new UsuarioDAO();
            return usuario.AgregarUsuario(usu);
        }

        public bool VerificarDni(UsuarioEntidad usuario)
        {
            string consulta = "Select * from Usuario where DniUsuario = '" + usuario.DniUsuario + "'";
            AccesoDatos acc = new AccesoDatos();
            return acc.existe(consulta);
        }

        public bool VerificarMail(UsuarioEntidad usuario)
        {
            string consulta = "Select * from Usuario where Mail = '" + usuario.EmailUsuario + "'";
            AccesoDatos acc = new AccesoDatos();
            return acc.existe(consulta);
        }

        public DataTable CargarGrv(string consulta)
        {
            AccesoDatos acc = new AccesoDatos();
            return acc.ObtenerTabla("Usuario", consulta);
        }

        public bool BorrarUsuario(string dni)
        {
            UsuarioDAO usuario = new UsuarioDAO();
            return usuario.EliminaUsuario(dni);
        }

        public bool EditarUsuario(UsuarioEntidad usuario, bool bol)
        {
            UsuarioDAO usuarioDAO = new UsuarioDAO();
            return usuarioDAO.EditarUsuario(usuario, bol);
        }

        public bool Logeo(UsuarioEntidad usuarioEntidad)
        {
            string consulta = "Select * From Usuario where Mail = '" + usuarioEntidad.EmailUsuario + "' AND contraseña = '" + usuarioEntidad.Contra + "'";
            AccesoDatos datos = new AccesoDatos();
            return datos.existe(consulta);
        }

        public DataTable ObtenerUsuario(UsuarioEntidad usuario)
        {
            string consulta = "Select * from Usuario where Mail = '" + usuario.EmailUsuario + "' and Contraseña = '" + usuario.Contra + "'";
            AccesoDatos acc = new AccesoDatos();
            return acc.ObtenerTabla("Usuario", consulta);
        }

        public bool EditarRolUsuario(string id, Boolean admin)
        {
            UsuarioDAO usuarios = new UsuarioDAO();
            return usuarios.RolAdmin(id, admin);
        }
    }
}
