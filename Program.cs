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
                    Console.WriteLine($"\n✅ Bienvenido, {usuario}. Tu saldo actual es: {usuarioObj.Saldo:C}");
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
    }
}
