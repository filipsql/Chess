using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chess_v2
{
    class Player
    {
        public bool naPotezu { get; set; }//true jeste, false nije
        public int indexKralja { get; set; }
        public int brojFigurica { get; set; }
        public bool boja { get; set; }

        //optimalnije mi je napraviti kod tako da se negde pamti index kralja, nego kralja traziti pri svakom potezu. jer se njegov index mora znati
        //unapred da bi smo izraacunali ko mu sve daje potencijalni "sah". Jer ukoliko npr. beli igrac napravi los potez, i dovede automatski kralja
        //u poziciji da mu neka figura da "sah" taj potez se smatra neregularnim. Da bismo registrovali takve poteze, mora se pre svakog izvrsenja poteza
        //proveravati da li neko "preti" kralju, sto je lakse uciniti ako unapred namo njegov index, nego da ga svaki put "trazimo" po "tabli".
        public Player() { }
        public Player(bool a)
        {
            if (a)
                indexKralja = 60;
            else
                indexKralja = 4;
            naPotezu = a;
            boja = !a;
            brojFigurica = 16;
        }
    }
}
