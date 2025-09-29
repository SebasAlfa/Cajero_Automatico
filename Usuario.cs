using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cajero_Automatico
{
    public class Usuario
    {
        public string NombreUsuario { get; set; }
        public string Contrasena { get; set; }
        public decimal Saldo { get; set; }

        public Usuario(string nombreUsuario, string contrasena, decimal saldo = 0)
        {
            NombreUsuario = nombreUsuario;
            Contrasena = contrasena;
            Saldo = saldo;
        }
    }
}
