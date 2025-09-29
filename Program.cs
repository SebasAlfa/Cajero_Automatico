using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cajero_Automatico
{
    class Program
    {
        static Administrador_archivos admin = new Administrador_archivos();

        static void Main(string[] args)
        {
            bool salir = false;

            while (!salir)
            {
                Console.Clear();
                Console.WriteLine(" === CAJERO AUTOMATICO === ");
                Console.WriteLine("1. Iniciar sesión");
                Console.WriteLine("2. Registrar usuario");
                Console.WriteLine("3. Salir");
                Console.Write("\nSeleccione una opción: ");

                string opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        IniciarSesion();
                        Console.ReadKey();
                        break;

                    case "2":
                        RegistrarUsuario();
                        Console.ReadKey();
                        break;

                    case "3":
                        Console.WriteLine("\n[Saliendo del sistema]");
                        salir = true;
                        Console.ReadKey();
                        break;

                    default:
                        Console.WriteLine("\n❌ Opción no válida.");
                        Console.ReadKey();
                        break;
                }
            }
        }

        static void IniciarSesion()
        {
            Console.Clear();
            Console.WriteLine("=== Iniciar sesión ===");
            Console.Write("Usuario: ");
            string usuario = Console.ReadLine()?.Trim();

            Console.Write("Contraseña: ");
            string contrasena = Console.ReadLine();

            bool acceso = admin.IniciarSesion(usuario, contrasena);

            if (acceso)
            {
                Usuario usuarioObj = admin.ObtenerUsuario(usuario);
                if (usuarioObj != null)
                {
                    MenuUsuario(usuarioObj); // ⬅ Aquí llamamos al menú del usuario autenticado
                }
                else
                {
                    Console.WriteLine("\n❌ Error interno: usuario no encontrado.");
                }
            }
            else
            {
                Console.WriteLine("\n❌ Usuario o contraseña incorrectos.");
            }
        }

        static void RegistrarUsuario()
        {
            Console.Clear();
            Console.WriteLine("=== Registrar nuevo usuario ===");
            Console.Write("Ingrese nombre de usuario: ");
            string usuario = Console.ReadLine()?.Trim();

            Console.Write("Ingrese contraseña: ");
            string contrasena = Console.ReadLine();

            Console.Write("Confirme contraseña: ");
            string confirm = Console.ReadLine();

            if (contrasena != confirm)
            {
                Console.WriteLine("\n❌ Las contraseñas no coinciden.");
                return;
            }

            bool registrado = admin.RegistrarUsuario(usuario, contrasena);

            if (registrado)
                Console.WriteLine("\n✅ Usuario registrado con éxito. Saldo inicial: $0");
            else
                Console.WriteLine("\n❌ El nombre de usuario ya existe o los datos son inválidos.");
        }

        static void Depositar(Usuario usuario)
        {
            Console.Write("\nIngrese el monto a depositar: ");
            if (decimal.TryParse(Console.ReadLine(), out decimal monto) && monto > 0)
            {
                usuario.Saldo += monto;
                admin.ActualizarSaldo(usuario.NombreUsuario, usuario.Saldo);
                admin.GuardarMovimiento(usuario.NombreUsuario, "Deposito", monto, usuario.Saldo);
                Console.WriteLine($"\n✅ Depósito exitoso. Nuevo saldo: {usuario.Saldo:C}");
            }
            else
            {
                Console.WriteLine("\n❌ Monto inválido.");
            }
            Console.ReadKey();
        }

        static void Retirar(Usuario usuario)
        {
            Console.Write("\nIngrese el monto a retirar: ");
            if (decimal.TryParse(Console.ReadLine(), out decimal monto) && monto > 0)
            {
                if (monto <= usuario.Saldo)
                {
                    usuario.Saldo -= monto;
                    admin.ActualizarSaldo(usuario.NombreUsuario, usuario.Saldo);
                    admin.GuardarMovimiento(usuario.NombreUsuario, "Retiro", monto, usuario.Saldo);
                    Console.WriteLine($"\n✅ Retiro exitoso. Nuevo saldo: {usuario.Saldo:C}");
                }
                else
                {
                    Console.WriteLine("\n❌ Saldo insuficiente.");
                }
            }
            else
            {
                Console.WriteLine("\n❌ Monto inválido.");
            }
            Console.ReadKey();
        }

        static void CambiarContrasena(Usuario usuario)
        {
            Console.Write("\nIngrese la nueva contraseña: ");
            string nueva = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(nueva))
            {
                usuario.Contrasena = nueva;
                admin.ActualizarContrasena(usuario.NombreUsuario, nueva);
                Console.WriteLine("\n✅ Contraseña cambiada exitosamente.");
            }
            else
            {
                Console.WriteLine("\n❌ Contraseña inválida.");
            }
            Console.ReadKey();
        }

        static void MenuUsuario(Usuario usuario)
        {
            bool salir = false;

            while (!salir)
            {
                Console.Clear();
                Console.WriteLine($"=== Bienvenido {usuario.NombreUsuario} ===");
                Console.WriteLine("1. Consultar saldo");
                Console.WriteLine("2. Depositar dinero");
                Console.WriteLine("3. Retirar dinero");
                Console.WriteLine("4. Cambiar contraseña");
                Console.WriteLine("5. Cerrar sesión");
                Console.WriteLine("6. Ver historial de movimientos");

                Console.Write("\nSeleccione una opción: ");

                string opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        Console.WriteLine($"\nTu saldo actual es: {usuario.Saldo:C}");
                        Console.ReadKey();
                        break;

                    case "2":
                        Depositar(usuario);
                        break;

                    case "3":
                        Retirar(usuario);
                        break;

                    case "4":
                        CambiarContrasena(usuario);
                        break;

                    case "5":
                        Console.WriteLine("\nCerrando sesión...");
                        salir = true;
                        break;
                    case "6":
                        MostrarHistorial(usuario);
                        break;


                    default:
                        Console.WriteLine("\n❌ Opción no válida.");
                        break;
                }
            }
        }

        static void MostrarHistorial(Usuario usuario)
        {
            var historial = admin.ObtenerHistorial(usuario.NombreUsuario);

            Console.WriteLine("\n=== Historial de Movimientos ===");
            if (historial.Count == 0)
            {
                Console.WriteLine("No hay movimientos registrados.");
            }
            else
            {
                foreach (var linea in historial)
                    Console.WriteLine(linea);
            }
            Console.ReadKey();
        }



    }
}
