using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace chess_v2
{
    class Konj:Figura
    {
        public Konj()
        {

        }
        public Konj(bool a)
        {
            Vrsta = figurica.konj;
            if (a)
                Slika = Application.StartupPath + "\\80\\BlackKnight.png";
            else
                Slika= Application.StartupPath + "\\80\\WhiteKnight.png";
            boja = a;
        }
    }
}
