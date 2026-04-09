namespace AutoClickerByGerman
{
    partial class AutoClickerNormalForm
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
            lblTitulo = new Label();
            lblIntervalo = new Label();
            numIntervaloMs = new NumericUpDown();
            lblEstado = new Label();
            lblAtajo = new Label();
            btnIniciarDetener = new Button();
            btnVolver = new Button();
            panelPrincipal = new Panel();
            ((System.ComponentModel.ISupportInitialize)numIntervaloMs).BeginInit();
            panelPrincipal.SuspendLayout();
            SuspendLayout();
            // 
            // lblTitulo
            // 
            lblTitulo.Dock = DockStyle.Top;
            lblTitulo.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point);
            lblTitulo.ForeColor = Color.FromArgb(15, 23, 42);
            lblTitulo.Location = new Point(0, 0);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(520, 70);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "AutoClicker normal";
            lblTitulo.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblIntervalo
            // 
            lblIntervalo.AutoSize = true;
            lblIntervalo.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            lblIntervalo.ForeColor = Color.FromArgb(30, 41, 59);
            lblIntervalo.Location = new Point(74, 110);
            lblIntervalo.Name = "lblIntervalo";
            lblIntervalo.Size = new Size(169, 20);
            lblIntervalo.TabIndex = 1;
            lblIntervalo.Text = "Intervalo entre clicks (ms):";
            // 
            // numIntervaloMs
            // 
            numIntervaloMs.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            numIntervaloMs.Location = new Point(260, 106);
            numIntervaloMs.Maximum = new decimal(new int[] { 60000, 0, 0, 0 });
            numIntervaloMs.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numIntervaloMs.Name = "numIntervaloMs";
            numIntervaloMs.Size = new Size(180, 27);
            numIntervaloMs.TabIndex = 2;
            numIntervaloMs.Value = new decimal(new int[] { 100, 0, 0, 0 });
            numIntervaloMs.ValueChanged += numIntervaloMs_ValueChanged;
            // 
            // lblEstado
            // 
            lblEstado.AutoSize = true;
            lblEstado.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
            lblEstado.ForeColor = Color.FromArgb(15, 23, 42);
            lblEstado.Location = new Point(74, 158);
            lblEstado.Name = "lblEstado";
            lblEstado.Size = new Size(113, 19);
            lblEstado.TabIndex = 3;
            lblEstado.Text = "Estado: detenido";
            // 
            // lblAtajo
            // 
            lblAtajo.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            lblAtajo.ForeColor = Color.FromArgb(71, 85, 105);
            lblAtajo.Location = new Point(74, 180);
            lblAtajo.Name = "lblAtajo";
            lblAtajo.Size = new Size(366, 30);
            lblAtajo.TabIndex = 4;
            lblAtajo.Text = "Atajo global: pulsa 0 para iniciar o detener, incluso con la ventana minimizada.";
            // 
            // btnIniciarDetener
            // 
            btnIniciarDetener.BackColor = Color.FromArgb(34, 197, 94);
            btnIniciarDetener.FlatAppearance.BorderSize = 0;
            btnIniciarDetener.FlatAppearance.MouseOverBackColor = Color.FromArgb(22, 163, 74);
            btnIniciarDetener.FlatStyle = FlatStyle.Flat;
            btnIniciarDetener.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point);
            btnIniciarDetener.ForeColor = Color.White;
            btnIniciarDetener.Location = new Point(74, 210);
            btnIniciarDetener.Name = "btnIniciarDetener";
            btnIniciarDetener.Size = new Size(170, 48);
            btnIniciarDetener.TabIndex = 5;
            btnIniciarDetener.Text = "Iniciar";
            btnIniciarDetener.UseVisualStyleBackColor = false;
            btnIniciarDetener.Click += btnIniciarDetener_Click;
            // 
            // btnVolver
            // 
            btnVolver.BackColor = Color.FromArgb(37, 99, 235);
            btnVolver.FlatAppearance.BorderSize = 0;
            btnVolver.FlatAppearance.MouseOverBackColor = Color.FromArgb(29, 78, 216);
            btnVolver.FlatStyle = FlatStyle.Flat;
            btnVolver.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point);
            btnVolver.ForeColor = Color.White;
            btnVolver.Location = new Point(270, 210);
            btnVolver.Name = "btnVolver";
            btnVolver.Size = new Size(170, 48);
            btnVolver.TabIndex = 6;
            btnVolver.Text = "Volver";
            btnVolver.UseVisualStyleBackColor = false;
            btnVolver.Click += btnVolver_Click;
            // 
            // panelPrincipal
            // 
            panelPrincipal.BackColor = Color.FromArgb(248, 250, 252);
            panelPrincipal.Controls.Add(btnVolver);
            panelPrincipal.Controls.Add(btnIniciarDetener);
            panelPrincipal.Controls.Add(lblAtajo);
            panelPrincipal.Controls.Add(lblEstado);
            panelPrincipal.Controls.Add(numIntervaloMs);
            panelPrincipal.Controls.Add(lblIntervalo);
            panelPrincipal.Controls.Add(lblTitulo);
            panelPrincipal.Dock = DockStyle.Fill;
            panelPrincipal.Location = new Point(0, 0);
            panelPrincipal.Name = "panelPrincipal";
            panelPrincipal.Size = new Size(520, 320);
            panelPrincipal.TabIndex = 0;
            // 
            // AutoClickerNormalForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(226, 232, 240);
            ClientSize = new Size(520, 320);
            Controls.Add(panelPrincipal);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "AutoClickerNormalForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "AutoClicker normal";
            ((System.ComponentModel.ISupportInitialize)numIntervaloMs).EndInit();
            panelPrincipal.ResumeLayout(false);
            panelPrincipal.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Label lblTitulo;
        private Label lblIntervalo;
        private NumericUpDown numIntervaloMs;
        private Label lblEstado;
        private Label lblAtajo;
        private Button btnIniciarDetener;
        private Button btnVolver;
        private Panel panelPrincipal;
    }
}
