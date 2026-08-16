using System;

namespace SistemaGestionPacientes
{
    class Program
    {
        static void Main(string[] args)
        {
            GestorPacientes gestor = new GestorPacientes();
            bool salir = false;

            while (!salir)
            {
                Console.Clear();
                Console.WriteLine("=====================================");
                Console.WriteLine("   SISTEMA DE GESTION DE PACIENTES   ");
                Console.WriteLine("=====================================");
                Console.WriteLine("1. Registrar nuevo paciente");
                Console.WriteLine("2. Listar todos los pacientes");
                Console.WriteLine("3. Buscar paciente por ID o nombre");
                Console.WriteLine("4. Actualizar datos de un paciente");
                Console.WriteLine("5. Eliminar un paciente");
                Console.WriteLine("6. Salir del sistema");
                Console.WriteLine("=====================================");
                Console.Write("Seleccione una opcion: ");

                string opcion = Console.ReadLine()?.Trim();

                switch (opcion)
                {
                    case "1":
                        EjecutarTransaccion(() => gestor.RegistrarPaciente(), "¿Desea registrar otro paciente? (S/N): ");
                        break;
                    case "2":
                        EjecutarTransaccion(() => gestor.ListarPacientes(), "¿Desea ver el listado de nuevo? (S/N): ");
                        break;
                    case "3":
                        EjecutarTransaccion(() => gestor.BuscarPaciente(), "¿Desea realizar otra busqueda? (S/N): ");
                        break;
                    case "4":
                        EjecutarTransaccion(() => gestor.ActualizarPaciente(), "¿Desea actualizar otro paciente? (S/N): ");
                        break;
                    case "5":
                        EjecutarTransaccion(() => gestor.EliminarPaciente(), "¿Desea eliminar otro paciente? (S/N): ");
                        break;
                    case "6":
                        salir = true;
                        Console.WriteLine("\n¡Gracias por usar el sistema! Presione cualquier tecla para salir...");
                        Console.ReadKey();
                        break;
                    default:
                        Console.WriteLine("\nOpción no válida. Presione cualquier tecla para intentar de nuevo...");
                        Console.ReadKey();
                        break;
                }
            }
        }

        static void EjecutarTransaccion(Action accion, string mensajeRepeticion)
        {
            bool repetir = true;
            while (repetir)
            {
                accion();
                Console.Write($"\n{mensajeRepeticion}");
                string respuesta = Console.ReadLine()?.Trim().ToUpper();
                if (respuesta != "S")
                {
                    repetir = false;
                }
            }
        }
    }
}