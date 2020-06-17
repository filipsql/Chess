using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace chess_v2
{
    class Pijun:Figura
    {
        public Pijun()
        {
        }
        public Pijun(bool a)
        {
            Vrsta = figurica.pijun;
            if(a)
                Slika = Application.StartupPath + "\\80\\BlackPawn.png";
            else
                Slika= Application.StartupPath + "\\80\\WhitePawn.png";
             boja = a;
        }
    }
}
