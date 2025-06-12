using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex6
{
    static class Simulador
    {
        public static async Task<TimeSpan> SimulacioSequencial()
        {
            var sw = Stopwatch.StartNew();

            await ExecutarTasca("Batre la massa", 8);
            await ExecutarTasca("Pre-escalfar forn", 10);
            await ExecutarTasca("Enfornar", 15);
            await ExecutarTasca("Preparar cobertura", 5);
            await ExecutarTasca("Refredar base", 4);
            await ExecutarTasca("Glassejar", 3);
            await ExecutarTasca("Decorar", 2);

            sw.Stop();
            return sw.Elapsed;
        }

        public static async Task<TimeSpan> SimulacioParalela()
        {
            var sw = Stopwatch.StartNew();

            var batre = new Tasca("Batre la massa", 8);
            var preescalfar = new Tasca("Pre-escalfar forn", 10);
            var enfornar = new Tasca("Enfornar", 15);
            var cobertura = new Tasca("Preparar cobertura", 5);
            var refredar = new Tasca("Refredar base", 4);
            var glassejar = new Tasca("Glassejar", 3);
            var decorar = new Tasca("Decorar", 2);

            enfornar.AfegirDependencia(batre);
            enfornar.AfegirDependencia(preescalfar);

            refredar.AfegirDependencia(enfornar);

            glassejar.AfegirDependencia(cobertura);
            glassejar.AfegirDependencia(refredar);

            decorar.AfegirDependencia(glassejar);

            await ExecutarAmbDependències(new[] { batre, preescalfar, cobertura, enfornar, refredar, glassejar, decorar });

            sw.Stop();
            return sw.Elapsed;
        }

        private static async Task ExecutarTasca(string nom, int durada)
        {
            Console.WriteLine($"Iniciant: {nom} ({durada}s)");
            await Task.Delay(durada * 1000);
            Console.WriteLine($"Finalitzada: {nom}");
        }

        private static async Task ExecutarAmbDependències(IEnumerable<Tasca> tasques)
        {
            var tasquesExecutades = new Dictionary<Tasca, Task>();

            foreach (var tasca in tasques)
            {
                ExecutarTascaAmbDeps(tasca, tasquesExecutades);
            }

            await Task.WhenAll(tasquesExecutades.Values);
        }

        private static Task ExecutarTascaAmbDeps(Tasca tasca, Dictionary<Tasca, Task> execucions)
        {
            if (execucions.ContainsKey(tasca)) return execucions[tasca];

            var tasquesPendents = tasca.Dependències.Select(d => ExecutarTascaAmbDeps(d, execucions));
            var nova = Task.WhenAll(tasquesPendents).ContinueWith(async _ => await tasca.Executar()).Unwrap();

            execucions[tasca] = nova;
            return nova;
        }
    }
}
