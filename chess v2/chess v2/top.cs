using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace chess_v2
{
    class top : Figura
    {
        
        public top()
        {

        }
        public top(bool a)
        {
            Vrsta = figurica.top;
            if (a)
                Slika = Application.StartupPath + "\\80\\BlackRook.png";
            else
                Slika = Application.StartupPath + "\\80\\WhiteRook.png";
            boja = a;
        }
    }
}
