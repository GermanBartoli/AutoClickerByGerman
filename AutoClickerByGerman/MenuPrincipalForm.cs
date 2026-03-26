using System;
using System.Drawing;
using System.Windows.Forms;

namespace AutoClickerByGerman
{
    public partial class MenuPrincipalForm : Form
    {
        public Form? FormularioSeleccionado { get; private set; }

        public MenuPrincipalForm()
        {
            InitializeComponent();
        }

        private void btnMinecraftDungeon_Click(object sender, EventArgs e)
        {
            FormularioSeleccionado = new Form1();
            Close();
        }

        private void btnAqw_Click(object sender, EventArgs e)
        {
            FormularioSeleccionado = new AqwForm();
            Close();
        }
    }
}
