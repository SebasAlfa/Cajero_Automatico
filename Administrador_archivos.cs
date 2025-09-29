using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cajero_Automatico
{
    public class Administrador_archivos
    {
        private List<Usuario> usuarios;
        private readonly string rutaArchivo;

        public Administrador_archivos()
        {
            rutaArchivo = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "usuarios.txt");
            CargarUsuarios();
        }

        private void CargarUsuarios()
        {
            usuarios = new List<Usuario>();

            if (File.Exists(rutaArchivo))
            {
                string[] lineas = File.ReadAllLines(rutaArchivo);

                foreach (string linea in lineas)
                {
                    string[] partes = linea.Split(';');
                    if (partes.Length == 3)
                    {
                        string nombre = partes[0];
                        string contrasena = partes[1];
                        decimal saldo = decimal.TryParse(partes[2], out decimal s) ? s : 0;
                        usuarios.Add(new Usuario(nombre, contrasena, saldo));
                    }
                }
            }
        }

        private void GuardarUsuarios()
        {
            List<string> lineas = usuarios
                .Select(u => $"{u.NombreUsuario};{u.Contrasena};{u.Saldo}")
                .ToList();

            File.WriteAllLines(rutaArchivo, lineas);
        }

        public bool IniciarSesion(string nombreUsuario, string contrasena)
        {
            return usuarios.Any(u =>
                u.NombreUsuario.Equals(nombreUsuario, StringComparison.OrdinalIgnoreCase) &&
                u.Contrasena == contrasena
            );
        }

        public bool RegistrarUsuario(string nombreUsuario, string contrasena)
        {
            if (string.IsNullOrWhiteSpace(nombreUsuario) || string.IsNullOrWhiteSpace(contrasena))
                return false;

            if (usuarios.Any(u => u.NombreUsuario.Equals(nombreUsuario, StringComparison.OrdinalIgnoreCase)))
                return false;

            usuarios.Add(new Usuario(nombreUsuario.Trim(), contrasena, 0)); // saldo inicial en 0
            GuardarUsuarios();
            return true;
        }

        // Método auxiliar para obtener un usuario por nombre
        public Usuario ObtenerUsuario(string nombreUsuario)
        {
            return usuarios.FirstOrDefault(u =>
                u.NombreUsuario.Equals(nombreUsuario, StringComparison.OrdinalIgnoreCase));
        }

        // Método para guardar cambios de saldo
        public void ActualizarSaldo(string nombreUsuario, decimal nuevoSaldo)
        {
            var usuario = ObtenerUsuario(nombreUsuario);
            if (usuario != null)
            {
                usuario.Saldo = nuevoSaldo;
                GuardarUsuarios();
            }
        }
    }
}
