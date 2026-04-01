namespace AutoClickerByGerman
{
    partial class AqwForm
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
            lblEstado = new Label();
            lblVentanaObjetivo = new Label();
            chkAutoatack = new CheckBox();
            chkVhl = new CheckBox();
            chkRevenant = new CheckBox();
            chkYami = new CheckBox();
            btnCapturarVentana = new Button();
            btnIniciarDetener = new Button();
            btnVolver = new Button();
            SuspendLayout();
            // 
            // lblTitulo
            // 
            lblTitulo.Dock = DockStyle.Top;
            lblTitulo.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point);
            lblTitulo.ForeColor = Color.FromArgb(17, 24, 39);
            lblTitulo.Location = new Point(0, 0);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(400, 60);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "Automatización AQW";
            lblTitulo.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblEstado
            // 
            lblEstado.AutoSize = false;
            lblEstado.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            lblEstado.ForeColor = Color.FromArgb(55, 65, 81);
            lblEstado.Location = new Point(30, 85);
            lblEstado.Name = "lblEstado";
            lblEstado.Size = new Size(340, 25);
            lblEstado.TabIndex = 1;
            lblEstado.Text = "Estado: detenido";
            lblEstado.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblVentanaObjetivo
            // 
            lblVentanaObjetivo.AutoSize = false;
            lblVentanaObjetivo.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            lblVentanaObjetivo.ForeColor = Color.FromArgb(75, 85, 99);
            lblVentanaObjetivo.Location = new Point(20, 115);
            lblVentanaObjetivo.Name = "lblVentanaObjetivo";
            lblVentanaObjetivo.Size = new Size(360, 36);
            lblVentanaObjetivo.TabIndex = 2;
            lblVentanaObjetivo.Text = "Ventana objetivo: no seleccionada";
            lblVentanaObjetivo.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // chkAutoatack
            // 
            chkAutoatack.AutoSize = true;
            chkAutoatack.Enabled = false;
            chkAutoatack.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            chkAutoatack.ForeColor = Color.Gray;
            chkAutoatack.Location = new Point(92, 155);
            chkAutoatack.Name = "chkAutoatack";
            chkAutoatack.Size = new Size(87, 19);
            chkAutoatack.TabIndex = 3;
            chkAutoatack.Text = "Autoatack";
            chkAutoatack.UseVisualStyleBackColor = true;
            chkAutoatack.CheckedChanged += chkAutoatack_CheckedChanged;
            // 
            // chkVhl
            // 
            chkVhl.AutoSize = true;
            chkVhl.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            chkVhl.ForeColor = Color.FromArgb(31, 41, 55);
            chkVhl.Location = new Point(225, 155);
            chkVhl.Name = "chkVhl";
            chkVhl.Size = new Size(47, 19);
            chkVhl.TabIndex = 4;
            chkVhl.Text = "VHL";
            chkVhl.UseVisualStyleBackColor = true;
            chkVhl.CheckedChanged += chkVhl_CheckedChanged;
            // 
            // chkRevenant
            // 
            chkRevenant.AutoSize = true;
            chkRevenant.Checked = true;
            chkRevenant.CheckState = CheckState.Checked;
            chkRevenant.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            chkRevenant.ForeColor = Color.FromArgb(31, 41, 55);
            chkRevenant.Location = new Point(114, 178);
            chkRevenant.Name = "chkRevenant";
            chkRevenant.Size = new Size(83, 19);
            chkRevenant.TabIndex = 5;
            chkRevenant.Text = "Revenant";
            chkRevenant.UseVisualStyleBackColor = true;
            chkRevenant.CheckedChanged += chkRevenant_CheckedChanged;
            // 
            // chkYami
            // 
            chkYami.AutoSize = true;
            chkYami.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            chkYami.ForeColor = Color.FromArgb(31, 41, 55);
            chkYami.Location = new Point(225, 178);
            chkYami.Name = "chkYami";
            chkYami.Size = new Size(53, 19);
            chkYami.TabIndex = 6;
            chkYami.Text = "Yami";
            chkYami.UseVisualStyleBackColor = true;
            chkYami.CheckedChanged += chkYami_CheckedChanged;
            // 
            // btnCapturarVentana
            // 
            btnCapturarVentana.BackColor = Color.FromArgb(239, 68, 68);
            btnCapturarVentana.FlatAppearance.BorderSize = 0;
            btnCapturarVentana.FlatAppearance.MouseOverBackColor = Color.FromArgb(220, 38, 38);
            btnCapturarVentana.FlatStyle = FlatStyle.Flat;
            btnCapturarVentana.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
            btnCapturarVentana.ForeColor = Color.White;
            btnCapturarVentana.Location = new Point(92, 208);
            btnCapturarVentana.Name = "btnCapturarVentana";
            btnCapturarVentana.Size = new Size(216, 38);
            btnCapturarVentana.TabIndex = 7;
            btnCapturarVentana.Text = "Capturar ventana (3s)";
            btnCapturarVentana.UseVisualStyleBackColor = false;
            btnCapturarVentana.Click += btnCapturarVentana_Click;
            // 
            // btnIniciarDetener
            // 
            btnIniciarDetener.BackColor = Color.FromArgb(37, 99, 235);
            btnIniciarDetener.FlatAppearance.BorderSize = 0;
            btnIniciarDetener.FlatAppearance.MouseOverBackColor = Color.FromArgb(29, 78, 216);
            btnIniciarDetener.FlatStyle = FlatStyle.Flat;
            btnIniciarDetener.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point);
            btnIniciarDetener.ForeColor = Color.White;
            btnIniciarDetener.Location = new Point(120, 256);
            btnIniciarDetener.Name = "btnIniciarDetener";
            btnIniciarDetener.Size = new Size(160, 48);
            btnIniciarDetener.TabIndex = 8;
            btnIniciarDetener.Text = "Iniciar";
            btnIniciarDetener.UseVisualStyleBackColor = false;
            btnIniciarDetener.Click += btnIniciarDetener_Click;
            // 
            // btnVolver
            // 
            btnVolver.BackColor = Color.FromArgb(100, 116, 139);
            btnVolver.FlatAppearance.BorderSize = 0;
            btnVolver.FlatAppearance.MouseOverBackColor = Color.FromArgb(71, 85, 105);
            btnVolver.FlatStyle = FlatStyle.Flat;
            btnVolver.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
            btnVolver.ForeColor = Color.White;
            btnVolver.Location = new Point(120, 312);
            btnVolver.Name = "btnVolver";
            btnVolver.Size = new Size(160, 36);
            btnVolver.TabIndex = 9;
            btnVolver.Text = "Volver";
            btnVolver.UseVisualStyleBackColor = false;
            btnVolver.Click += btnVolver_Click;
            // 
            // AqwForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(241, 245, 249);
            ClientSize = new Size(400, 365);
            Controls.Add(btnVolver);
            Controls.Add(btnIniciarDetener);
            Controls.Add(btnCapturarVentana);
            Controls.Add(chkYami);
            Controls.Add(chkRevenant);
            Controls.Add(chkVhl);
            Controls.Add(chkAutoatack);
            Controls.Add(lblVentanaObjetivo);
            Controls.Add(lblEstado);
            Controls.Add(lblTitulo);
            Name = "AqwForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "AQW - Automatización";
            ResumeLayout(false);
        }

        #endregion

        private Label lblTitulo;
        private Label lblEstado;
        private Label lblVentanaObjetivo;
        private CheckBox chkAutoatack;
        private CheckBox chkVhl;
        private CheckBox chkRevenant;
        private CheckBox chkYami;
        private Button btnCapturarVentana;
        private Button btnIniciarDetener;
        private Button btnVolver;
    }
}
