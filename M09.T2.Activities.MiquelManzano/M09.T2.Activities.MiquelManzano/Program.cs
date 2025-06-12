using Ex5;
namespace Ex5
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Simulació 1:");
            Simulacio.Run(
                bateriaInicial: 100000,
                dispositius: new List<Dispositiu>
                {
                    new Dispositiu("D1", 30000, 10000),
                    new Dispositiu("D2", 20000, 12000),
                    new Dispositiu("D3", 5000, 1000)
                });

            Console.WriteLine("\nSimulació 2:");
            Simulacio.Run(
                bateriaInicial: 100000,
                dispositius: new List<Dispositiu>
                {
                    new Dispositiu("D1", 25000, 23000),
                    new Dispositiu("D2", 20000, 12000),
                    new Dispositiu("D3", 8000, 1000),
                    new Dispositiu("D4", 10000, 1000)
                });
        }
    }
}