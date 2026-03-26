namespace AutoClickerByGerman
{
    partial class MenuPrincipalForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            tituloPrincipal = new Label();
            btnMinecraftDungeon = new Button();
            btnAqw = new Button();
            panelContenedor = new Panel();
            panelContenedor.SuspendLayout();
            SuspendLayout();
            // 
            // tituloPrincipal
            // 
            tituloPrincipal.Dock = DockStyle.Top;
            tituloPrincipal.Font = new Font("Segoe UI", 20F, FontStyle.Bold, GraphicsUnit.Point);
            tituloPrincipal.ForeColor = Color.FromArgb(15, 23, 42);
            tituloPrincipal.Location = new Point(0, 0);
            tituloPrincipal.Name = "tituloPrincipal";
            tituloPrincipal.Size = new Size(520, 80);
            tituloPrincipal.TabIndex = 0;
            tituloPrincipal.Text = "AutoClicker By German";
            tituloPrincipal.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btnMinecraftDungeon
            // 
            btnMinecraftDungeon.BackColor = Color.FromArgb(34, 197, 94);
            btnMinecraftDungeon.FlatAppearance.BorderSize = 0;
            btnMinecraftDungeon.FlatAppearance.MouseOverBackColor = Color.FromArgb(22, 163, 74);
            btnMinecraftDungeon.FlatStyle = FlatStyle.Flat;
            btnMinecraftDungeon.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point);
            btnMinecraftDungeon.ForeColor = Color.White;
            btnMinecraftDungeon.Location = new Point(110, 115);
            btnMinecraftDungeon.Name = "btnMinecraftDungeon";
            btnMinecraftDungeon.Size = new Size(300, 52);
            btnMinecraftDungeon.TabIndex = 1;
            btnMinecraftDungeon.Text = "Minecraft Dungeon";
            btnMinecraftDungeon.UseVisualStyleBackColor = false;
            btnMinecraftDungeon.Click += btnMinecraftDungeon_Click;
            // 
            // btnAqw
            // 
            btnAqw.BackColor = Color.FromArgb(37, 99, 235);
            btnAqw.FlatAppearance.BorderSize = 0;
            btnAqw.FlatAppearance.MouseOverBackColor = Color.FromArgb(29, 78, 216);
            btnAqw.FlatStyle = FlatStyle.Flat;
            btnAqw.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point);
            btnAqw.ForeColor = Color.White;
            btnAqw.Location = new Point(110, 185);
            btnAqw.Name = "btnAqw";
            btnAqw.Size = new Size(300, 52);
            btnAqw.TabIndex = 2;
            btnAqw.Text = "AQW";
            btnAqw.UseVisualStyleBackColor = false;
            btnAqw.Click += btnAqw_Click;
            // 
            // panelContenedor
            // 
            panelContenedor.BackColor = Color.FromArgb(248, 250, 252);
            panelContenedor.Controls.Add(btnAqw);
            panelContenedor.Controls.Add(btnMinecraftDungeon);
            panelContenedor.Controls.Add(tituloPrincipal);
            panelContenedor.Dock = DockStyle.Fill;
            panelContenedor.Location = new Point(0, 0);
            panelContenedor.Name = "panelContenedor";
            panelContenedor.Size = new Size(520, 310);
            panelContenedor.TabIndex = 3;
            // 
            // MenuPrincipalForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(226, 232, 240);
            ClientSize = new Size(520, 310);
            Controls.Add(panelContenedor);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "MenuPrincipalForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Menu principal";
            panelContenedor.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Label tituloPrincipal;
        private Button btnMinecraftDungeon;
        private Button btnAqw;
        private Panel panelContenedor;
    }
}
