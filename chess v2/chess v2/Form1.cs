using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Forms;

namespace chess_v2
{
    public partial class Form1 : Form
    {
        private Tabela table;
        private Tabela bekap;//tabela gde ce se pamtiti prosli potezi, za slucaj da zelimo da vratimo potez.
        private Player player1;
        private Player player2;
        private Stopwatch s1, s2;
        Button b;
        int bekapK;
        Button bekapA;//Neke funkcije se mogu spojiti u jednu, mozda bi kod tako bio efektivniji. Ali s obzirom na ograniceno vreme, hteo sam da dobijem vise na funkcionalnosti, da bih zaobiso moguce bugove.
        Button bekapB;
        public Form1()
        {
            b = new Button();
            bekapA = new Button();
            bekapB = new Button();
            player1 = new Player(true);//igrac koji igra prvi (beli igrac), јеr po pravlima on igra prvi
            player2 = new Player(false);
            InitializeComponent();
            table = new Tabela();
            bekap = new Tabela();
            s1 = new Stopwatch();
            s2 = new Stopwatch();
            timer1.Start();
            s1.Start();
            poredjajFigure();

        }
        private void poredjajFigure()
        {
            for (int i = 0; i < 8; i++)
                table.poljas[1 * 8 + i] = new Polja(new Pijun(true));
            for (int i = 0; i < 8; i++)
                table.poljas[6 * 8 + i] = new Polja(new Pijun(false));
            // table.poljas[42] = new Polja(new Pijun(true));
            table.poljas[0] = new Polja(new top(true));
            table.poljas[7] = new Polja(new top(true));
            table.poljas[7 * 8] = new Polja(new top(false));
            table.poljas[7 * 8 + 7] = new Polja(new top(false));
            table.poljas[1] = new Polja(new Konj(true));
            table.poljas[6] = new Polja(new Konj(true));
            table.poljas[7 * 8 + 1] = new Polja(new Konj(false));
            table.poljas[7 * 8 + 6] = new Polja(new Konj(false));
            table.poljas[2] = new Polja(new Lovac(true));
            table.poljas[5] = new Polja(new Lovac(true));
            table.poljas[7 * 8 + 2] = new Polja(new Lovac(false));
            table.poljas[7 * 8 + 5] = new Polja(new Lovac(false));
            table.poljas[4] = new Polja(new Kralj(true));
            table.poljas[7 * 8 + 4] = new Polja(new Kralj(false));
            table.poljas[3] = new Polja(new Kraljica(true));
            table.poljas[7 * 8 + 3] = new Polja(new Kraljica(false));
        }
        private void button1_Click(object sender, EventArgs e)
        {
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Size s = new Size();
            s.Width = 60;
            s.Height = 60;
            Figura f = new Figura();
            int oldIndex = -1;
            Size z = new Size();
            int j;
            bool pColor = true;

            for (int i = 0; i < 8; i++)
            {
                pColor = !pColor;
                for (j = 0; j < 8; j++)
                {

                    b = new Button();
                    if (table.poljas[i * 8 + j].figura.Slika != null)
                    {
                        try
                        {
                            b.BackgroundImage = Image.FromFile(table.poljas[i * 8 + j].figura.Slika);
                        }
                        catch(Exception)
                        {
                            MessageBox.Show("Neuspesno ucitavanje slika!\n Molimo vas da proverite da li se fajl '80' nalazi u root folderu programa!");
                            this.Close();
                        }
                    }
                    b.BackgroundImageLayout = ImageLayout.Stretch;
                    b.Click += (object sender2, EventArgs e2) =>
                    {
                        Button button = sender2 as Button;
                        // MessageBox.Show(button.TabIndex.ToString());
                        if (player1.naPotezu != player2.naPotezu && table.poljas[button.TabIndex].figura.Slika != null && oldIndex == -1 && (!player1.naPotezu == table.poljas[button.TabIndex].figura.boja || player2.naPotezu == table.poljas[button.TabIndex].figura.boja))
                        {
                            countMove(table.poljas[button.TabIndex].figura, button.TabIndex);
                            f = table.poljas[button.TabIndex].figura;
                            oldIndex = button.TabIndex;
                            if (table.poljas[button.TabIndex].figura.Vrsta == figurica.kralj)
                                checkRocade(button.TabIndex);

                        }
                        else if (button.BackColor == Color.CadetBlue || button.BackColor == Color.MediumVioletRed)
                        {
                            if (player1.naPotezu)
                            {
                                // bool a = true;     
                                
                                bekap.preuzmiPolja(table);                               
                                if(player1.brojFigurica==1)
                                {
                                    if (!checkifRemi(player1))
                                    {
                                        MessageBox.Show("Nereseno! REMI");
                                        player1.naPotezu = false;
                                        player2.naPotezu = false;
                                        s1.Stop();
                                        s2.Stop();
                                    }
                                }
                                bekapK = player1.indexKralja;
                                planaMove(oldIndex, button.TabIndex, f, player1);
                                
                                if (checkifChess(player1))//ako je sah vraca false
                                {
                                    makeaMove(oldIndex, button.TabIndex, f, player2);
                                    if(table.poljas[button.TabIndex].figura.Vrsta==figurica.kralj && button.TabIndex-oldIndex==-2)
                                    {
                                        planaMove(oldIndex - 4, oldIndex - 1, f, player1);
                                        makeaMove(oldIndex - 4, oldIndex - 1, f, player2);
                                    }
                                    if (table.poljas[button.TabIndex].figura.Vrsta == figurica.kralj && button.TabIndex - oldIndex == 2)
                                    {
                                        planaMove(oldIndex + 3, oldIndex + 1, f, player1);
                                        makeaMove(oldIndex + 3, oldIndex + 1, f, player2);

                                    }
                                    if (table.poljas[button.TabIndex].figura.Vrsta == figurica.top || table.poljas[button.TabIndex].figura.Vrsta == figurica.kralj)
                                        table.poljas[button.TabIndex].figura.dalijepomren = true;
                                    if(table.poljas[button.TabIndex].figura.Vrsta==figurica.pijun && button.TabIndex/8==0)
                                    {
                                        Form2 f2 = new Form2(player1.boja);
                                        f2.ShowDialog();
                                       // MessageBox.Show(((figurica)f2.vratiFig()).ToString());
                                        table.poljas[button.TabIndex].figura = f2.fig;
                                        flowLayoutPanel1.Controls[button.TabIndex].BackgroundImage = Image.FromFile(f2.fig.Slika);
                                        f2.Close();


                                    }
                                    player1.naPotezu = player1.naPotezu ^ true;//logicki OR operator
                                    player2.naPotezu = player2.naPotezu ^ true;
                                    s1.Stop();
                                    s2.Start();
                                    if (!checkifChess(player2))
                                    {
                                        MessageBox.Show("Sah!");
                                        if(!checkifMate(player2))
                                        {
                                            MessageBox.Show("Sah mat!");
                                            player1.naPotezu = false;
                                            player2.naPotezu = false;
                                            s1.Stop();
                                            s2.Stop();
                                        }
                                        player2.indexKralja = bekapK;
                                    }
                                }
                                else
                                {
                                    table.preuzmiPolja(bekap);
                                    player1.indexKralja = bekapK;
                                    MessageBox.Show("Nevalidan potez! Sah vam je na tom mestu.");
                                }
                         

                            }
                            else
                            {
                               
                                bekap.preuzmiPolja(table);
                                if (player2.brojFigurica == 1)
                                {
                                    if (!checkifRemi(player2))
                                    {
                                        MessageBox.Show("Nereseno! REMI!");
                                        player1.naPotezu = false;
                                        player2.naPotezu = false;
                                        s1.Stop();
                                        s2.Stop();
                                    }
                                }
                                bekapK = player2.indexKralja;
                                planaMove(oldIndex, button.TabIndex, f, player2);
                               
                                if (checkifChess(player2))//ako je sah vraca false
                                {
                                    makeaMove(oldIndex, button.TabIndex, f, player1);
                                    if (table.poljas[button.TabIndex].figura.Vrsta == figurica.kralj && button.TabIndex - oldIndex == -2)
                                    {
                                        planaMove(oldIndex - 4, oldIndex - 1, f, player2);
                                        makeaMove(oldIndex - 4, oldIndex - 1, f, player1);
                                    }
                                    if (table.poljas[button.TabIndex].figura.Vrsta == figurica.kralj && button.TabIndex - oldIndex == 2)
                                    {
                                        planaMove(oldIndex + 3, oldIndex + 1, f, player2);
                                        makeaMove(oldIndex + 3, oldIndex + 1, f, player1);
                                    }
                                    if (table.poljas[button.TabIndex].figura.Vrsta == figurica.top || table.poljas[button.TabIndex].figura.Vrsta == figurica.kralj)
                                        table.poljas[button.TabIndex].figura.dalijepomren = true;
                                    if (table.poljas[button.TabIndex].figura.Vrsta == figurica.pijun && button.TabIndex / 8 == 7)
                                    {
                                        Form2 f2 = new Form2(player2.boja);
                                        f2.ShowDialog();
                                        // MessageBox.Show(((figurica)f2.vratiFig()).ToString());
                                        table.poljas[button.TabIndex].figura = f2.fig;
                                        flowLayoutPanel1.Controls[button.TabIndex].BackgroundImage = Image.FromFile(f2.fig.Slika);
                                        f2.Close();


                                    }
                                    player1.naPotezu = player1.naPotezu ^ true;//logicki OR operator
                                    player2.naPotezu = player2.naPotezu ^ true;
                                    s2.Stop();
                                    s1.Start();
                                    if (!checkifChess(player1))
                                    {
                                        MessageBox.Show("Sah!");
                                        if (!checkifMate(player1))
                                        {
                                            MessageBox.Show("Sah mat!");
                                            player1.naPotezu = false;
                                            player2.naPotezu = false;
                                            s1.Stop();
                                            s2.Stop();
                                        }
                                        player1.indexKralja = bekapK;
                                    }
                                }
                                else
                                {
                                    table.preuzmiPolja(bekap);
                                    player2.indexKralja = bekapK;
                                    MessageBox.Show("Nevalidan potez! Sah vam je na tom mestu.");
                                }
                            

                            }
                            for (int k = 0; k < 64; k++)
                            {
                                flowLayoutPanel1.Controls[k].BackColor = flowLayoutPanel1.Controls[k].ForeColor;
                            }
                            oldIndex = -1;
                        }
                        else
                        {
                            for (int k = 0; k < 64; k++)
                            {
                                flowLayoutPanel1.Controls[k].BackColor = flowLayoutPanel1.Controls[k].ForeColor;
                            }
                            oldIndex = -1;
                          //  MessageBox.Show(player1.indexKralja.ToString() + "," + player2.indexKralja.ToString());
                        }

                    };

                    if (pColor)
                        b.BackColor = Color.Peru;
                    else
                        b.BackColor = Color.WhiteSmoke;
                    pColor = !pColor;
                    b.ForeColor = b.BackColor;
                    b.Margin = new Padding(0);
                    b.Name = i.ToString() + "," + j.ToString();
                    b.Size = s;
                    flowLayoutPanel1.Controls.Add(b);

                }
                z.Width = flowLayoutPanel1.Width;
                flowLayoutPanel1.MaximumSize = z;
            }
        }

        private bool checkRocade(int a)
        {
            if(!table.poljas[a].figura.dalijepomren && !table.poljas[a-1].zauzeto && !table.poljas[a-2].zauzeto && !table.poljas[a-3].zauzeto && !table.poljas[a-4].figura.dalijepomren && table.poljas[a-4].figura.Vrsta==figurica.top)
            {
                flowLayoutPanel1.Controls[a - 2].BackColor = Color.CadetBlue;
            }
            if(!table.poljas[a].figura.dalijepomren && !table.poljas[a+1].zauzeto && !table.poljas[a+2].zauzeto && !table.poljas[a+3].figura.dalijepomren && table.poljas[a+3].figura.Vrsta==figurica.top)
            {
                flowLayoutPanel1.Controls[a + 2].BackColor = Color.CadetBlue;
            }
            return true;
        }

        private bool checkifRemi(Player p)
        {
            bekap.preuzmiPolja(table);
            bekapK = p.indexKralja;
            int index = p.indexKralja-8;
            if (index >= 0)//+y osa
            {
                table.poljas[index] = table.poljas[bekapK];
                p.indexKralja = index;
                table.poljas[bekapK] = new Polja();
                if (checkifChess(p))
                {
                    table.preuzmiPolja(bekap);
                    return true;
                }
                table.preuzmiPolja(bekap);
            }
            index = bekapK + 8;
            if (index < 64)//-y
            {
                table.poljas[index] = table.poljas[bekapK];
                p.indexKralja = index;
                table.poljas[bekapK] = new Polja();
                if (checkifChess(p))
                {
                    table.preuzmiPolja(bekap);
                    return true;
                }
                table.preuzmiPolja(bekap);
            }
            index = bekapK + 1;
            if (index / 8 == bekapK / 8)
            {
                table.poljas[index] = table.poljas[bekapK];
                p.indexKralja = index;
                table.poljas[bekapK] = new Polja();
                if (checkifChess(p))
                {
                    table.preuzmiPolja(bekap);
                    return true;
                }
                table.preuzmiPolja(bekap);
            }
            index = bekapK - 1;
            if (index / 8 == bekapK / 8)
            {
                table.poljas[index] = table.poljas[bekapK];
                p.indexKralja = index;
                table.poljas[bekapK] = new Polja();
                if (checkifChess(p))
                {
                    table.preuzmiPolja(bekap);
                    return true;
                }
                table.preuzmiPolja(bekap);
            }
            index = bekapK;
            index -= 7;
            if (index >= 0 && index % 8 > 0  && index/8==(bekapK/8)-1)//+x+y!
            {
                table.poljas[index] = table.poljas[bekapK];
                p.indexKralja = index;
                table.poljas[bekapK] = new Polja();
                if (checkifChess(p))
                {
                    table.preuzmiPolja(bekap);
                    return true;
                }
                table.preuzmiPolja(bekap);
            }
            index = bekapK;
            index -= 9;
            if (index >= 0 && index % 8 < 7 && index/8==(bekapK/ 8)-1)//-x+y
            {
                table.poljas[index] = table.poljas[bekapK];
                p.indexKralja = index;
                table.poljas[bekapK] = new Polja();
                if (checkifChess(p))
                {
                    table.preuzmiPolja(bekap);
                    return true;
                }
                table.preuzmiPolja(bekap);
            }
            index = bekapK;
            index += 9;
            if (index < 64 && index % 8 > 0 && index/8==(bekapK/8)+1)//+x-y
            {
                table.poljas[index] = table.poljas[bekapK];
                p.indexKralja = index;
                table.poljas[bekapK] = new Polja();
                if (checkifChess(p))
                {
                    table.preuzmiPolja(bekap);
                    return true;
                }
                table.preuzmiPolja(bekap);
            }
            index = bekapK;
            index += 7;
            if (index < 64 && index % 8 < 7 && index / 8 == (bekapK / 8) + 1)
            {
                table.poljas[index] = table.poljas[bekapK];
                p.indexKralja = index;
                table.poljas[bekapK] = new Polja();
                if (checkifChess(p))
                {
                    table.preuzmiPolja(bekap);
                    return true;
                }
                table.preuzmiPolja(bekap);
            }
            p.indexKralja = bekapK;
            return false;
        }

        private bool checkifMate(Player p)
        {
            bekapK = p.indexKralja;
            for (int i=0; i<64; i++)
            {
                if(table.poljas[i].figura.boja==p.boja && table.poljas[i].figura.Slika!=null)
                {
                    bekap.preuzmiPolja(table);

                    if (table.poljas[i].figura.Vrsta == figurica.kralj)
                    {
                        
                        int index = i - 8;
                        if (index >= 0 && (table.poljas[index].figura.boja != p.boja || table.poljas[index].figura.Slika == null))//+y osa
                        {
                            table.poljas[index] = table.poljas[i];
                            p.indexKralja = index;
                            table.poljas[i] = new Polja();
                            if (checkifChess(p))
                            {
                                table.preuzmiPolja(bekap);
                                return true;
                            }
                            table.preuzmiPolja(bekap);
                        }
                        index = i + 8;
                        if (index < 64 && (table.poljas[index].figura.boja != p.boja || table.poljas[index].figura.Slika == null))//-y
                        {
                            table.poljas[index] = table.poljas[i];
                            p.indexKralja = index;
                            table.poljas[i] = new Polja();
                            if (checkifChess(p))
                            {
                                table.preuzmiPolja(bekap);
                                return true;
                            }
                            table.preuzmiPolja(bekap);
                        }
                        index = i + 1;
                        if (index / 8 == i / 8 && (table.poljas[index].figura.boja != p.boja || table.poljas[index].figura.Slika == null))
                        {
                            table.poljas[index] = table.poljas[i];
                            p.indexKralja = index;
                            table.poljas[i] = new Polja();
                            if (checkifChess(p))
                            {
                                table.preuzmiPolja(bekap);
                                return true;
                            }
                            table.preuzmiPolja(bekap);
                        }
                        index = i - 1;
                        if (index / 8 == i / 8 && (table.poljas[index].figura.boja != p.boja || table.poljas[index].figura.Slika == null))
                        {
                            table.poljas[index] = table.poljas[i];
                            p.indexKralja = index;
                            table.poljas[i] = new Polja();
                            if (checkifChess(p))
                            {
                                table.preuzmiPolja(bekap);
                                return true;
                            }
                            table.preuzmiPolja(bekap);
                        }
                        index = i;
                        index -= 7;
                        if (index >= 0 && index % 8 > 0 && (table.poljas[index].figura.boja != p.boja || table.poljas[index].figura.Slika == null))//+x+y!
                        {
                            table.poljas[index] = table.poljas[i];
                            p.indexKralja = index;
                            table.poljas[i] = new Polja();
                            if (checkifChess(p))
                            {
                                table.preuzmiPolja(bekap);
                                return true;
                            }
                            table.preuzmiPolja(bekap);
                        }
                        index = i;
                        index -= 9;
                        if (index >= 0 && index % 8 < 7 && (table.poljas[index].figura.boja != p.boja || table.poljas[index].figura.Slika == null))//-x+y
                        {
                            table.poljas[index] = table.poljas[i];
                            p.indexKralja = index;
                            table.poljas[i] = new Polja();
                            if (checkifChess(p))
                            {
                                table.preuzmiPolja(bekap);
                                return true;
                            }
                            table.preuzmiPolja(bekap);
                        }
                        index = i;
                        index += 9;
                        if (index < 64 && index % 8 > 0 && (table.poljas[index].figura.boja != p.boja || table.poljas[index].figura.Slika == null))//+x-y
                        {
                            table.poljas[index] = table.poljas[i];
                            p.indexKralja = index;
                            table.poljas[i] = new Polja();
                            if (checkifChess(p))
                            {
                                table.preuzmiPolja(bekap);
                                return true;
                            }
                            table.preuzmiPolja(bekap);
                        }
                        index = i;
                        index += 7;
                        if (index < 64 && index % 8 < 7 && (table.poljas[index].figura.boja != p.boja || table.poljas[index].figura.Slika == null))
                        {
                            table.poljas[index] = table.poljas[i];
                            p.indexKralja = index;
                            table.poljas[i] = new Polja();
                            if (checkifChess(p))
                            {
                                table.preuzmiPolja(bekap);
                                return true;
                            }
                            table.preuzmiPolja(bekap);
                        }
                        p.indexKralja = bekapK;

                    }
                    else if (table.poljas[i].figura.Vrsta == figurica.top)
                    {
                        int index = i - 8;
                        bool pr = false;
                        while (index >= 0 && !pr)//+y osa
                        {
                            if (table.poljas[index].figura.boja != p.boja || table.poljas[index].figura.Slika == null)
                            {
                                table.poljas[index] = table.poljas[i];
                                table.poljas[i] = new Polja();
                                if (checkifChess(p))
                                {
                                    table.preuzmiPolja(bekap);
                                    return true;
                                }
                                table.preuzmiPolja(bekap);
                            }
                            if (table.poljas[index].zauzeto)
                                pr = true;
                            index -= 8;
                        }
                        index = i + 8;
                        pr = false;
                        while (index < 64 && !pr)//-y
                        {
                            if (table.poljas[index].figura.boja != p.boja || table.poljas[index].figura.Slika == null)
                            {
                                table.poljas[index] = table.poljas[i];
                                table.poljas[i] = new Polja();
                                if (checkifChess(p))
                                {
                                    table.preuzmiPolja(bekap);
                                    return true;
                                }
                                table.preuzmiPolja(bekap);
                            }
                            if (table.poljas[index].zauzeto)
                                pr = true;
                            index += 8;
                        }
                        index = i + 1;
                        pr = false;
                        while (index / 8 == i / 8 && !pr)
                        {
                            if (table.poljas[index].figura.boja != p.boja || table.poljas[index].figura.Slika == null)
                            {
                                table.poljas[index] = table.poljas[i];
                                table.poljas[i] = new Polja();
                                if (checkifChess(p))
                                {
                                    table.preuzmiPolja(bekap);
                                    return true;
                                }
                                table.preuzmiPolja(bekap);
                            }
                            if (table.poljas[index].zauzeto)
                                pr = true;
                            index++;
                        }
                        index = i - 1;
                        pr = false;
                        while (index / 8 == i / 8 && index >= 0 && !pr)
                        {
                            if (table.poljas[index].figura.boja != p.boja || table.poljas[index].figura.Slika == null)
                            {
                                table.poljas[index] = table.poljas[i];
                                table.poljas[i] = new Polja();
                                if (checkifChess(p))
                                {
                                    table.preuzmiPolja(bekap);
                                    return true;
                                }
                                table.preuzmiPolja(bekap);
                            }
                            if (table.poljas[index].zauzeto)
                                pr = true;
                            index--;
                        }
                    }
                    else if (table.poljas[i].figura.Vrsta == figurica.lovac)
                    {
                        int index = i;
                        index -= 7;
                        bool pR = false;
                        while (index > 0 && index % 8 > 0 && !pR)//+x+y
                        {
                            if (table.poljas[index].figura.boja != p.boja || table.poljas[index].figura.Slika == null)
                            {
                                table.poljas[index] = table.poljas[i];
                                table.poljas[i] = new Polja();
                                if (checkifChess(p))
                                {
                                    table.preuzmiPolja(bekap);
                                    return true;
                                }
                                table.preuzmiPolja(bekap);
                            }
                            if (table.poljas[index].zauzeto)
                                pR = true;
                            index -= 7;
                        }
                        pR = false;
                        index = i;
                        index -= 9;
                        while (index >= 0 && index % 8 < 7 && !pR)//-x+y
                        {
                            if (table.poljas[index].figura.boja != p.boja || table.poljas[index].figura.Slika == null)
                            {
                                table.poljas[index] = table.poljas[i];
                                table.poljas[i] = new Polja();
                                if (checkifChess(p))
                                {
                                    table.preuzmiPolja(bekap);
                                    return true;
                                }
                                table.preuzmiPolja(bekap);
                            }
                            if (table.poljas[index].zauzeto)
                                pR = true;
                            index -= 9;
                        }
                        pR = false;
                        index = i;
                        index += 9;
                        while (index < 64 && index % 8 > 0 && !pR)//+x-y
                        {
                            if (table.poljas[index].figura.boja != p.boja || table.poljas[index].figura.Slika == null)
                            {
                                table.poljas[index] = table.poljas[i];
                                table.poljas[i] = new Polja();
                                if (checkifChess(p))
                                {
                                    table.preuzmiPolja(bekap);
                                    return true;
                                }
                                table.preuzmiPolja(bekap);
                            }
                            if (table.poljas[index].zauzeto)
                                pR = true;
                            index += 9;
                        }
                        pR = false;
                        index = i;
                        index += 7;
                        while (index < 64 && index % 8 < 7 && !pR)
                        {
                            if (table.poljas[index].figura.boja != p.boja || table.poljas[index].figura.Slika == null)
                            {
                                table.poljas[index] = table.poljas[i];
                                table.poljas[i] = new Polja();
                                if (checkifChess(p))
                                {
                                    table.preuzmiPolja(bekap);
                                    return true;
                                }
                                table.preuzmiPolja(bekap);
                            }
                            if (table.poljas[index].zauzeto)
                                pR = true;
                            index += 7;
                        }
                    }
                    else if (table.poljas[i].figura.Vrsta == figurica.kraljica)
                    {
                        int index = i - 8;
                        bool pR = false;
                        while (index >= 0 && !pR)//+y osa
                        {
                            if (table.poljas[index].figura.boja != p.boja || table.poljas[index].figura.Slika == null)
                            {
                                table.poljas[index] = table.poljas[i];
                                table.poljas[i] = new Polja();
                                if (checkifChess(p))
                                {
                                    table.preuzmiPolja(bekap);
                                    return true;
                                }
                                table.preuzmiPolja(bekap);
                            }
                            if (table.poljas[index].zauzeto)
                                pR = true;
                            index -= 8;
                        }
                        pR = false;
                        index = i + 8;
                        while (index < 64 && !pR)//-y
                        {
                            if (table.poljas[index].figura.boja != p.boja || table.poljas[index].figura.Slika == null)
                            {
                                table.poljas[index] = table.poljas[i];
                                table.poljas[i] = new Polja();
                                if (checkifChess(p))
                                {
                                    table.preuzmiPolja(bekap);
                                    return true;
                                }
                                table.preuzmiPolja(bekap);
                            }
                            if (table.poljas[index].zauzeto)
                                pR = true;
                            index += 8;
                        }
                        pR = false;
                        index = i + 1;
                        while (index / 8 == i / 8 && !pR)
                        {
                            if (table.poljas[index].figura.boja != p.boja || table.poljas[index].figura.Slika == null)
                            {
                                table.poljas[index] = table.poljas[i];
                                table.poljas[i] = new Polja();
                                if (checkifChess(p))
                                {
                                    table.preuzmiPolja(bekap);
                                    return true;
                                }
                                table.preuzmiPolja(bekap);
                            }
                            if (table.poljas[index].zauzeto)
                                pR = true;
                            index++;
                        }
                        pR = false;
                        index = i - 1;
                        while (index / 8 == i / 8 && index >= 0 && !pR)
                        {
                            if (table.poljas[index].figura.boja != p.boja || table.poljas[index].figura.Slika == null)
                            {
                                table.poljas[index] = table.poljas[i];
                                table.poljas[i] = new Polja();
                                if (checkifChess(p))
                                {
                                    table.preuzmiPolja(bekap);
                                    return true;
                                }
                                table.preuzmiPolja(bekap);
                            }
                            if (table.poljas[index].zauzeto)
                                pR = true;
                            index--;
                        }
                        pR = false;
                        index = i;
                        index -= 7;
                        while (index > 0 && index % 8 > 0 && !pR)//+x+y
                        {
                            if (table.poljas[index].figura.boja != p.boja || table.poljas[index].figura.Slika == null)
                            {
                                table.poljas[index] = table.poljas[i];
                                table.poljas[i] = new Polja();
                                if (checkifChess(p))
                                {
                                    table.preuzmiPolja(bekap);
                                    return true;
                                }
                                table.preuzmiPolja(bekap);
                            }
                            if (table.poljas[index].zauzeto)
                                pR = true;
                            index -= 7;
                        }
                        pR = false;
                        index = i;
                        index -= 9;
                        while (index >= 0 && index % 8 < 7 && !pR)//-x+y
                        {
                            if (table.poljas[index].figura.boja != p.boja || table.poljas[index].figura.Slika == null)
                            {
                                table.poljas[index] = table.poljas[i];
                                table.poljas[i] = new Polja();
                                if (checkifChess(p))
                                {
                                    table.preuzmiPolja(bekap);
                                    return true;
                                }
                                table.preuzmiPolja(bekap);
                            }
                            if (table.poljas[index].zauzeto)
                                pR = true;
                            index -= 9;
                        }
                        pR = false;
                        index = i;
                        index += 9;
                        while (index < 64 && index % 8 > 0 && !pR)//+x-y
                        {
                            if (table.poljas[index].figura.boja != p.boja || table.poljas[index].figura.Slika == null)
                            {
                                table.poljas[index] = table.poljas[i];
                                table.poljas[i] = new Polja();
                                if (checkifChess(p))
                                {
                                    table.preuzmiPolja(bekap);
                                    return true;
                                }
                                table.preuzmiPolja(bekap);
                            }
                            if (table.poljas[index].zauzeto)
                                pR = true;
                            index += 9;
                        }
                        pR = false;
                        index = i;
                        index += 7;
                        while (index < 64 && index % 8 < 7 && !pR)
                        {
                            if (table.poljas[index].figura.boja != p.boja || table.poljas[index].figura.Slika == null)
                            {
                                table.poljas[index] = table.poljas[i];
                                table.poljas[i] = new Polja();
                                if (checkifChess(p))
                                {
                                    table.preuzmiPolja(bekap);
                                    return true;
                                }
                                table.preuzmiPolja(bekap);
                            }
                            if (table.poljas[index].zauzeto)
                                pR = true;
                            index += 7;
                        }
                    }
                    else if (table.poljas[i].figura.Vrsta==figurica.konj)
                    {
                        int index = i;
                        index -= 15;
                        if (index >= 0 && (table.poljas[index].figura.boja != p.boja || table.poljas[index].figura.Slika == null))
                        {
                            table.poljas[index] = table.poljas[i];
                            table.poljas[i] = new Polja();
                            if (checkifChess(p))
                            {
                                table.preuzmiPolja(bekap);
                                return true;
                            }
                            table.preuzmiPolja(bekap);
                        }
                        index = i;
                        index -= 17;
                        if (index >= 0 && (table.poljas[index].figura.boja != p.boja || table.poljas[index].figura.Slika == null))
                        {
                            table.poljas[index] = table.poljas[i];
                            table.poljas[i] = new Polja();
                            if (checkifChess(p))
                            {
                                table.preuzmiPolja(bekap);
                                return true;
                            }
                            table.preuzmiPolja(bekap);
                        }
                        index = i;
                        index += 15;
                        if (index < 64 && (table.poljas[index].figura.boja != p.boja || table.poljas[index].figura.Slika == null))
                        {
                            table.poljas[index] = table.poljas[i];
                            table.poljas[i] = new Polja();
                            if (checkifChess(p))
                            {
                                table.preuzmiPolja(bekap);
                                return true;
                            }
                            table.preuzmiPolja(bekap);
                        }
                        index = i;
                        index += 17;
                        if (index < 64 && (table.poljas[index].figura.boja != p.boja || table.poljas[index].figura.Slika == null))
                        {
                            table.poljas[index] = table.poljas[i];
                            table.poljas[i] = new Polja();
                            if (checkifChess(p))
                            {
                                table.preuzmiPolja(bekap);
                                return true;
                            }
                            table.preuzmiPolja(bekap);
                        }
                        index = i;
                        index -= 6;
                        if (index % 8 > 0 && index >= 0 && (table.poljas[index].figura.boja != p.boja || table.poljas[index].figura.Slika == null))
                        {
                            table.poljas[index] = table.poljas[i];
                            table.poljas[i] = new Polja();
                            if (checkifChess(p))
                            {
                                table.preuzmiPolja(bekap);
                                return true;
                            }
                            table.preuzmiPolja(bekap);
                        }
                        index = i;
                        index -= 10;
                        if (index % 8 < 7 && index >= 0 && (table.poljas[index].figura.boja != p.boja || table.poljas[index].figura.Slika == null))
                        {
                            table.poljas[index] = table.poljas[i];
                            table.poljas[i] = new Polja();
                            if (checkifChess(p))
                            {
                                table.preuzmiPolja(bekap);
                                return true;
                            }
                            table.preuzmiPolja(bekap);
                        }

                        index = i;
                        index += 6;
                        if (index % 8 < 7 && index < 64 && (table.poljas[index].figura.boja != p.boja || table.poljas[index].figura.Slika == null))
                        {
                            table.poljas[index] = table.poljas[i];
                            table.poljas[i] = new Polja();
                            if (checkifChess(p))
                            {
                                table.preuzmiPolja(bekap);
                                return true;
                            }
                            table.preuzmiPolja(bekap);
                        }
                        index = i;
                        index += 10;
                        if (index % 8 > 0 && index < 64 && (table.poljas[index].figura.boja != p.boja || table.poljas[index].figura.Slika == null))
                        {
                            table.poljas[index] = table.poljas[i];
                            table.poljas[i] = new Polja();
                            if (checkifChess(p))
                            {
                                table.preuzmiPolja(bekap);
                                return true;
                            }
                            table.preuzmiPolja(bekap);
                        }
                    }
                    else if(table.poljas[i].figura.Vrsta==figurica.pijun)
                    {
                        int index;
                        if (!table.poljas[i].figura.boja && i / 8 > 0)//bele
                        {
                            index = i - 9;
                            if (index>=0 && table.poljas[index].figura.boja != p.boja && table.poljas[index].figura.Slika!=null)
                            {
                                table.poljas[index] = table.poljas[i];
                                table.poljas[i] = new Polja();
                                if (checkifChess(p))
                                {
                                    table.preuzmiPolja(bekap);
                                    return true;
                                }
                                table.preuzmiPolja(bekap);
                            }
                            index = i - 8;
                            if (index >= 0 && !table.poljas[index].zauzeto)
                            {
                                table.poljas[index] = table.poljas[i];
                                table.poljas[i] = new Polja();
                                if (checkifChess(p))
                                {
                                    table.preuzmiPolja(bekap);
                                    return true;
                                }
                                table.preuzmiPolja(bekap);
                            }
                            index = i - 7;
                            if (index > 0 && table.poljas[index].figura.boja != p.boja != p.boja && table.poljas[index].figura.Slika != null)
                            {
                                table.poljas[index] = table.poljas[i];
                                table.poljas[i] = new Polja();
                                if (checkifChess(p))
                                {
                                    table.preuzmiPolja(bekap);
                                    return true;
                                }
                                table.preuzmiPolja(bekap);
                            }
                            if (i/8==6)
                            {
                                index = i - 16;
                                    if (!table.poljas[index].zauzeto)
                                    {
                                        table.poljas[index] = table.poljas[i];
                                        table.poljas[i] = new Polja();
                                        if (checkifChess(p))
                                        {
                                            table.preuzmiPolja(bekap);
                                            return true;
                                        }
                                        table.preuzmiPolja(bekap);
                                    }

                            }
                            
                        }
                        else if (table.poljas[i].figura.boja && i / 8 < 7)//crne
                        {
                            index = i + 9;
                            if (index < 64 && table.poljas[index].figura.boja != p.boja && table.poljas[index].figura.Slika != null)
                            {
                                table.poljas[index] = table.poljas[i];
                                table.poljas[i] = new Polja();
                                if (checkifChess(p))
                                {
                                    table.preuzmiPolja(bekap);
                                    return true;
                                }
                                table.preuzmiPolja(bekap);
                            }
                            index = i + 8;
                            if (index < 64 && !table.poljas[index].zauzeto)
                            {//
                                table.poljas[index] = table.poljas[i];
                                table.poljas[i] = new Polja();
                                if (checkifChess(p))
                                {
                                    table.preuzmiPolja(bekap);
                                    return true;
                                }
                                table.preuzmiPolja(bekap);
                            }
                            index = i + 7;
                            if (index < 64 && table.poljas[index].figura.boja != p.boja != p.boja && table.poljas[index].figura.Slika != null)
                            {
                                table.poljas[index] = table.poljas[i];
                                table.poljas[i] = new Polja();
                                if (checkifChess(p))
                                {
                                    table.preuzmiPolja(bekap);
                                    return true;
                                }
                                table.preuzmiPolja(bekap);
                            }
                            if (i / 8 == 1)
                            {
                                index = i + 16;
                                if (!table.poljas[index].zauzeto)
                                {
                                    table.poljas[index] = table.poljas[i];
                                    table.poljas[i] = new Polja();
                                    if (checkifChess(p))
                                    {
                                        table.preuzmiPolja(bekap);
                                        return true;
                                    }
                                    table.preuzmiPolja(bekap);
                                }

                            }

                        }

                    }
                }    
               

            }

            return false;//vraca false ako je mat
        }

        private void planaMove(int oldIndex, int tabIndex, Figura f, Player p)
        {
            table.poljas[tabIndex] = table.poljas[oldIndex];
            // flowLayoutPanel1.Controls[tabIndex].BackgroundImage = flowLayoutPanel1.Controls[oldIndex].BackgroundImage;

            table.poljas[oldIndex] = new Polja();
            // flowLayoutPanel1.Controls[oldIndex].BackgroundImage = null;
            if (table.poljas[tabIndex].figura.Vrsta == figurica.kralj)
            {
                bekapK = p.indexKralja;
                p.indexKralja = tabIndex;

            }

        }

        private bool checkifChess(Player p)//ako je sah vraca false
        {
            // bekap = table;
          //  bekapK = p.indexKralja;
            int indexer = p.indexKralja - 8;
            while (indexer >= 0 && !table.poljas[indexer].zauzeto)//gore
            {
                indexer -= 8;
            }
            if ((indexer >= 0 && indexer < 64) && ((table.poljas[indexer].figura.Vrsta == figurica.top && table.poljas[indexer].figura.boja != table.poljas[p.indexKralja].figura.boja) || (table.poljas[indexer].figura.Vrsta == figurica.kraljica && table.poljas[indexer].figura.boja != table.poljas[p.indexKralja].figura.boja)))
                return false;
            indexer = p.indexKralja + 8;
            while (indexer < 64 && !table.poljas[indexer].zauzeto)//dole
            {
                indexer += 8;
            }
            if ((indexer >= 0 && indexer < 64) && ((table.poljas[indexer].figura.Vrsta == figurica.top && table.poljas[indexer].figura.boja != table.poljas[p.indexKralja].figura.boja) || (table.poljas[indexer].figura.Vrsta == figurica.kraljica && table.poljas[indexer].figura.boja != table.poljas[p.indexKralja].figura.boja)))
                return false;
            indexer = p.indexKralja + 1;
            while (indexer < 64 && indexer / 8 == p.indexKralja / 8 && !table.poljas[indexer].zauzeto)//desno
            {
                indexer++;
            }
            // indexer--;
            if (indexer / 8 == p.indexKralja / 8 && (indexer >= 0 && indexer < 64) && ((table.poljas[indexer].figura.Vrsta == figurica.top && table.poljas[indexer].figura.boja != table.poljas[p.indexKralja].figura.boja) || (table.poljas[indexer].figura.Vrsta == figurica.kraljica && table.poljas[indexer].figura.boja != table.poljas[p.indexKralja].figura.boja)))
                return false;
            indexer = p.indexKralja - 1;
            while (indexer >= 0 && indexer / 8 == p.indexKralja / 8 && !table.poljas[indexer].zauzeto)//levo
            {
                indexer--;
            }
            if (indexer / 8 == p.indexKralja / 8 && (indexer >= 0 && indexer < 64) && ((table.poljas[indexer].figura.Vrsta == figurica.top && table.poljas[indexer].figura.boja != table.poljas[p.indexKralja].figura.boja) || (table.poljas[indexer].figura.Vrsta == figurica.kraljica && table.poljas[indexer].figura.boja != table.poljas[p.indexKralja].figura.boja)))
                return false;
            indexer = p.indexKralja - 7;
            while (indexer >= 0 && indexer % 8 > 0 && !table.poljas[indexer].zauzeto)//+x+y
            {
                indexer = indexer - 7;
            }
            if (indexer >= 0 && indexer % 8 > 0 && ((table.poljas[indexer].figura.Vrsta == figurica.lovac && table.poljas[indexer].figura.boja != table.poljas[p.indexKralja].figura.boja) || (table.poljas[indexer].figura.Vrsta == figurica.kraljica && table.poljas[indexer].figura.boja != table.poljas[p.indexKralja].figura.boja)))
                return false;
            indexer = p.indexKralja - 9;
            while (indexer >= 0 && indexer % 8 < 7 && !table.poljas[indexer].zauzeto)//-x+y
            {
                indexer = indexer - 9;
            }
            if (indexer >= 0 && indexer % 8 < 7 && ((table.poljas[indexer].figura.Vrsta == figurica.lovac && table.poljas[indexer].figura.boja != table.poljas[p.indexKralja].figura.boja) || (table.poljas[indexer].figura.Vrsta == figurica.kraljica && table.poljas[indexer].figura.boja != table.poljas[p.indexKralja].figura.boja)))
                return false;
            indexer = p.indexKralja + 9;
            while (indexer < 64 && indexer % 8 > 0 && !table.poljas[indexer].zauzeto)//+x+y
            {
                indexer = indexer + 9; ;
            }
            if (indexer < 64 && indexer % 8 > 0 && ((table.poljas[indexer].figura.Vrsta == figurica.lovac && table.poljas[indexer].figura.boja != table.poljas[p.indexKralja].figura.boja) || (table.poljas[indexer].figura.Vrsta == figurica.kraljica && table.poljas[indexer].figura.boja != table.poljas[p.indexKralja].figura.boja)))
                return false;
            indexer = p.indexKralja + 7;
            while (indexer < 64 && indexer % 8 < 7 && !table.poljas[indexer].zauzeto)//+x+y
            {
                indexer = indexer + 7;
            }
            if (indexer < 64 && indexer % 8 < 7 && ((table.poljas[indexer].figura.Vrsta == figurica.lovac && table.poljas[indexer].figura.boja != table.poljas[p.indexKralja].figura.boja) || (table.poljas[indexer].figura.Vrsta == figurica.kraljica && table.poljas[indexer].figura.boja != table.poljas[p.indexKralja].figura.boja)))
                return false;
            //provera za pijune, mora za obe boje odvojeno.
            if(player1.naPotezu)
            {
                indexer = p.indexKralja - 7;
                if (indexer > 0 && indexer/8==(p.indexKralja-8)/8 &&(table.poljas[indexer].figura.Vrsta == figurica.pijun && table.poljas[indexer].figura.boja != table.poljas[p.indexKralja].figura.boja))
                    return false;
                indexer = p.indexKralja - 9;
                if (indexer >= 0 && indexer / 8 == (p.indexKralja -8)/ 8 && (table.poljas[indexer].figura.Vrsta == figurica.pijun && table.poljas[indexer].figura.boja != table.poljas[p.indexKralja].figura.boja))
                    return false;
            }
            else
            {
                indexer = p.indexKralja + 7;
                if (indexer < 64 && indexer / 8 == (p.indexKralja+8) / 8 && (table.poljas[indexer].figura.Vrsta == figurica.pijun && table.poljas[indexer].figura.boja != table.poljas[p.indexKralja].figura.boja))
                    return false;
                indexer = p.indexKralja + 9;
                if (indexer < 64 && indexer / 8 == (p.indexKralja+8) / 8 && (table.poljas[indexer].figura.Vrsta == figurica.pijun && table.poljas[indexer].figura.boja != table.poljas[p.indexKralja].figura.boja))
                    return false;
            }
            //i konj za kraj
            //konj moze biti potencijalna pretnja samo ako je na 8 odredjenih pozicija.
            indexer = p.indexKralja - 17;
            if (indexer < 64 && indexer >= 0 && (table.poljas[indexer].figura.Vrsta == figurica.konj && table.poljas[indexer].figura.boja != table.poljas[p.indexKralja].figura.boja))
                return false;
            indexer = p.indexKralja - 15;
            if (indexer < 64 && indexer >= 0 && (table.poljas[indexer].figura.Vrsta == figurica.konj && table.poljas[indexer].figura.boja != table.poljas[p.indexKralja].figura.boja))
                return false;
            indexer = p.indexKralja - 10;
            if (indexer < 64 && indexer >= 0 && (table.poljas[indexer].figura.Vrsta == figurica.konj && table.poljas[indexer].figura.boja != table.poljas[p.indexKralja].figura.boja))
                return false;
            indexer = p.indexKralja - 6;
            if (indexer < 64 && indexer >= 0 && (table.poljas[indexer].figura.Vrsta == figurica.konj && table.poljas[indexer].figura.boja != table.poljas[p.indexKralja].figura.boja))
                return false;
            indexer = p.indexKralja + 6;
            if (indexer < 64 && indexer >= 0 && (table.poljas[indexer].figura.Vrsta == figurica.konj && table.poljas[indexer].figura.boja != table.poljas[p.indexKralja].figura.boja))
                return false;
            indexer = p.indexKralja + 10;
            if (indexer < 64 && indexer >= 0 && (table.poljas[indexer].figura.Vrsta == figurica.konj && table.poljas[indexer].figura.boja != table.poljas[p.indexKralja].figura.boja))
                return false;
            indexer = p.indexKralja + 15;
            if (indexer < 64 && indexer >= 0 && (table.poljas[indexer].figura.Vrsta == figurica.konj && table.poljas[indexer].figura.boja != table.poljas[p.indexKralja].figura.boja))
                return false;
            indexer = p.indexKralja + 17;
            if (indexer < 64 && indexer >= 0 && (table.poljas[indexer].figura.Vrsta == figurica.konj && table.poljas[indexer].figura.boja != table.poljas[p.indexKralja].figura.boja))
                return false;
            //i kralj ne sme da pridje drugom kralju
            if (Math.Abs(player1.indexKralja - player2.indexKralja) <= 9  && Math.Abs(player1.indexKralja - player2.indexKralja) >= 7 &&
                Math.Abs(player1.indexKralja - player2.indexKralja) != 1 )
                return false;

            return true;


        }

        private void makeaMove(int oldIndex, int tabIndex, Figura f, Player p)
        {
            // table.poljas[tabIndex] = table.poljas[oldIndex];
            if(flowLayoutPanel1.Controls[tabIndex].BackgroundImage!=null)
            {
                p.brojFigurica--;
            }
            flowLayoutPanel1.Controls[tabIndex].BackgroundImage = flowLayoutPanel1.Controls[oldIndex].BackgroundImage;

            //  table.poljas[oldIndex] = new Polja();
            flowLayoutPanel1.Controls[oldIndex].BackgroundImage = null;

           // if (table.poljas[tabIndex].figura.Vrsta == figurica.kralj)
                //p.indexKralja = tabIndex;
        }

        private void countMove(Figura figura, int tabIndex)
        {
            if (figura.Vrsta == figurica.pijun)
            {
                if (!figura.boja && tabIndex / 8 > 0)//bele
                {
                    //table.poljas[tabIndex - 8].playable = true;
                    changePawnColor(tabIndex - 8, tabIndex);
                    //  if(tabIndex/8==6)
                    //   changePawnColor(tabIndex - 16, tabIndex);
                }
                else if (figura.boja && tabIndex / 8 < 7)//crne
                {
                    changePawnColor(tabIndex + 8, tabIndex);
                    // if (tabIndex / 8 == 1)
                    //    changePawnColor(tabIndex + 16, tabIndex);
                }
            }
            else if (figura.Vrsta == figurica.top)
            {
                int index = tabIndex - 8;
                bool pR = false;
                while (index >= 0 && !pR)//+y osa
                {
                    pR = changeColor(index, tabIndex);
                    index -= 8;
                }
                index = tabIndex + 8;
                pR = false;
                while (index < 64 && !pR)//-y
                {
                    pR = changeColor(index, tabIndex);
                    index += 8;
                }
                index = tabIndex + 1;
                pR = false;
                while (index / 8 == tabIndex / 8 && !pR)
                {
                    pR = changeColor(index, tabIndex);
                    index++;
                }
                pR = false;
                index = tabIndex - 1;
                while (index / 8 == tabIndex / 8 && index >= 0 && !pR)
                {
                    pR = changeColor(index, tabIndex);
                    index--;
                }

            }
            else if (figura.Vrsta == figurica.lovac)
            {
                int index = tabIndex;
                index -= 7;
                bool pR = false;
                while (index >= 0 && index % 8 > 0 && !pR)//+x+y
                {
                    pR = changeColor(index, tabIndex);
                    index -= 7;
                }
                pR = false;
                index = tabIndex;
                index -= 9;
                while (index >= 0 && index % 8 < 7 && !pR)//-x+y
                {
                    pR = changeColor(index, tabIndex);
                    index -= 9;
                }
                pR = false;
                index = tabIndex;
                index += 9;
                while (index < 64 && index % 8 > 0 && !pR)//+x-y
                {
                    pR = changeColor(index, tabIndex);
                    index += 9;
                }
                pR = false;
                index = tabIndex;
                index += 7;
                while (index < 64 && index % 8 < 7 && !pR)
                {
                    pR = changeColor(index, tabIndex);
                    index += 7;
                }
            }
            else if (figura.Vrsta == figurica.kraljica)
            {
                int index = tabIndex - 8;
                bool pR = false;
                while (index >= 0 && !pR)//+y osa
                {
                    pR = changeColor(index, tabIndex);
                    index -= 8;
                }
                pR = false;
                index = tabIndex + 8;
                while (index < 64 && !pR)//-y
                {
                    pR = changeColor(index, tabIndex);
                    index += 8;
                }
                pR = false;
                index = tabIndex + 1;
                while (index / 8 == tabIndex / 8 && !pR)
                {
                    pR = changeColor(index, tabIndex);
                    index++;
                }
                pR = false;
                index = tabIndex - 1;
                while (index / 8 == tabIndex / 8 && index >= 0 && !pR)
                {
                    pR = changeColor(index, tabIndex);
                    index--;
                }
                pR = false;
                index = tabIndex;
                index -= 7;
                while (index >= 0 && index % 8 > 0 && !pR)//+x+y
                {
                    pR = changeColor(index, tabIndex);
                    index -= 7;
                }
                pR = false;
                index = tabIndex;
                index -= 9;
                while (index >= 0 && index % 8 < 7 && !pR)//-x+y
                {
                    pR = changeColor(index, tabIndex);
                    index -= 9;
                }
                pR = false;
                index = tabIndex;
                index += 9;
                while (index < 64 && index % 8 > 0 && !pR)//+x-y
                {
                    pR = changeColor(index, tabIndex);
                    index += 9;
                }
                pR = false;
                index = tabIndex;
                index += 7;
                while (index < 64 && index % 8 < 7 && !pR)
                {
                    pR = changeColor(index, tabIndex);
                    index += 7;
                }
            }
            else if (figura.Vrsta == figurica.kralj)
            {
                int index = tabIndex - 8;

                if (index >= 0)//+y osa
                {
                    changeColor(index, tabIndex);
                    index -= 8;
                }
                index = tabIndex + 8;
                if (index < 64)//-y
                {
                    changeColor(index, tabIndex);
                    index += 8;
                }
                index = tabIndex + 1;
                if (index / 8 == tabIndex / 8)
                {
                    changeColor(index, tabIndex);
                    index++;
                }
                index = tabIndex - 1;
                if (index / 8 == tabIndex / 8 && index >= 0)
                {
                    changeColor(index, tabIndex);
                    index--;
                }
                index = tabIndex;
                index -= 7;
                if (index >= 0 && index % 8 > 0)//+x+y
                {
                    changeColor(index, tabIndex);
                    index -= 7;
                }
                index = tabIndex;
                index -= 9;
                if (index >= 0 && index % 8 < 7)//-x+y
                {
                    changeColor(index, tabIndex);
                    index -= 9;
                }
                index = tabIndex;
                index += 9;
                if (index < 64 && index % 8 > 0)//+x-y
                {
                    changeColor(index, tabIndex);
                    index += 9;
                }
                index = tabIndex;
                index += 7;
                if (index < 64 && index % 8 < 7)
                {
                    changeColor(index, tabIndex);
                    index += 7;
                }
            }
            else if (figura.Vrsta == figurica.konj)
            {
                int index = tabIndex;
                index -= 15;
                if (index >= 0)
                {
                    changeColor(index, tabIndex);
                }
                index = tabIndex;
                index -= 17;
                if (index >= 0)
                {
                    changeColor(index, tabIndex);
                }
                index = tabIndex;
                index += 15;
                if (index < 64)
                {
                    changeColor(index, tabIndex);
                }
                index = tabIndex;
                index += 17;
                if (index < 64)
                {
                    changeColor(index, tabIndex);
                }
                index = tabIndex;
                index -= 6;
                if (index % 8 > 0 && index >= 0)
                {
                    changeColor(index, tabIndex);
                }
                index = tabIndex;
                index -= 10;
                if (index % 8 < 7 && index >= 0)
                {
                    changeColor(index, tabIndex);
                }

                index = tabIndex;
                index += 6;
                if (index % 8 < 7 && index < 64)
                {
                    changeColor(index, tabIndex);
                }
                index = tabIndex;
                index += 10;
                if (index % 8 > 0 && index < 64)
                {
                    changeColor(index, tabIndex);
                }

            }
        }
        private bool changeColor(int a, int b)
        {
            if (table.poljas[a].zauzeto && table.poljas[a].figura.boja != table.poljas[b].figura.boja)//Ovde ce da ide, da se boja razlikuje od boje igraca koji je na potezu. Da olaksam sebi taj deo.
            {
                flowLayoutPanel1.Controls[a].BackColor = Color.MediumVioletRed;
                return true;
            }
            else if (table.poljas[a].zauzeto)
                return true;
            else
                flowLayoutPanel1.Controls[a].BackColor = Color.CadetBlue;
            return false;

        }
        private void changePawnColor(int a, int b)
        {
            if ((a-1)/8==a/8 && a-1>=0 && table.poljas[a - 1].zauzeto && table.poljas[a - 1].figura.boja != table.poljas[b].figura.boja)//Ovde ce da ide, da se boja razlikuje od boje igraca koji je na potezu. Da olaksam sebi taj deo.
            {
                flowLayoutPanel1.Controls[a - 1].BackColor = Color.MediumVioletRed;
            }
            if ((a+1)/8 ==a/8 && a+1<64&& table.poljas[a + 1].zauzeto && table.poljas[a + 1].figura.boja != table.poljas[b].figura.boja)
            {
                flowLayoutPanel1.Controls[a + 1].BackColor = Color.MediumVioletRed;

            }
            if (!table.poljas[a].zauzeto)
            {
                flowLayoutPanel1.Controls[a].BackColor = Color.CadetBlue;
                if (a+8<64 && a-8 >=0 && table.poljas[a - 8].figura.boja && !table.poljas[a + 8].zauzeto && b / 8 == 1)
                    flowLayoutPanel1.Controls[a + 8].BackColor = Color.CadetBlue;
                else if (a-8>=0 && a+8 < 64 &&!table.poljas[a + 8].figura.boja && !table.poljas[a - 8].zauzeto && b / 8 == 6)
                    flowLayoutPanel1.Controls[a - 8].BackColor = Color.CadetBlue;
            }

            else
                return; ;

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            

            

        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }

        private void timer1_Tick_1(object sender, EventArgs e)
        {
            label1.Text = "Player1 time: "+s1.Elapsed.Minutes.ToString()+":"+s1.Elapsed.Seconds.ToString();
            label2.Text = "Player2 time: " + s2.Elapsed.Minutes.ToString() + ":" + s2.Elapsed.Seconds.ToString();
            if(s1.Elapsed.Minutes==60)
            {
                timer1.Stop();
                MessageBox.Show("Igrac broj 1 je izgubio! Isteklo je vreme");
                player1.naPotezu = false;
                player2.naPotezu = false;
                s1.Stop();
            }
            if (s2.Elapsed.Minutes == 60)
            {
                timer1.Stop();
                MessageBox.Show("Igrac broj 2 je izgubio! Isteklo je vreme");
                player1.naPotezu = false;
                player2.naPotezu = false;
                s2.Stop();
            }
        }
    }
}
