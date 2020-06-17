using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chess_v2
{
    sealed class Tabela
    {
        public Polja[] poljas { get; set; }
       

        public Tabela()
        {
            poljas = new Polja[64];
            for (int i = 0; i < 64; i++)
                poljas[i] = new Polja();
        }
        public void postaviFiguru(Figura a, int b)
        {
            poljas[b].figura = a;
        }
        public void preuzmiPolja(Tabela a)
        {
            for (int i = 0; i < 64; i++)
                this.poljas[i] = a.poljas[i];
        }
    }
}
