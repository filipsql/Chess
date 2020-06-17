using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace chess_v2
{
    class Kralj:Figura
    {

        public Kralj()
        {

        }
        public Kralj(bool a)
        {
            Vrsta = figurica.kralj;
            if(a)
                Slika= Application.StartupPath + "\\80\\BlackKing.png";
            else
                Slika= Application.StartupPath + "\\80\\WhiteKing.png";
            boja = a;
        }
    }
}
