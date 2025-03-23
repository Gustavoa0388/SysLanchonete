using ECTurbo.Codigos;

using SysLanchonete.Properties;

using ECTurbo_CRUD;
using System.Windows.Forms;
using System;

namespace SysLanchonete.Formularios
{
    public partial class FrmConexaoSQLite : Form
    {
        public FrmConexaoSQLite()
        {
            InitializeComponent();
        }

        private void btEscolherArquivo_Click(object sender, EventArgs e)
        {
            string Arquivo = Funcoes.SelecionarArquivo("Selecione seu banco de dados", "SQLite|*.db;");

            if (Arquivo != string.Empty)
                TxtBanco.Text = Arquivo;
        }

        private void BtSalvar_Click(object sender, EventArgs e)
        {
            if (TxtBanco.Text == string.Empty)
            {
                Funcoes.CriarLabel(TxtBanco, "Selecione o arquivo do SQLite");
                return;
            }

            Settings.Default.SQLite_Banco = TxtBanco.Text;
            Settings.Default.Save();

            string Resposta = SQLITE.TestarConexao();

            if (Resposta == string.Empty)
            {
                Funcoes.MsgOk("Conexão realizada com sucesso");
                Application.Restart();
            }
            else
                Funcoes.MsgErro(Resposta);

        }
    }
}
