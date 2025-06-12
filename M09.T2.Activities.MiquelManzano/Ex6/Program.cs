namespace Ex6
{
    internal static class Program
    {
        private static async Task Main(string[] args)
        {
            Console.WriteLine("Simulació seqüencial:");
            var tempsSeq = await Simulador.SimulacioSequencial();
            Console.WriteLine($"Temps total: {tempsSeq.TotalSeconds:F2} s\n");

            Console.WriteLine("Simulació paral·lela:");
            var tempsParal = await Simulador.SimulacioParalela();
            Console.WriteLine($"Temps total: {tempsParal.TotalSeconds:F2} s");
        }
    }
}