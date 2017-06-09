using System;
using System.Windows.Forms;

namespace batalha_naval
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
            this.boardPlayer = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pnlConexao = new System.Windows.Forms.Panel();
            this.btnConectar = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cbIPs = new System.Windows.Forms.ComboBox();
            this.gbGaragem = new System.Windows.Forms.GroupBox();
            this.pbSubmarino = new System.Windows.Forms.PictureBox();
            this.pbDestroier = new System.Windows.Forms.PictureBox();
            this.pbCruzador = new System.Windows.Forms.PictureBox();
            this.pbEncouracado = new System.Windows.Forms.PictureBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pbPortaAvioes = new System.Windows.Forms.PictureBox();
            this.boardEnemy = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.boardPlayer)).BeginInit();
            this.panel1.SuspendLayout();
            this.pnlConexao.SuspendLayout();
            this.gbGaragem.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbSubmarino)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbDestroier)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbCruzador)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbEncouracado)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPortaAvioes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.boardEnemy)).BeginInit();
            this.SuspendLayout();
            // 
            // boardPlayer
            // 
            this.boardPlayer.AllowDrop = true;
            this.boardPlayer.Location = new System.Drawing.Point(12, 12);
            this.boardPlayer.Name = "boardPlayer";
            this.boardPlayer.Size = new System.Drawing.Size(400, 400);
            this.boardPlayer.TabIndex = 0;
            this.boardPlayer.TabStop = false;
            this.boardPlayer.DragDrop += new System.Windows.Forms.DragEventHandler(this.board_DragDrop);
            this.boardPlayer.DragEnter += new System.Windows.Forms.DragEventHandler(this.board_DragEnter);
            this.boardPlayer.DragOver += new System.Windows.Forms.DragEventHandler(this.board_DragOver);
            this.boardPlayer.DragLeave += new System.EventHandler(this.board_DragLeave);
            this.boardPlayer.Paint += new System.Windows.Forms.PaintEventHandler(this.boardPlayer_Paint);
            this.boardPlayer.MouseEnter += new System.EventHandler(this.board_MouseEnter);
            this.boardPlayer.MouseLeave += new System.EventHandler(this.board_MouseLeave);
            this.boardPlayer.MouseMove += new System.Windows.Forms.MouseEventHandler(this.board_MouseMove);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.pnlConexao);
            this.panel1.Controls.Add(this.gbGaragem);
            this.panel1.Location = new System.Drawing.Point(418, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(240, 400);
            this.panel1.TabIndex = 1;
            // 
            // pnlConexao
            // 
            this.pnlConexao.Controls.Add(this.btnConectar);
            this.pnlConexao.Controls.Add(this.label1);
            this.pnlConexao.Controls.Add(this.cbIPs);
            this.pnlConexao.Location = new System.Drawing.Point(3, 3);
            this.pnlConexao.Name = "pnlConexao";
            this.pnlConexao.Size = new System.Drawing.Size(234, 72);
            this.pnlConexao.TabIndex = 4;
            this.pnlConexao.Visible = false;
            // 
            // btnConectar
            // 
            this.btnConectar.Location = new System.Drawing.Point(113, 49);
            this.btnConectar.Name = "btnConectar";
            this.btnConectar.Size = new System.Drawing.Size(121, 23);
            this.btnConectar.TabIndex = 2;
            this.btnConectar.Text = "Conectar";
            this.btnConectar.UseVisualStyleBackColor = true;
            this.btnConectar.Click += new System.EventHandler(this.btnConectar_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(0, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(113, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Disponíveis para jogar";
            // 
            // cbIPs
            // 
            this.cbIPs.FormattingEnabled = true;
            this.cbIPs.Location = new System.Drawing.Point(113, 22);
            this.cbIPs.Name = "cbIPs";
            this.cbIPs.Size = new System.Drawing.Size(121, 21);
            this.cbIPs.TabIndex = 0;
            // 
            // gbGaragem
            // 
            this.gbGaragem.Controls.Add(this.pbSubmarino);
            this.gbGaragem.Controls.Add(this.pbDestroier);
            this.gbGaragem.Controls.Add(this.pbCruzador);
            this.gbGaragem.Controls.Add(this.pbEncouracado);
            this.gbGaragem.Controls.Add(this.label7);
            this.gbGaragem.Controls.Add(this.label6);
            this.gbGaragem.Controls.Add(this.label5);
            this.gbGaragem.Controls.Add(this.label4);
            this.gbGaragem.Controls.Add(this.label3);
            this.gbGaragem.Controls.Add(this.label2);
            this.gbGaragem.Controls.Add(this.pbPortaAvioes);
            this.gbGaragem.Location = new System.Drawing.Point(3, 0);
            this.gbGaragem.Name = "gbGaragem";
            this.gbGaragem.Size = new System.Drawing.Size(234, 397);
            this.gbGaragem.TabIndex = 3;
            this.gbGaragem.TabStop = false;
            this.gbGaragem.Text = "Garagem";
            // 
            // pbSubmarino
            // 
            this.pbSubmarino.Image = global::batalha_naval.Properties.Resources.Submarino;
            this.pbSubmarino.Location = new System.Drawing.Point(9, 325);
            this.pbSubmarino.Name = "pbSubmarino";
            this.pbSubmarino.Size = new System.Drawing.Size(80, 40);
            this.pbSubmarino.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbSubmarino.TabIndex = 10;
            this.pbSubmarino.TabStop = false;
            this.pbSubmarino.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ArrasteBarco);
            // 
            // pbDestroier
            // 
            this.pbDestroier.Image = global::batalha_naval.Properties.Resources.Destroier;
            this.pbDestroier.Location = new System.Drawing.Point(9, 254);
            this.pbDestroier.Name = "pbDestroier";
            this.pbDestroier.Size = new System.Drawing.Size(80, 40);
            this.pbDestroier.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbDestroier.TabIndex = 9;
            this.pbDestroier.TabStop = false;
            this.pbDestroier.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ArrasteBarco);
            // 
            // pbCruzador
            // 
            this.pbCruzador.Image = global::batalha_naval.Properties.Resources.Cruzador;
            this.pbCruzador.Location = new System.Drawing.Point(9, 183);
            this.pbCruzador.Name = "pbCruzador";
            this.pbCruzador.Size = new System.Drawing.Size(120, 40);
            this.pbCruzador.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbCruzador.TabIndex = 8;
            this.pbCruzador.TabStop = false;
            this.pbCruzador.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ArrasteBarco);
            // 
            // pbEncouracado
            // 
            this.pbEncouracado.Image = global::batalha_naval.Properties.Resources.Encouracado;
            this.pbEncouracado.Location = new System.Drawing.Point(9, 112);
            this.pbEncouracado.Name = "pbEncouracado";
            this.pbEncouracado.Size = new System.Drawing.Size(160, 40);
            this.pbEncouracado.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbEncouracado.TabIndex = 7;
            this.pbEncouracado.TabStop = false;
            this.pbEncouracado.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ArrasteBarco);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 309);
            this.label7.Margin = new System.Windows.Forms.Padding(3, 12, 3, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(57, 13);
            this.label7.TabIndex = 6;
            this.label7.Text = "Submarino";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 238);
            this.label6.Margin = new System.Windows.Forms.Padding(3, 12, 3, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(49, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "Destroier";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 167);
            this.label5.Margin = new System.Windows.Forms.Padding(3, 12, 3, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(49, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Cruzador";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 96);
            this.label4.Margin = new System.Windows.Forms.Padding(3, 12, 3, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Encouraçado";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Porta-Aviões";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(98, 362);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(130, 32);
            this.label2.TabIndex = 1;
            this.label2.Text = "Pressione Enter enquanto arrasta para virar o barco!";
            // 
            // pbPortaAvioes
            // 
            this.pbPortaAvioes.Image = global::batalha_naval.Properties.Resources.PortaAvioes;
            this.pbPortaAvioes.Location = new System.Drawing.Point(9, 41);
            this.pbPortaAvioes.Name = "pbPortaAvioes";
            this.pbPortaAvioes.Size = new System.Drawing.Size(200, 40);
            this.pbPortaAvioes.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbPortaAvioes.TabIndex = 0;
            this.pbPortaAvioes.TabStop = false;
            this.pbPortaAvioes.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ArrasteBarco);
            // 
            // boardEnemy
            // 
            this.boardEnemy.Location = new System.Drawing.Point(418, 12);
            this.boardEnemy.Name = "boardEnemy";
            this.boardEnemy.Size = new System.Drawing.Size(400, 400);
            this.boardEnemy.TabIndex = 2;
            this.boardEnemy.TabStop = false;
            this.boardEnemy.Visible = false;
            this.boardEnemy.Paint += new System.Windows.Forms.PaintEventHandler(this.boardEnemy_Paint);
            this.boardEnemy.MouseDown += new System.Windows.Forms.MouseEventHandler(this.boardEnemy_MouseDown);
            this.boardEnemy.MouseEnter += new System.EventHandler(this.boardEnemy_MouseEnter);
            this.boardEnemy.MouseLeave += new System.EventHandler(this.boardEnemy_MouseLeave);
            this.boardEnemy.MouseMove += new System.Windows.Forms.MouseEventHandler(this.boardEnemy_MouseMove);
            // 
            // GameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(670, 424);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.boardPlayer);
            this.Controls.Add(this.boardEnemy);
            this.Name = "GameForm";
            this.Text = "Batalha Naval";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.GameForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.boardPlayer)).EndInit();
            this.panel1.ResumeLayout(false);
            this.pnlConexao.ResumeLayout(false);
            this.pnlConexao.PerformLayout();
            this.gbGaragem.ResumeLayout(false);
            this.gbGaragem.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbSubmarino)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbDestroier)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbCruzador)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbEncouracado)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPortaAvioes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.boardEnemy)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox boardPlayer;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox gbGaragem;
        private System.Windows.Forms.Button btnConectar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbIPs;
        private System.Windows.Forms.Panel pnlConexao;
        private System.Windows.Forms.PictureBox pbPortaAvioes;
        private System.Windows.Forms.PictureBox pbSubmarino;
        private System.Windows.Forms.PictureBox pbDestroier;
        private System.Windows.Forms.PictureBox pbCruzador;
        private System.Windows.Forms.PictureBox pbEncouracado;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox boardEnemy;
    }
}

