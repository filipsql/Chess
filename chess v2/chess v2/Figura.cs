using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chess_v2
{
    public enum figurica
    {
        pijun = 1,
        top,
        lovac,
        konj,
        kraljica,
        kralj
    }

    public class Figura
    {
        public figurica Vrsta { get; set; }
        public String Slika { get; set; }
        public bool boja { get; set; }//true crna, false bela.
        public bool dalijepomren { get; set; }
        public Figura()
        {
            Vrsta = 0;
            Slika = null;
            boja = false;
            dalijepomren = false;
        }
        public Figura(figurica a, String b, bool c)
        {
            Vrsta = a;
            Slika = b;
            boja = c;
        }
        public Figura(Figura a)
        {
            this.Vrsta = a.Vrsta;
            this.Slika = a.Slika;
            this.boja = a.boja;
            this.dalijepomren = false;
        }
        
    }
}
