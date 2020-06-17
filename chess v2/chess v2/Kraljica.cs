using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace chess_v2
{
    class Kraljica:Figura
    {
        public Kraljica() { }
        public Kraljica(bool a)
        {
            Vrsta = figurica.kraljica;
            if(a)
                Slika= Application.StartupPath+"\\80\\BlackQueen.png";
            else
                Slika = Application.StartupPath + "\\80\\WhiteQueen.png";
            boja = a;
        }
    }
}
