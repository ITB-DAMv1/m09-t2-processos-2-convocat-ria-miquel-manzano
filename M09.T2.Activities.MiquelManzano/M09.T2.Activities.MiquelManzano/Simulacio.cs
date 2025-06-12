using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex5
{
    static class Simulacio
    {
        public static void Run(int bateriaInicial, List<Dispositiu> dispositius)
        {
            BateriaAuxiliar bateria = new BateriaAuxiliar(bateriaInicial);
            List<Thread> fils = new List<Thread>();
            Stopwatch sw = Stopwatch.StartNew();

            foreach (var dispositiu in dispositius)
            {
                var thread = new Thread(() => dispositiu.CarregarBateria(bateria));
                fils.Add(thread);
                thread.Start();
            }

            // Espera a que tots els fils finalitzin
            foreach (var thread in fils)
            {
                thread.Join();
            }

            sw.Stop();
            Console.WriteLine($"\nTemps total: {sw.Elapsed.TotalSeconds:F2} segons");
            Console.WriteLine($"Bateria restant: {bateria.CàrregaRestant} mAh");
        }
    }
}
