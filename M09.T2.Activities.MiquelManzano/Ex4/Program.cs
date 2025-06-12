using System.Diagnostics;

namespace Ex4
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            string rutaFitxer = "../../../processos.txt";
            int totalProcessos = 0;
            int totalFils = 0;

            using (StreamWriter writer = new StreamWriter(rutaFitxer))
            {
                writer.WriteLine("Llista de processos actius i els seus fils:\n");

                foreach (Process p in Process.GetProcesses())
                {
                    try
                    {
                        writer.WriteLine($"Procés: {p.ProcessName} (ID: {p.Id})");
                        writer.WriteLine("  Fils:");

                        foreach (ProcessThread t in p.Threads)
                        {
                            writer.WriteLine($"    - Thread ID: {t.Id}");
                            totalFils++;
                        }

                        writer.WriteLine();
                        totalProcessos++;
                    }
                    catch (Exception ex)
                    {
                        writer.WriteLine($"[Error accedint al procés {p.ProcessName} (ID: {p.Id})]: {ex.Message}");
                    }
                }

                writer.WriteLine("Resum:");
                writer.WriteLine($"Total de processos: {totalProcessos}");
                writer.WriteLine($"Total de fils: {totalFils}");
            }

            Console.WriteLine("Informació guardada a 'processos.txt'");
            Console.WriteLine($"Total de processos: {totalProcessos}");
            Console.WriteLine($"Total de fils: {totalFils}");
        }
    }
}