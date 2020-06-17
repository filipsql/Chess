using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chess_v2
{
    class Polja
    {
        public Figura figura { get; set; }
        //  public bool bojaP { get; set; }// dal je polje crno ili belo
        public bool zauzeto { get; set; }
        public Polja()
        {
            figura = new Figura();
            zauzeto = false;
        }
        public Polja(Figura a)
            :this()
        {
            figura = new Figura(a);
            zauzeto = true;
        }


    }
}
