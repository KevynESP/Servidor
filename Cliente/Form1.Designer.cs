namespace Cliente
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtMensaje = new System.Windows.Forms.TextBox();
            this.btnEnviar = new System.Windows.Forms.Button();
            this.btnConectar = new System.Windows.Forms.Button();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.txtChat = new System.Windows.Forms.TextBox();
            this.lbUsuarios = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.BtnAnadir = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtMensaje
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.txtMensaje, 2);
            this.txtMensaje.Location = new System.Drawing.Point(3, 513);
            this.txtMensaje.MaxLength = 256;
            this.txtMensaje.Name = "txtMensaje";
            this.txtMensaje.Size = new System.Drawing.Size(712, 22);
            this.txtMensaje.TabIndex = 2;
            this.txtMensaje.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox1_KeyPress);
            // 
            // btnEnviar
            // 
            this.btnEnviar.Location = new System.Drawing.Point(721, 513);
            this.btnEnviar.Name = "btnEnviar";
            this.btnEnviar.Size = new System.Drawing.Size(69, 25);
            this.btnEnviar.TabIndex = 1;
            this.btnEnviar.Text = "Enviar";
            this.btnEnviar.UseVisualStyleBackColor = true;
            this.btnEnviar.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnConectar
            // 
            this.btnConectar.Location = new System.Drawing.Point(721, 3);
            this.btnConectar.Name = "btnConectar";
            this.btnConectar.Size = new System.Drawing.Size(86, 25);
            this.btnConectar.TabIndex = 1;
            this.btnConectar.Text = "Conectarse";
            this.btnConectar.UseVisualStyleBackColor = true;
            this.btnConectar.Click += new System.EventHandler(this.button2_Click);
            // 
            // txtNombre
            // 
            this.txtNombre.Location = new System.Drawing.Point(152, 3);
            this.txtNombre.MaxLength = 15;
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(563, 22);
            this.txtNombre.TabIndex = 0;
            this.txtNombre.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox2_KeyPress);
            // 
            // txtChat
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.txtChat, 2);
            this.txtChat.Location = new System.Drawing.Point(152, 39);
            this.txtChat.Multiline = true;
            this.txtChat.Name = "txtChat";
            this.txtChat.ReadOnly = true;
            this.tableLayoutPanel1.SetRowSpan(this.txtChat, 2);
            this.txtChat.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtChat.Size = new System.Drawing.Size(662, 465);
            this.txtChat.TabIndex = 4;
            // 
            // lbUsuarios
            // 
            this.lbUsuarios.FormattingEnabled = true;
            this.lbUsuarios.ItemHeight = 16;
            this.lbUsuarios.Location = new System.Drawing.Point(3, 68);
            this.lbUsuarios.Name = "lbUsuarios";
            this.lbUsuarios.Size = new System.Drawing.Size(134, 436);
            this.lbUsuarios.TabIndex = 5;
            this.lbUsuarios.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.listboxDibujar);
            this.lbUsuarios.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(3, 7, 0, 0);
            this.label1.Size = new System.Drawing.Size(76, 23);
            this.label1.TabIndex = 6;
            this.label1.Text = "UserName";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.7367F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 79.2633F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 98F));
            this.tableLayoutPanel1.Controls.Add(this.txtNombre, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnEnviar, 3, 3);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.txtChat, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.lbUsuarios, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.btnConectar, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.txtMensaje, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.BtnAnadir, 0, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 12);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 99.99999F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 445F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 44F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(817, 554);
            this.tableLayoutPanel1.TabIndex = 7;
            // 
            // BtnAnadir
            // 
            this.BtnAnadir.Location = new System.Drawing.Point(3, 39);
            this.BtnAnadir.Name = "BtnAnadir";
            this.BtnAnadir.Size = new System.Drawing.Size(75, 23);
            this.BtnAnadir.TabIndex = 7;
            this.BtnAnadir.Text = "Añadir";
            this.BtnAnadir.UseVisualStyleBackColor = true;
            this.BtnAnadir.Visible = false;
            this.BtnAnadir.Click += new System.EventHandler(this.BtnAnadir_Click);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(0, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 23);
            this.label3.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 23);
            this.label2.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(841, 558);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "Form1";
            this.Text = "MakaApp";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtMensaje;
        private System.Windows.Forms.Button btnEnviar;
        private System.Windows.Forms.Button btnConectar;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.TextBox txtChat;
        private System.Windows.Forms.ListBox lbUsuarios;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button BtnAnadir;
    }
}

