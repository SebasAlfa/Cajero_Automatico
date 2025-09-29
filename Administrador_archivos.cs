using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cajero_Automatico
{
    public class Administrador_archivos
    {
        private List<Usuario> usuarios;

        public Administrador_archivos()
        {
            // Ahora la lista comienza vacía
            usuarios = new List<Usuario>();
        }

        // Método para iniciar sesión (si no hay usuarios registrados, siempre falla)
        public bool IniciarSesion(string nombreUsuario, string contrasena)
        {
            return usuarios.Any(u =>
                u.NombreUsuario.Equals(nombreUsuario, StringComparison.OrdinalIgnoreCase) &&
                u.Contrasena == contrasena
            );
        }

        // Este método lo implementaremos en la rama "feature/registro"
        public bool RegistrarUsuario(string nombreUsuario, string contrasena)
        {
            if (string.IsNullOrWhiteSpace(nombreUsuario) || string.IsNullOrWhiteSpace(contrasena))
                return false;

            // Revisar si ya existe un usuario con ese nombre
            if (usuarios.Any(u => u.NombreUsuario.Equals(nombreUsuario, StringComparison.OrdinalIgnoreCase)))
                return false;

            usuarios.Add(new Usuario(nombreUsuario.Trim(), contrasena));
            return true;
        }
    }
}
