using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex5
{
    class BateriaAuxiliar
    {
        private int capacitat;
        private readonly object _lock = new object();

        public BateriaAuxiliar(int capacitat)
        {
            this.capacitat = capacitat;
        }

        public int CàrregaRestant
        {
            get
            {
                lock (_lock)
                {
                    return capacitat;
                }
            }
        }

        public bool IntentaCarregar(int quantitat)
        {
            lock (_lock)
            {
                if (capacitat >= quantitat)
                {
                    capacitat -= quantitat;
                    return true;
                }
                return false;
            }
        }
    }
}
