﻿namespace batalha_naval
{
    partial class GameForm
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
            this.board = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.gbCaragem = new System.Windows.Forms.GroupBox();
            this.pnlConexao = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.board)).BeginInit();
            this.panel1.SuspendLayout();
            this.pnlConexao.SuspendLayout();
            this.SuspendLayout();
            // 
            // board
            // 
            this.board.Location = new System.Drawing.Point(12, 12);
            this.board.Name = "board";
            this.board.Size = new System.Drawing.Size(400, 400);
            this.board.TabIndex = 0;
            this.board.TabStop = false;
            this.board.Paint += new System.Windows.Forms.PaintEventHandler(this.board_Paint);
            this.board.MouseDown += new System.Windows.Forms.MouseEventHandler(this.board_MouseDown);
            this.board.MouseEnter += new System.EventHandler(this.board_MouseEnter);
            this.board.MouseLeave += new System.EventHandler(this.board_MouseLeave);
            this.board.MouseMove += new System.Windows.Forms.MouseEventHandler(this.board_MouseMove);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.gbCaragem);
            this.panel1.Controls.Add(this.pnlConexao);
            this.panel1.Location = new System.Drawing.Point(418, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(240, 400);
            this.panel1.TabIndex = 1;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(113, 22);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 21);
            this.comboBox1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(-6, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(113, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Disponíveis para jogar";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(113, 49);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(121, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Conectar";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // gbCaragem
            // 
            this.gbCaragem.Location = new System.Drawing.Point(3, 6);
            this.gbCaragem.Name = "gbCaragem";
            this.gbCaragem.Size = new System.Drawing.Size(234, 391);
            this.gbCaragem.TabIndex = 3;
            this.gbCaragem.TabStop = false;
            this.gbCaragem.Text = "Garagem";
            // 
            // pnlConexao
            // 
            this.pnlConexao.Controls.Add(this.button1);
            this.pnlConexao.Controls.Add(this.label1);
            this.pnlConexao.Controls.Add(this.comboBox1);
            this.pnlConexao.Location = new System.Drawing.Point(3, 3);
            this.pnlConexao.Name = "pnlConexao";
            this.pnlConexao.Size = new System.Drawing.Size(234, 72);
            this.pnlConexao.TabIndex = 4;
            this.pnlConexao.Visible = false;
            // 
            // GameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(670, 424);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.board);
            this.Name = "GameForm";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.board)).EndInit();
            this.panel1.ResumeLayout(false);
            this.pnlConexao.ResumeLayout(false);
            this.pnlConexao.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox board;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox gbCaragem;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Panel pnlConexao;
    }
}

