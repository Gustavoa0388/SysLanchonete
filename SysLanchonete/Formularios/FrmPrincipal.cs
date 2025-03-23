using System.Windows.Forms;

using ECTurbo.Codigos;

using ECTurbo_CRUD;


namespace SysLanchonete.Formularios
{
    public partial class FrmPrincipal : Form
    {
        public FrmPrincipal()
        {
            InitializeComponent();
        }

        private void FrmPrincipal_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.Shift && e.Alt && e.KeyCode == Keys.B)
            {
                Funcoes.Modal(new FrmConexaoSQLite());
                Application.Restart();
            }
        }

    }
}
