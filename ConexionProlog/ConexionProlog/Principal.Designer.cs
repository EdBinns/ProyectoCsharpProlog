
namespace ConexionProlog
{
	partial class Principal
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
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
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.button1 = new System.Windows.Forms.Button();
            this.tabla = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.tbMatriz = new System.Windows.Forms.TextBox();
            this.btnRandom = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.clean = new System.Windows.Forms.Button();
            this.btnConsult = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.tbSearch = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.tabla)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(16, 145);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(222, 38);
            this.button1.TabIndex = 0;
            this.button1.Text = "Crear Matriz";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // tabla
            // 
            this.tabla.BackgroundColor = System.Drawing.SystemColors.Control;
            this.tabla.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tabla.GridColor = System.Drawing.SystemColors.ActiveCaption;
            this.tabla.Location = new System.Drawing.Point(244, 12);
            this.tabla.Name = "tabla";
            this.tabla.RowHeadersWidth = 10;
            this.tabla.RowTemplate.Height = 28;
            this.tabla.Size = new System.Drawing.Size(1239, 749);
            this.tabla.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 68);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(226, 20);
            this.label1.TabIndex = 3;
            this.label1.Text = "Ingrese el tamaño de la matriz:";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // tbMatriz
            // 
            this.tbMatriz.Location = new System.Drawing.Point(16, 100);
            this.tbMatriz.Name = "tbMatriz";
            this.tbMatriz.Size = new System.Drawing.Size(222, 26);
            this.tbMatriz.TabIndex = 4;
            this.tbMatriz.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // btnRandom
            // 
            this.btnRandom.Location = new System.Drawing.Point(16, 232);
            this.btnRandom.Name = "btnRandom";
            this.btnRandom.Size = new System.Drawing.Size(222, 40);
            this.btnRandom.TabIndex = 6;
            this.btnRandom.Text = "LLenar tabla random";
            this.btnRandom.UseVisualStyleBackColor = true;
            this.btnRandom.Click += new System.EventHandler(this.btnRandom_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.SystemColors.HotTrack;
            this.button2.Location = new System.Drawing.Point(16, 326);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(222, 38);
            this.button2.TabIndex = 10;
            this.button2.Text = "Iniciar actividad";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // clean
            // 
            this.clean.Location = new System.Drawing.Point(16, 189);
            this.clean.Name = "clean";
            this.clean.Size = new System.Drawing.Size(222, 37);
            this.clean.TabIndex = 8;
            this.clean.Text = "Limpiar matriz";
            this.clean.UseVisualStyleBackColor = true;
            this.clean.Click += new System.EventHandler(this.clean_Click);
            // 
            // btnConsult
            // 
            this.btnConsult.Location = new System.Drawing.Point(16, 278);
            this.btnConsult.Name = "btnConsult";
            this.btnConsult.Size = new System.Drawing.Size(222, 32);
            this.btnConsult.TabIndex = 9;
            this.btnConsult.Text = "Encontrar todos los grupos";
            this.btnConsult.UseVisualStyleBackColor = true;
            this.btnConsult.Click += new System.EventHandler(this.btnConsult_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(26, 430);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(201, 20);
            this.label2.TabIndex = 11;
            this.label2.Text = "Buscar grupos por tamaño:";
            // 
            // tbSearch
            // 
            this.tbSearch.Location = new System.Drawing.Point(16, 454);
            this.tbSearch.Name = "tbSearch";
            this.tbSearch.Size = new System.Drawing.Size(211, 26);
            this.tbSearch.TabIndex = 12;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(16, 486);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(201, 33);
            this.btnSearch.TabIndex = 13;
            this.btnSearch.Text = "Buscar";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.button3_Click);
            // 
            // Principal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1521, 765);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.tbSearch);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnConsult);
            this.Controls.Add(this.clean);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.btnRandom);
            this.Controls.Add(this.tbMatriz);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tabla);
            this.Controls.Add(this.button1);
            this.Name = "Principal";
            this.Text = "Form1";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Principal_Load);
            ((System.ComponentModel.ISupportInitialize)(this.tabla)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridView tabla;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbMatriz;
        private System.Windows.Forms.Button btnRandom;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button clean;
        private System.Windows.Forms.Button btnConsult;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbSearch;
        private System.Windows.Forms.Button btnSearch;
    }
}

