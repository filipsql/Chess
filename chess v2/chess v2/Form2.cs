using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace chess_v2
{
    public partial class Form2 : Form
    {
        public Figura fig { get; set; }
        bool boja;
        public Form2()
        {
            InitializeComponent();
            fig = new Figura();

        }
        public Form2(bool a)
            :this()
        {
            InitializeComponent();
            boja = a;
        }
        public int vratiFig()
        {
            return (int)fig.Vrsta;
        }
        private void Form2_Load(object sender, EventArgs e)
        {
            
        }

        private void kraljica_Click(object sender, EventArgs e)
        {
            fig = new Kraljica(boja);
            Close();
        }

        private void top_Click(object sender, EventArgs e)
        {
            fig = new top(boja);
            Close();
        }

        private void konj_Click(object sender, EventArgs e)
        {
            //   return ko;
            fig = new Konj(boja);
            Close();
        }

        private void lovac_Click(object sender, EventArgs e)
        {
            fig = new Lovac(boja);
            Close();
           // return lv;
        }
    }
}
