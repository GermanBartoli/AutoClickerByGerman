namespace AutoClickerByGerman
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnStartStop = new Button();
            txtIntervaloClick = new TextBox();
            labelStatus = new Label();
            button1 = new Button();
            SuspendLayout();
            // 
            // btnStartStop
            // 
            btnStartStop.Location = new Point(319, 349);
            btnStartStop.Name = "btnStartStop";
            btnStartStop.Size = new Size(75, 23);
            btnStartStop.TabIndex = 0;
            btnStartStop.Text = "button1";
            btnStartStop.UseVisualStyleBackColor = true;
            btnStartStop.Click += buttonStartStop_Click;
            // 
            // txtIntervaloClick
            // 
            txtIntervaloClick.Location = new Point(209, 215);
            txtIntervaloClick.Name = "txtIntervaloClick";
            txtIntervaloClick.Size = new Size(100, 23);
            txtIntervaloClick.TabIndex = 1;
            // 
            // labelStatus
            // 
            labelStatus.AutoSize = true;
            labelStatus.Location = new Point(172, 294);
            labelStatus.Name = "labelStatus";
            labelStatus.Size = new Size(38, 15);
            labelStatus.TabIndex = 2;
            labelStatus.Text = "label1";
            // 
            // button1
            // 
            button1.Location = new Point(493, 349);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 3;
            button1.Text = "button1";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click_1;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(button1);
            Controls.Add(labelStatus);
            Controls.Add(txtIntervaloClick);
            Controls.Add(btnStartStop);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnStartStop;
        private TextBox txtIntervaloClick;
        private Label labelStatus;
        private Button button1;
    }
}
