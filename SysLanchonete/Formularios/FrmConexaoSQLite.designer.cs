using System.Drawing;
using System.Windows.Forms;

namespace SysLanchonete.Formularios
{
    partial class FrmConexaoSQLite
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmConexaoSQLite));
            this.BtSalvar = new ECTurbo.Controles.ECTurbo_Botao();
            this.btEscolherArquivo = new ECTurbo.Controles.ECTurbo_Botao();
            this.TxtBanco = new ECTurbo.Controles.ECTurbo_TextBox();
            this.ecTurbo_Label2 = new ECTurbo.Controles.ECTurbo_Label();
            this.ecTurbo_Imagem1 = new ECTurbo.Controles.ECTurbo_Imagem();
            ((System.ComponentModel.ISupportInitialize)(this.ecTurbo_Imagem1)).BeginInit();
            this.SuspendLayout();
            // 
            // BtSalvar
            // 
            this.BtSalvar.Angulo = 120;
            this.BtSalvar.Arred = 10;
            this.BtSalvar.AtivarSombra = false;
            this.BtSalvar.Cor1 = System.Drawing.Color.LightSkyBlue;
            this.BtSalvar.Cor2 = System.Drawing.Color.Navy;
            this.BtSalvar.CorBorda = System.Drawing.Color.Gray;
            this.BtSalvar.CorSombra = System.Drawing.Color.Black;
            this.BtSalvar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtSalvar.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtSalvar.ForeColor = System.Drawing.Color.White;
            this.BtSalvar.Location = new System.Drawing.Point(97, 245);
            this.BtSalvar.Name = "BtSalvar";
            this.BtSalvar.PosicaoImagem = ECTurbo.Controles.ECTurbo_Botao.Posicoes.Esquerda;
            this.BtSalvar.Size = new System.Drawing.Size(309, 37);
            this.BtSalvar.TabIndex = 3;
            this.BtSalvar.TamanhoIcone = 16;
            this.BtSalvar.TamanhoSombra = 3;
            this.BtSalvar.TamBorda = 0;
            this.BtSalvar.Text = "Salvar e Testar a Conexão";
            this.BtSalvar.UseVisualStyleBackColor = true;
            this.BtSalvar.Click += new System.EventHandler(this.BtSalvar_Click);
            // 
            // btEscolherArquivo
            // 
            this.btEscolherArquivo.Angulo = 90;
            this.btEscolherArquivo.Arred = 10;
            this.btEscolherArquivo.AtivarSombra = false;
            this.btEscolherArquivo.Cor1 = System.Drawing.Color.LightSkyBlue;
            this.btEscolherArquivo.Cor2 = System.Drawing.Color.Navy;
            this.btEscolherArquivo.CorBorda = System.Drawing.Color.Gray;
            this.btEscolherArquivo.CorSombra = System.Drawing.Color.Black;
            this.btEscolherArquivo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btEscolherArquivo.ForeColor = System.Drawing.Color.White;
            this.btEscolherArquivo.Location = new System.Drawing.Point(378, 200);
            this.btEscolherArquivo.Name = "btEscolherArquivo";
            this.btEscolherArquivo.PosicaoImagem = ECTurbo.Controles.ECTurbo_Botao.Posicoes.Esquerda;
            this.btEscolherArquivo.Size = new System.Drawing.Size(28, 23);
            this.btEscolherArquivo.TabIndex = 3;
            this.btEscolherArquivo.TamanhoIcone = 16;
            this.btEscolherArquivo.TamanhoSombra = 3;
            this.btEscolherArquivo.TamBorda = 0;
            this.btEscolherArquivo.Text = "...";
            this.btEscolherArquivo.UseVisualStyleBackColor = true;
            this.btEscolherArquivo.Click += new System.EventHandler(this.btEscolherArquivo_Click);
            // 
            // TxtBanco
            // 
            this.TxtBanco.BackColor = System.Drawing.Color.White;
            this.TxtBanco.Coluna = "";
            this.TxtBanco.Font = new System.Drawing.Font("Century Gothic", 12F);
            this.TxtBanco.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.TxtBanco.Location = new System.Drawing.Point(97, 200);
            this.TxtBanco.Name = "TxtBanco";
            this.TxtBanco.Obgt = false;
            this.TxtBanco.ReadOnly = true;
            this.TxtBanco.Size = new System.Drawing.Size(279, 27);
            this.TxtBanco.TabIndex = 2;
            this.TxtBanco.Tag = "";
            this.TxtBanco.Unico = "";
            // 
            // ecTurbo_Label2
            // 
            this.ecTurbo_Label2.Angulo = 45;
            this.ecTurbo_Label2.AtivarSombra = false;
            this.ecTurbo_Label2.AutoSize = true;
            this.ecTurbo_Label2.Cor1 = System.Drawing.Color.RoyalBlue;
            this.ecTurbo_Label2.Cor2 = System.Drawing.Color.Navy;
            this.ecTurbo_Label2.CorSombra = System.Drawing.Color.Firebrick;
            this.ecTurbo_Label2.EspacoTexto = 5;
            this.ecTurbo_Label2.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ecTurbo_Label2.Location = new System.Drawing.Point(104, 180);
            this.ecTurbo_Label2.Name = "ecTurbo_Label2";
            this.ecTurbo_Label2.Size = new System.Drawing.Size(307, 20);
            this.ecTurbo_Label2.TabIndex = 1;
            this.ecTurbo_Label2.Text = "Selecione o arquivo do banco de dados";
            this.ecTurbo_Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ecTurbo_Label2.X = 2;
            this.ecTurbo_Label2.Y = 2;
            // 
            // ecTurbo_Imagem1
            // 
            this.ecTurbo_Imagem1.Coluna = "";
            this.ecTurbo_Imagem1.Cor1 = System.Drawing.Color.Transparent;
            this.ecTurbo_Imagem1.Cor2 = System.Drawing.Color.Transparent;
            this.ecTurbo_Imagem1.Image = ((System.Drawing.Image)(resources.GetObject("ecTurbo_Imagem1.Image")));
            this.ecTurbo_Imagem1.Location = new System.Drawing.Point(124, 25);
            this.ecTurbo_Imagem1.Name = "ecTurbo_Imagem1";
            this.ecTurbo_Imagem1.Padrao = null;
            this.ecTurbo_Imagem1.Raio1 = 0;
            this.ecTurbo_Imagem1.Raio2 = 0;
            this.ecTurbo_Imagem1.Raio3 = 0;
            this.ecTurbo_Imagem1.Raio4 = 0;
            this.ecTurbo_Imagem1.Size = new System.Drawing.Size(303, 134);
            this.ecTurbo_Imagem1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.ecTurbo_Imagem1.TabIndex = 0;
            this.ecTurbo_Imagem1.TabStop = false;
            this.ecTurbo_Imagem1.Tag = "";
            this.ecTurbo_Imagem1.TamanhoBorda = 0;
            // 
            // FrmConexaoSQLite
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(508, 307);
            this.Controls.Add(this.BtSalvar);
            this.Controls.Add(this.btEscolherArquivo);
            this.Controls.Add(this.TxtBanco);
            this.Controls.Add(this.ecTurbo_Label2);
            this.Controls.Add(this.ecTurbo_Imagem1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmConexaoSQLite";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Configurações de Acesso ao Banco de Dados SQLite";
            ((System.ComponentModel.ISupportInitialize)(this.ecTurbo_Imagem1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ECTurbo.Controles.ECTurbo_Imagem ecTurbo_Imagem1;
        private ECTurbo.Controles.ECTurbo_Label ecTurbo_Label2;
        private ECTurbo.Controles.ECTurbo_TextBox TxtBanco;
        private ECTurbo.Controles.ECTurbo_Botao btEscolherArquivo;
        private ECTurbo.Controles.ECTurbo_Botao BtSalvar;
    }
}