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
            btnCapturarVentana = new Button();
            btnIniciarDetener = new Button();
            btnVolver = new Button();
            SuspendLayout();
            // 
            // lblTitulo
            // 
            lblTitulo.Dock = DockStyle.Top;
            lblTitulo.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point);
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
            chkAutoatack.Checked = true;
            chkAutoatack.CheckState = CheckState.Checked;
            chkAutoatack.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            chkAutoatack.ForeColor = Color.White;
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
            chkVhl.ForeColor = Color.White;
            chkVhl.Location = new Point(225, 155);
            chkVhl.Name = "chkVhl";
            chkVhl.Size = new Size(47, 19);
            chkVhl.TabIndex = 4;
            chkVhl.Text = "VHL";
            chkVhl.UseVisualStyleBackColor = true;
            chkVhl.CheckedChanged += chkVhl_CheckedChanged;
            // 
            // btnCapturarVentana
            // 
            btnCapturarVentana.BackColor = Color.FromArgb(231, 76, 60);
            btnCapturarVentana.FlatAppearance.BorderSize = 0;
            btnCapturarVentana.FlatStyle = FlatStyle.Flat;
            btnCapturarVentana.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
            btnCapturarVentana.ForeColor = Color.White;
            btnCapturarVentana.Location = new Point(92, 184);
            btnCapturarVentana.Name = "btnCapturarVentana";
            btnCapturarVentana.Size = new Size(216, 38);
            btnCapturarVentana.TabIndex = 5;
            btnCapturarVentana.Text = "Capturar ventana (3s)";
            btnCapturarVentana.UseVisualStyleBackColor = false;
            btnCapturarVentana.Click += btnCapturarVentana_Click;
            // 
            // btnIniciarDetener
            // 
            btnIniciarDetener.BackColor = Color.FromArgb(52, 152, 219);
            btnIniciarDetener.FlatAppearance.BorderSize = 0;
            btnIniciarDetener.FlatStyle = FlatStyle.Flat;
            btnIniciarDetener.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point);
            btnIniciarDetener.ForeColor = Color.White;
            btnIniciarDetener.Location = new Point(120, 232);
            btnIniciarDetener.Name = "btnIniciarDetener";
            btnIniciarDetener.Size = new Size(160, 48);
            btnIniciarDetener.TabIndex = 6;
            btnIniciarDetener.Text = "Iniciar";
            btnIniciarDetener.UseVisualStyleBackColor = false;
            btnIniciarDetener.Click += btnIniciarDetener_Click;
            // 
            // btnVolver
            // 
            btnVolver.BackColor = Color.FromArgb(127, 140, 141);
            btnVolver.FlatAppearance.BorderSize = 0;
            btnVolver.FlatStyle = FlatStyle.Flat;
            btnVolver.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
            btnVolver.ForeColor = Color.White;
            btnVolver.Location = new Point(120, 288);
            btnVolver.Name = "btnVolver";
            btnVolver.Size = new Size(160, 36);
            btnVolver.TabIndex = 7;
            btnVolver.Text = "Volver";
            btnVolver.UseVisualStyleBackColor = false;
            btnVolver.Click += btnVolver_Click;
            // 
            // AqwForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(24, 26, 27);
            ClientSize = new Size(400, 340);
            Controls.Add(btnVolver);
            Controls.Add(btnIniciarDetener);
            Controls.Add(btnCapturarVentana);
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
        private Button btnCapturarVentana;
        private Button btnIniciarDetener;
        private Button btnVolver;
    }
}
