using System;
using System.Drawing;
using System.Windows.Forms;

namespace AutoClickerByGerman
{
    public partial class MenuPrincipalForm : Form
    {
        public MenuPrincipalForm()
        {
            InitializeComponent();
        }

        private void btnMinecraftDungeon_Click(object sender, EventArgs e)
        {
            using var form = new Form1();
            form.ShowDialog(this);
        }

        private void btnAqw_Click(object sender, EventArgs e)
        {
            using var form = new AqwForm();
            form.ShowDialog(this);
        }
    }
}
