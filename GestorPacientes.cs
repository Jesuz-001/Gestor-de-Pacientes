using System;
using System.Collections.Generic;
using System.Linq;

namespace SistemaGestionPacientes
{
    public class GestorPacientes
    {
        private List<Paciente> listaPacientes = new List<Paciente>();

        public bool ExisteId(string id)
        {
            return listaPacientes.Any(p => p.Id.Equals(id, StringComparison.OrdinalIgnoreCase));
        }

        public void RegistrarPaciente()
        {
            Console.Clear();
            Console.WriteLine("=== REGISTRAR NUEVO PACIENTE ===");

            string id;
            do
            {
                Console.Write("Ingrese ID/Cédula: ");
                id = Console.ReadLine()?.Trim();
                if (string.IsNullOrWhiteSpace(id))
                {
                    Console.WriteLine("El ID no puede estar vacío.");
                }
                else if (ExisteId(id))
                {
                    Console.WriteLine("Error: Ya existe un paciente registrado con este ID.");
                    id = null;
                }
            } while (string.IsNullOrWhiteSpace(id));

            string nombre;
            do
            {
                Console.Write("Ingrese Nombre Completo: ");
                nombre = Console.ReadLine()?.Trim();
            } while (string.IsNullOrWhiteSpace(nombre));

            int edad;
            while (true)
            {
                Console.Write("Ingrese Edad: ");
                if (int.TryParse(Console.ReadLine(), out edad) && edad >= 0 && edad <= 120)
                    break;
                Console.WriteLine("Por favor, ingrese una edad válida.");
            }

            string sexo;
            do
            {
                Console.Write("Ingrese Sexo (M/F/Otro): ");
                sexo = Console.ReadLine()?.Trim();
            } while (string.IsNullOrWhiteSpace(sexo));

            string diagnostico;
            do
            {
                Console.Write("Ingrese Diagnóstico: ");
                diagnostico = Console.ReadLine()?.Trim();
            } while (string.IsNullOrWhiteSpace(diagnostico));

            Paciente nuevoPaciente = new Paciente(id, nombre, edad, sexo, diagnostico, DateTime.Now);
            listaPacientes.Add(nuevoPaciente);

            Console.WriteLine("\n¡Paciente registrado con éxito!");
        }

        public void ListarPacientes()
        {
            Console.Clear();
            Console.WriteLine("=== LISTADO DE PACIENTES ===");

            if (listaPacientes.Count == 0)
            {
                Console.WriteLine("No hay pacientes registrados en el sistema.");
                return;
            }

            foreach (var paciente in listaPacientes)
            {
                Console.WriteLine(paciente.ToString());
            }
        }

        public void BuscarPaciente()
        {
            Console.Clear();
            Console.WriteLine("=== BUSCAR PACIENTE ===");

            if (listaPacientes.Count == 0)
            {
                Console.WriteLine("No hay pacientes registrados en el sistema.");
                return;
            }

            Console.Write("Ingrese ID o Nombre a buscar: ");
            string criterio = Console.ReadLine()?.Trim();

            var resultados = listaPacientes.Where(p => 
                p.Id.Equals(criterio, StringComparison.OrdinalIgnoreCase) || 
                p.NombreCompleto.IndexOf(criterio, StringComparison.OrdinalIgnoreCase) >= 0
            ).ToList();

            if (resultados.Count > 0)
            {
                Console.WriteLine($"\nSe encontraron {resultados.Count} coincidencia(s):");
                foreach (var p in resultados)
                {
                    Console.WriteLine(p.ToString());
                }
            }
            else
            {
                Console.WriteLine("\nNo se encontraron pacientes que coincidan con el criterio.");
            }
        }

        public void ActualizarPaciente()
        {
            Console.Clear();
            Console.WriteLine("=== ACTUALIZAR DATOS DE PACIENTE ===");

            Console.Write("Ingrese el ID del paciente a modificar: ");
            string id = Console.ReadLine()?.Trim();

            Paciente paciente = listaPacientes.FirstOrDefault(p => p.Id.Equals(id, StringComparison.OrdinalIgnoreCase));

            if (paciente == null)
            {
                Console.WriteLine("Error: Paciente no encontrado.");
                return;
            }

            Console.WriteLine($"\nPaciente encontrado: {paciente}");
            Console.WriteLine("Deje el campo en blanco si desea mantener el valor actual.\n");

            Console.Write($"Nuevo Nombre [{paciente.NombreCompleto}]: ");
            string nuevoNombre = Console.ReadLine()?.Trim();
            if (!string.IsNullOrWhiteSpace(nuevoNombre)) paciente.NombreCompleto = nuevoNombre;

            Console.Write($"Nueva Edad [{paciente.Edad}]: ");
            string nuevaEdadStr = Console.ReadLine()?.Trim();
            if (int.TryParse(nuevaEdadStr, out int nuevaEdad) && nuevaEdad >= 0) paciente.Edad = nuevaEdad;

            Console.Write($"Nuevo Sexo [{paciente.Sexo}]: ");
            string nuevoSexo = Console.ReadLine()?.Trim();
            if (!string.IsNullOrWhiteSpace(nuevoSexo)) paciente.Sexo = nuevoSexo;

            Console.Write($"Nuevo Diagnóstico [{paciente.Diagnostico}]: ");
            string nuevoDiagnostico = Console.ReadLine()?.Trim();
            if (!string.IsNullOrWhiteSpace(nuevoDiagnostico)) paciente.Diagnostico = nuevoDiagnostico;

            Console.WriteLine("\n¡Datos del paciente actualizados exitosamente!");
        }

        public void EliminarPaciente()
        {
            Console.Clear();
            Console.WriteLine("=== ELIMINAR PACIENTE ===");

            Console.Write("Ingrese el ID del paciente a eliminar: ");
            string id = Console.ReadLine()?.Trim();

            Paciente paciente = listaPacientes.FirstOrDefault(p => p.Id.Equals(id, StringComparison.OrdinalIgnoreCase));

            if (paciente == null)
            {
                Console.WriteLine("Error: Paciente no encontrado.");
                return;
            }

            Console.WriteLine($"\nPaciente encontrado: {paciente}");
            Console.Write("¿Está seguro de que desea eliminar este paciente? (S/N): ");
            string confirmacion = Console.ReadLine()?.Trim().ToUpper();

            if (confirmacion == "S")
            {
                listaPacientes.Remove(paciente);
                Console.WriteLine("\n¡Paciente eliminado correctamente!");
            }
            else
            {
                Console.WriteLine("\nOperacion cancelada.");
            }
        }
    }
}