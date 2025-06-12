using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex6
{
    class Tasca
    {
        public string Nom { get; }
        public int Durada { get; } // en segons
        public List<Tasca> Dependències { get; }

        public Tasca(string nom, int durada)
        {
            Nom = nom;
            Durada = durada;
            Dependències = new List<Tasca>();
        }

        public void AfegirDependencia(Tasca t) => Dependències.Add(t);

        public async Task Executar()
        {
            Console.WriteLine($"Iniciant: {Nom} ({Durada}s)");
            await Task.Delay(Durada * 1000);
            Console.WriteLine($"Finalitzada: {Nom}");
        }
    }
}
