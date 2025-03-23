using System;
using System.Windows.Forms;

using ECTurbo.Codigos;

using ECTurbo_CRUD;

namespace SysLanchonete
{
    static class Program
    {
        /// <summary>
        /// Ponto de entrada principal para o aplicativo.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);


            //if (SQLITE.TestarConexao() == string.Empty)
                Application.Run(new Formularios.FrmPrincipal());
            //else
                //Funcoes.Modal(new Formularios.FrmConexaoSQLite());
        }
    }
}
