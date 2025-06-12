using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex5
{
    class Dispositiu
    {
        private readonly string id;
        private readonly int capacitat;
        private readonly int consumPerSegon;
        private int càrregues = 0;

        public Dispositiu(string id, int capacitat, int consumPerSegon)
        {
            this.id = id;
            this.capacitat = capacitat;
            this.consumPerSegon = consumPerSegon;
        }

        public void CarregarBateria(BateriaAuxiliar bateria)
        {
            while (true)
            {
                if (!bateria.IntentaCarregar(capacitat))
                    break;

                càrregues++;

                // Simula el consum de bateria
                int segons = capacitat / consumPerSegon;
                for (int i = 0; i < segons; i++)
                {
                    Thread.Sleep(1000); // 1 segon
                }
            }

            Console.WriteLine($"{id} ha carregat {càrregues} vegades.");
        }
    }
}
