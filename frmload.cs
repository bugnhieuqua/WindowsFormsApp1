using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class frmload : Form
    {
        public frmload()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            // tiến trình load
            if (prgload.Value < 100)
            {
                prgload.Value += 1;
            }
            else
            {
                timer1.Stop();
                frmlevel1 game = new frmlevel1();
                this.Hide();
                game.Show();
            }
        }
    }
}
