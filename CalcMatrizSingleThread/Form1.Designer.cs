namespace CalcMatriz
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
            btCarregaMatriz = new Button();
            btCalculaMatriz = new Button();
            SuspendLayout();
            // 
            // btCarregaMatriz
            // 
            btCarregaMatriz.Location = new Point(277, 108);
            btCarregaMatriz.Name = "btCarregaMatriz";
            btCarregaMatriz.Size = new Size(184, 74);
            btCarregaMatriz.TabIndex = 0;
            btCarregaMatriz.Text = "Carregar Matriz";
            btCarregaMatriz.UseVisualStyleBackColor = true;
            btCarregaMatriz.Click += btCarregaMatriz_Click;
            // 
            // btCalculaMatriz
            // 
            btCalculaMatriz.Enabled = false;
            btCalculaMatriz.Location = new Point(277, 317);
            btCalculaMatriz.Name = "btCalculaMatriz";
            btCalculaMatriz.Size = new Size(184, 66);
            btCalculaMatriz.TabIndex = 1;
            btCalculaMatriz.Text = "Calcular Matrizes";
            btCalculaMatriz.UseVisualStyleBackColor = true;
            btCalculaMatriz.Click += btCalculaMatriz_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btCalculaMatriz);
            Controls.Add(btCarregaMatriz);
            Name = "Form1";
            Text = "Calculo matriz";
            ResumeLayout(false);
        }

        #endregion

        private Button btCarregaMatriz;
        private Button btCalculaMatriz;
    }
}
