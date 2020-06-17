using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace chess_v2
{
    class Lovac:Figura
    {
        public Lovac() { }
        public Lovac(bool a)
        {
            Vrsta = figurica.lovac;
            if(a)
                Slika= Application.StartupPath+"\\80\\BlackBishop.png";
            else
                Slika= Application.StartupPath + "\\80\\WhiteBishop.png";
            boja = a;
        }
    }
}
