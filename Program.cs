using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cajero_Automatico
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool salir = false;

            while (!salir) 
            {
                MostrarMenu();
                string opcion = Console.ReadLine()?.Trim();

                switch (opcion)
                {
                    case "1":
                        IniciarSesion();
                        break;
                    case "2":
                        RegistrarUsuarioEsqueleto();
                        break;
                    case "3":
                        Console.WriteLine("\n[Saliendo del sistema]");
                        salir = true;
                        break;
                    default:
                        Console.WriteLine("\n❌ Opción no válida.");
                        break;
                }

                if (!salir)
                {
                    Console.WriteLine("\nPresione una tecla para continuar...");
                    Console.ReadKey();
                }
            }
        }
        static void MostrarMenu()
        {
            Console.Clear();
            Console.WriteLine(" === CAJERO AUTOMATICO === ");
            Console.WriteLine("1. Iniciar sesión");
            Console.WriteLine("2. Registrar usuario");
            Console.WriteLine("3. Salir");
            Console.Write("\nSeleccione una opción: ");
        }

        // ESQUELETO: aquí NO implementamos la lógica real aún.
        // En la siguiente rama implementaremos la clase Administrador_archivos y la lógica real.
        static Administrador_archivos admin = new Administrador_archivos();

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
                Console.WriteLine("\n✅ Bienvenido, inicio de sesión exitoso.");
            else
                Console.WriteLine("\n❌ Usuario o contraseña incorrectos (o no hay usuarios registrados).");
        }

        static void RegistrarUsuarioEsqueleto()
        {
            Console.Clear();
            Console.WriteLine("=== Registrar usuario (esqueleto) ===");
            Console.WriteLine("\n➡️  Esto es sólo el esqueleto del menú. Implementaremos el registro real en la rama 'feature/registro'.");
        }
    }
}
