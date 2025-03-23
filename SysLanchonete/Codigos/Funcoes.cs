﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

using SysLanchonete.Properties;

namespace ECTurbo.Codigos
{
    public class Funcoes
    {
        public static ErrorProvider Erro;
        public static bool Resposta;

        public static string BaseApp = AppDomain.CurrentDomain.BaseDirectory;

        public static List<string> ArquivosSelecionados;

        public static void CriarLabel(Control ctr, string texto, string tipo = "erro", Color cor = default, string descricao = "", bool Piscar = true)
        {

            Label lbl = new Label()
            {
                Name = "lbl_" + ctr.Name,
                Text = texto,
                BackColor = Color.Transparent,
                Font = new Font("Century Gothic", 9F, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(ctr.Location.X, ctr.Location.Y + ctr.Height)
            };

            if (cor == default)
            {
                if (tipo == "erro")
                {
                    lbl.ForeColor = Color.Red;
                }
                else if (tipo == "info")
                {
                    lbl.ForeColor = Color.CornflowerBlue;
                }
                else if (tipo == "alerta")
                {
                    lbl.ForeColor = Color.DarkOrange;
                }
            }
            else
            {
                lbl.ForeColor = cor;
            }

            ctr.Parent.Controls.Add(lbl);

            if (Piscar)
            {
                System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
                timer.Interval = 500;
                timer.Tick += (sender, e) =>
                {
                    lbl.Visible = !lbl.Visible;
                };
                timer.Start();
            }

            Label lblAnchor = new Label()
            {
                Name = "lblAnchor_" + ctr.Name,
                AutoSize = false, // Desativa o ajuste automático de tamanho.
                Size = new Size(10, 10), // Define um tamanho mínimo.
                Location = new Point(lbl.Left + lbl.Width - 10, lbl.Top + 4), // Posiciona ao lado.
            };

            ctr.Parent.Controls.Add(lblAnchor);

            if (descricao != "")
            {
                ErrorProvider Erro = new ErrorProvider();
                Erro.BlinkStyle = ErrorBlinkStyle.NeverBlink;
                Erro.SetError(lblAnchor, descricao);
            }
        }

        public static void RemoverLabel(Control ctr)
        {
            if (ctr.Parent == null)
                return;

            ctr.Parent.Controls.Remove(ctr.Parent.Controls["lbl_" + ctr.Name]);
            ctr.Parent.Controls.Remove(ctr.Parent.Controls["lblAnchor_" + ctr.Name]);
        }

        public static void MsgOk(string Msg, string Titulo = "SUCESSO!", int Opacidade = 60, Color Cor = default)
        {
            Formularios.FormMsg frm = new Formularios.FormMsg();

            frm.TituloSucesso.Text = Titulo;
            frm.MsgSucesso.Text = Msg;

            OcultarTabs(frm.MpConteudo, frm.pagSucesso);

            Modal(frm, Opacidade, Cor);
        }

        public static void MsgAlerta(string Msg, string Titulo = "ALERTA!", int Opacidade = 60, Color Cor = default)
        {
            Formularios.FormMsg frm = new Formularios.FormMsg();

            frm.TituloALerta.Text = Titulo;
            frm.MsgAlerta.Text = Msg;

            OcultarTabs(frm.MpConteudo, frm.pagAlertas);

            Modal(frm, Opacidade, Cor);
        }

        public static void MsgErro(string Msg, string Titulo = "ERRO!", int Opacidade = 60, Color Cor = default)
        {
            Formularios.FormMsg frm = new Formularios.FormMsg();

            frm.TituloErro.Text = Titulo;
            frm.MsgErro.Text = Msg;

            int TamanhoTexto;
            using (Graphics g = frm.MsgErro.CreateGraphics())
            {
                SizeF textSize = g.MeasureString(frm.MsgErro.Text, frm.MsgErro.Font, frm.MsgErro.ClientSize.Width);
                TamanhoTexto = (int)Math.Ceiling(textSize.Height);
            }
            if (TamanhoTexto > frm.MsgErro.ClientSize.Height)
                frm.MsgErro.ScrollBars = ScrollBars.Vertical;
            else
                frm.MsgErro.ScrollBars = ScrollBars.None;

            OcultarTabs(frm.MpConteudo, frm.pagErros);

            Modal(frm, Opacidade, Cor);
        }

        public static void OcultarTabs(TabControl tbC, TabPage tbP)
        {

            foreach (TabPage pag in tbC.TabPages)
            {
                tbC.TabPages.Remove(pag);
            }

            tbC.TabPages.Add(tbP);
        }

        public static bool Pergunta(string Msg, string Titulo = "CONFIRMAÇÃO!", int Opacidade = 60, Color Cor = default)
        {
            Formularios.FormMsg frm = new Formularios.FormMsg();

            frm.TituloPergunta.Text = Titulo;
            frm.MsgPergunta.Text = Msg;

            OcultarTabs(frm.MpConteudo, frm.pagConfirmar);

            Modal(frm, Opacidade, Cor);

            return Resposta;
        }

        public static void ModalTelaInteira(Form frmAbrir, int Opacidade = 60, Color Cor = default)
        {
            Form frm = new Form()
            {
                BackColor = Cor == default ? Color.Black : Cor,
                FormBorderStyle = FormBorderStyle.None,
                StartPosition = FormStartPosition.CenterScreen,
                Opacity = Convert.ToDouble(Opacidade) / 100,
                Size = SystemInformation.WorkingArea.Size
            };

            frm.Show();

            frmAbrir.ShowDialog();

            frm.Close();
        }

        public static void Modal(Form frmAbrir, int Opacidade = 60, Color Cor = default)
        {
            Form fAtivo = Form.ActiveForm;

            Form frm = new Form()
            {
                BackColor = Cor == default ? Color.Black : Cor,
                FormBorderStyle = FormBorderStyle.None,
                StartPosition = FormStartPosition.Manual,
                Opacity = Convert.ToDouble(Opacidade) / 100,
                ShowInTaskbar = false
            };

            if (fAtivo != null)
            {
                frm.Size = fAtivo.Size;
                frm.Location = fAtivo.Location;

                if (fAtivo.FormBorderStyle != FormBorderStyle.None)
                {
                    frm.Size = new Size(fAtivo.Width - 16, fAtivo.Height - 9);
                    frm.Location = new Point(fAtivo.Location.X + 8, fAtivo.Location.Y + 1);
                }
            }
            else
            {
                frm.Size = SystemInformation.WorkingArea.Size;
                frm.Location = SystemInformation.WorkingArea.Location;
            }

            frm.Show();

            frmAbrir.StartPosition = FormStartPosition.CenterParent;
            frmAbrir.ShowDialog();

            frm.Close();

            if (fAtivo != null)
            {
                fAtivo.Activate();
            }
        }

        public static string SelecionarArquivo(string Titulo = "Selecione sua imagem",
            string Filtro = "Imagem|*.png;*.jpg;*.jpeg;*.bmp;*.gif")
        {
            using (OpenFileDialog cx = new OpenFileDialog())
            {

                cx.Title = Titulo;

                cx.Filter = Filtro;

                DialogResult r = cx.ShowDialog();

                if (r == DialogResult.OK)
                    return cx.FileName;
                else
                    return "";

            }
        }

        public static int SelecionarVariosArquivos(string Titulo = "Selecione sua imagem",
           string Filtro = "Imagem|*.png;*.jpg;*.jpeg;*.bmp;*.gif")
        {
            using (OpenFileDialog cx = new OpenFileDialog())
            {
                cx.Title = Titulo;
                cx.Filter = Filtro;
                cx.Multiselect = true;

                if (cx.ShowDialog() == DialogResult.OK)
                {
                    ArquivosSelecionados = new List<string>();

                    foreach (string arq in cx.FileNames)
                    {
                        ArquivosSelecionados.Add(arq);
                    }
                }

                return cx.FileNames.Count();
            }
        }

        public static string MiniaturaImagem(PictureBox PictureBox,
                                           string SalvarEm = "",
                                           int Altura = 0,
                                           int Largura = 0,
                                           string Nome = "Miniatura",
                                           string Extensao = "jpg",
                                           bool FundoBranco = false)
        {
            if (SalvarEm == "")
                SalvarEm = BaseApp;

            if (Altura == 0)
            {
                Altura = PictureBox.Height;
                Largura = PictureBox.Width;
            }

            using (Bitmap bmp = new Bitmap(Largura, Altura))
            {
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    if (FundoBranco == true)
                        g.Clear(Color.White);

                    g.DrawImage(PictureBox.Image, 0, 0, Largura, Altura);
                }

                bmp.Save(SalvarEm + "\\" + Nome + "." + Extensao);

                return SalvarEm + "\\" + Nome + "." + Extensao;
            }
        }


        public static bool LimparControles(Control Obj, bool PerguntarAntes = true, string Msg = "")
        {
            if (PerguntarAntes == true)
            {
                if (Msg == "")
                    Msg = "Os dados não salvos serão descartados, confirmar?";

                if (Pergunta(Msg) == false)
                    return false;
            }


            foreach (Control Ctr in Obj.Controls)
            {
                if (Ctr.Controls.Count > 0)
                    LimparControles(Ctr, false);

                RemoverLabel(Ctr);

                if (Ctr.Tag != null)
                {
                    string tag = Ctr.Tag.ToString().ToLower();

                    if (tag.Contains("nao_limpar"))
                        continue;

                    if (tag.Contains("data_atual"))
                    {
                        Ctr.Text = DateTime.Today.ToShortDateString();
                        continue;
                    }

                    if (Ctr is PictureBox pc)
                    {
                        if (tag.Contains("|padrao"))
                        {
                            string ft = PegarTag(pc, "padrao");
                            Image FotoPadrao = (Image)Resources.ResourceManager.GetObject(ft);

                            pc.Image = FotoPadrao;
                        }

                        continue;
                    }

                    if (Ctr is CheckBox ckb)
                    {
                        if (tag.Contains("valor_padrao"))
                            ckb.Checked = true;
                        else
                            ckb.Checked = false;

                        continue;
                    }

                    if (Ctr is RadioButton rbb)
                    {
                        if (tag.Contains("valor_padrao"))
                            rbb.Checked = true;
                        else
                            rbb.Checked = false;

                        continue;
                    }

                }

                if (Ctr is TextBox || Ctr is ComboBox || Ctr is MaskedTextBox)
                    Ctr.Text = string.Empty;

                if (Ctr is ComboBox cb)
                    cb.SelectedIndex = -1;

                if (Ctr is CheckBox ck)
                    ck.Checked = false;

                if (Ctr is RadioButton rb)
                    rb.Checked = false;
            }

            return true;
        }

        public static bool EnviarEmail(string Para,
                                       string Assunto,
                                       string Mensagem,
                                       Control MostrarMsgAguarde = null)
        {

            bool retorno = false;

            using (SmtpClient smtp = new SmtpClient())
            {
                smtp.Host = "smtp.gmail.com";

                smtp.EnableSsl = true;

                smtp.Credentials = new NetworkCredential("edivam.cursos@gmail.com", "qmji ofwp eqtm myio");

                using (MailMessage msg = new MailMessage())
                {
                    msg.From = new MailAddress("edivam.cursos@gmail.com", "ECTurbo");

                    msg.To.Add(new MailAddress(Para));

                    msg.Subject = Assunto;

                    msg.Body = Mensagem;

                    msg.IsBodyHtml = true;

                    if (ArquivosSelecionados != null)
                    {
                        foreach (string arq in ArquivosSelecionados)
                        {
                            Attachment anexo = new Attachment(arq);
                            msg.Attachments.Add(anexo);
                        }
                    }

                    try
                    {
                        if (MostrarMsgAguarde != null)
                        {
                            Funcoes.CriarLabel(MostrarMsgAguarde, "Aguarde enviando E-mail...", "info");
                            MostrarMsgAguarde.Enabled = false;
                            MostrarMsgAguarde.Cursor = Cursors.WaitCursor;
                            Application.DoEvents();
                        }

                        smtp.Send(msg);

                        retorno = true;
                    }
                    catch (System.Exception ex)
                    {
                        MsgErro("Ocorreu um erro ao enviar o E-mail\r\n" + ex.Message);
                    }

                    if (MostrarMsgAguarde != null)
                    {
                        Funcoes.RemoverLabel(MostrarMsgAguarde);
                        MostrarMsgAguarde.Enabled = true;
                        MostrarMsgAguarde.Cursor = Cursors.Default;
                    }
                }
            }

            return retorno;
        }



       

        public static string ChaveAleatoria(int QtdCaracteres = 10)
        {
            string c = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyzÇç0123456789!@#$%&(){}[]";

            int t = c.Length;

            Random r = new Random();

            HashSet<int> Sorteados = new HashSet<int>();

            while (Sorteados.Count < QtdCaracteres)
            {
                Sorteados.Add(r.Next(t));
            }

            string Chave = "";
            foreach (int i in Sorteados)
            {
                Chave += c[i];
            }

            Chave = Embaralhar(Chave);
            Chave = Embaralhar(Chave);

            return Chave;
        }

        private static string Embaralhar(string Texto)
        {
            int t = Texto.Length;

            Random r = new Random();

            HashSet<int> Sorteados = new HashSet<int>();

            while (Sorteados.Count < t)
            {
                Sorteados.Add(r.Next(t));
            }

            string Chave = "";
            foreach (int i in Sorteados)
            {
                Chave += Texto[i];
            }

            return Chave;
        }

        public static string PegarEstadoUF(string UF)
        {
            if (EstadosUFs.TryGetValue(UF, out string Estado))
                return Estado;
            else
                return "";
        }

        public static string PegarUFEstado(string Estado)
        {
            var uf = EstadosUFs.FirstOrDefault(e => e.Value.Equals(Estado, StringComparison.OrdinalIgnoreCase)).Key;

            if (string.IsNullOrEmpty(uf))
                return "";
            else
                return uf;
        }

        public static Dictionary<string, string> EstadosUFs = new Dictionary<string, string>
        {
            { "AC", "Acre" },
            { "AL", "Alagoas" },
            { "AM", "Amazonas" },
            { "AP", "Amapá" },
            { "BA", "Bahia" },
            { "CE", "Ceará" },
            { "DF", "Distrito Federal" },
            { "ES", "Espírito Santo" },
            { "GO", "Goiás" },
            { "MA", "Maranhão" },
            { "MG", "Minas Gerais" },
            { "MS", "Mato Grosso do Sul" },
            { "MT", "Mato Grosso" },
            { "PA", "Pará" },
            { "PB", "Paraíba" },
            { "PE", "Pernambuco" },
            { "PI", "Piauí" },
            { "PR", "Paraná" },
            { "RJ", "Rio de Janeiro" },
            { "RN", "Rio Grande do Norte" },
            { "RO", "Rondônia" },
            { "RR", "Roraima" },
            { "RS", "Rio Grande do Sul" },
            { "SC", "Santa Catarina" },
            { "SE", "Sergipe" },
            { "SP", "São Paulo" },
            { "TO", "Tocantins" }
        };


        public static GraphicsPath CriarPath(Rectangle Base,
                                             int Raio = 1,
                                             int rES = 0,
                                             int rDS = 0,
                                             int rDI = 0,
                                             int rEI = 0)
        {
            if (Raio < 1)
                Raio = 1;

            if (Raio > Base.Width)
                Raio = Base.Width;

            if (Raio > Base.Height)
                Raio = Base.Height;

            if (rES < 1)
                rES = Raio;
            else
            {
                if (rES > Base.Width)
                    rES = Base.Width;

                if (rES > Base.Height)
                    rES = Base.Height;
            }


            if (rDS < 1)
                rDS = Raio;
            else
            {
                if (rDS > Base.Width)
                    rDS = Base.Width;

                if (rDS > Base.Height)
                    rDS = Base.Height;
            }

            if (rDI < 1)
                rDI = Raio;
            else
            {
                if (rDI > Base.Width)
                    rDI = Base.Width;

                if (rDI > Base.Height)
                    rDI = Base.Height;
            }

            if (rEI < 1)
                rEI = Raio;
            else
            {
                if (rEI > Base.Width)
                    rEI = Base.Width;

                if (rEI > Base.Height)
                    rEI = Base.Height;
            }


            Rectangle RectRaio = new Rectangle()
            {
                X = Base.X,
                Y = Base.Y,
                Width = Raio,
                Height = Raio
            };

            GraphicsPath path = new GraphicsPath();

            //Arco superior esquerda
            RectRaio.Width = rES;
            RectRaio.Height = rES;
            path.AddArc(RectRaio, 180, 90);


            //Arco superior Direita
            RectRaio.Width = rDS;
            RectRaio.Height = rDS;
            RectRaio.X = Base.Width + Base.X - RectRaio.Width;
            path.AddArc(RectRaio, 270, 90);


            //Arco inferior direita
            RectRaio.Width = rDI;
            RectRaio.Height = rDI;
            RectRaio.X = Base.Width + Base.X - RectRaio.Width;
            RectRaio.Y = Base.Height + Base.Y - RectRaio.Height;
            path.AddArc(RectRaio, 0, 90);


            //Arco inferior esquerda
            RectRaio.Width = rEI;
            RectRaio.Height = rEI;
            RectRaio.X = Base.X;
            RectRaio.Y = Base.Height + Base.Y - RectRaio.Height;
            path.AddArc(RectRaio, 90, 90);

            path.CloseFigure();

            return path;
        }


        public static string FormatarNumero(object Valor, string Formato = "C2")
        {
            if (Valor == DBNull.Value)
                return 0.ToString(Formato);

            if (Valor == null)
                return 0.ToString(Formato);

            try
            {

                string v = Funcoes.NormalizarNumero(Valor.ToString());

                return Convert.ToDouble(v).ToString(Formato);
            }
            catch (Exception)
            {
                return 0.ToString(Formato);
            }

        }


        public static string FormatarData(object Valor, string Formato = "dd/MM/yyyy")
        {
            if (Valor == DBNull.Value)
                return "";

            if (Valor == null)
                return "";

            try
            {
                string Retorno = Convert.ToDateTime(Valor).ToString(Formato);

                if (Formato.Contains("MMM"))
                {
                    Retorno = Retorno.Replace(".", "");
                }

                CultureInfo cInfo = new CultureInfo("pt-BR");
                Retorno = cInfo.TextInfo.ToTitleCase(Retorno);

                return Retorno;

            }
            catch (Exception)
            {
                return "";
            }

        }

        public static double CvDouble(object Valor)
        {
            if (Valor == DBNull.Value)
                return 0;

            if (Valor == null)
                return 0;

            try
            {
                string v = Funcoes.NormalizarNumero(Valor.ToString());

                return Convert.ToDouble(v);
            }
            catch (Exception)
            {
                return 0;
            }

        }




        public static Color CorTransparente(Color color, int amount)
        {
            int r = color.R + amount;
            int g = color.G + amount;
            int b = color.B + amount;

            // Ensure values are within 0-255 range
            r = Math.Min(255, Math.Max(0, r));
            g = Math.Min(255, Math.Max(0, g));
            b = Math.Min(255, Math.Max(0, b));

            return Color.FromArgb(color.A, r, g, b);
        }

        public static void PegarFotoBanco(PictureBox Obj, object FotoBanco)
        {
            if (FotoBanco == DBNull.Value || FotoBanco == null)
            {
                string ft = PegarTag(Obj, "padrao");
                Image FotoPadrao = (Image)Resources.ResourceManager.GetObject(ft);

                Obj.Image = FotoPadrao;
            }
            else
            {
                byte[] imageData = (byte[])FotoBanco;

                using (MemoryStream ms = new MemoryStream(imageData))
                {
                    Obj.Image = Image.FromStream(ms);
                }
            }
        }

        public static bool CompararImagens(Image img1, Image img2)
        {
            // Verificar se as imagens são nulas
            if (img1 == null || img2 == null)
                return false;

            // Verificar se as imagens têm o mesmo tamanho
            if (img1.Width != img2.Width || img1.Height != img2.Height)
                return false;

            // Converter as imagens para Bitmap para acessar os pixels
            Bitmap bmp1 = new Bitmap(img1);
            Bitmap bmp2 = new Bitmap(img2);

            // Comparar pixel a pixel
            for (int y = 0; y < bmp1.Height; y++)
            {
                for (int x = 0; x < bmp1.Width; x++)
                {
                    if (bmp1.GetPixel(x, y) != bmp2.GetPixel(x, y))
                    {
                        return false; // Os pixels são diferentes
                    }
                }
            }

            // Se todos os pixels são iguais, as imagens são iguais
            return true;
        }


        public static string PegarTag(Control Ctr, string Propriedade)
        {
            if (Ctr.Tag == null)
                return "";

            string[] Configuracoes = Ctr.Tag.ToString().Split('|');

            foreach (string Config in Configuracoes)
            {
                string[] p = Config.Split('=');
                if (p[0] == Propriedade)
                    return p[1];
            }

            return "";
        }

        public static string EscolherFoto(PictureBox CtrFoto)
        {
            string Arquivo = SelecionarArquivo();

            if (Arquivo != string.Empty)
            {
                CtrFoto.Image = Image.FromFile(Arquivo);
            }

            return Arquivo;
        }





        public static bool FotoPadrao(PictureBox CtrFoto)
        {
            string resource = PegarTag(CtrFoto, "padrao");

            Image ImgPadrao = (Image)Resources.ResourceManager.GetObject(resource);

            if (CompararImagens(CtrFoto.Image, ImgPadrao) == false)
            {
                CtrFoto.Image = ImgPadrao;
                return true;
            }
            else
                return false;

        }

        public static void CarregarMeses(ComboBox Cb)
        {
            if (Cb.Items.Count > 0)
                return;

            Cb.Items.Add("Janeiro");
            Cb.Items.Add("Fevereiro");
            Cb.Items.Add("Março");
            Cb.Items.Add("Abril");
            Cb.Items.Add("Maio");
            Cb.Items.Add("Junho");
            Cb.Items.Add("Julho");
            Cb.Items.Add("Agosto");
            Cb.Items.Add("Setembro");
            Cb.Items.Add("Outubro");
            Cb.Items.Add("Novembro");
            Cb.Items.Add("Dezembro");
        }

        public static void CarregarAnos(ComboBox Cb, int Inicio, int Fim)
        {
            Cb.Items.Clear();

            for (int i = Inicio; i <= Fim; i++)
            {
                Cb.Items.Add(i);
            }
        }

        public static bool FotoPadrao(PictureBox CtrFoto, bool PerguntarAntes = true)
        {
            string resource = PegarTag(CtrFoto, "padrao");

            Image ImgPadrao = (Image)Resources.ResourceManager.GetObject(resource);

            if (CompararImagens(CtrFoto.Image, ImgPadrao) == false)
            {
                if (PerguntarAntes == false)
                    return false;

                if (Pergunta("A imagem será removido definitivamente, confirmar?") == false)
                    return false;

                CtrFoto.Image = ImgPadrao;
                return true;
            }
            else
                return false;

        }


        /*
         * 
         * public static void Exportar(string nomeArquivo, string rdlc,
                           string[] DS = null,
                           object[] DSDataTable = null,
                           ReportParameterCollection p = null,
                           string Para = "PDF")
        {
            ReportViewer report = new ReportViewer();

            report.LocalReport.EnableExternalImages = true;

            if (DS != null && DS.Length > 0)
            {
                report.LocalReport.DataSources.Clear();

                for (int i = 0; i < DSDataTable.Length; i++)
                {
                    ReportDataSource rds = new ReportDataSource(DS[i], DSDataTable[i] as DataTable);
                    report.LocalReport.DataSources.Add(rds);
                }
            }

            string CaminhoReports = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Relatorios", rdlc + ".rdlc");
            report.LocalReport.ReportPath = CaminhoReports;

            if (!nomeArquivo.Contains(":"))
            {
                nomeArquivo = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, nomeArquivo);
            }

            if (p != null)
                report.LocalReport.SetParameters(p);

            report.Refresh();
            report.RefreshReport();

            try
            {
                Warning[] warnings;
                string[] streamids;
                string mimeType;
                string encoding;
                string filenameExtension;

                string t = Para.ToUpper() switch
                {
                    "EXCEL" => ".xls",
                    "PNG" => ".png",
                    "JPG" => ".jpg",
                    _ => ".pdf"
                };

                string deviceInfo = Para.ToUpper() switch
                {
                    "PNG" => "<DeviceInfo><OutputFormat>PNG</OutputFormat></DeviceInfo>",
                    "JPG" => "<DeviceInfo><OutputFormat>JPG</OutputFormat></DeviceInfo>",
                    _ => null
                };

                byte[] bytes = report.LocalReport.Render(
                    Para.ToUpper() switch
                    {
                        "PNG" or "JPG" => "IMAGE",
                        "EXCEL" => "EXCEL",
                        _ => "PDF"
                    },
                    deviceInfo,
                    out mimeType,
                    out encoding,
                    out filenameExtension,
                    out streamids,
                    out warnings);

                if (Para.ToUpper() == "PNG" || Para.ToUpper() == "JPG")
                {
                    for (int i = 0; i < streamids.Length; i++)
                    {
                        string filePath = nomeArquivo + $"_Page_{i + 1}" + t;
                        File.WriteAllBytes(filePath, bytes);
                        Console.WriteLine($"Imagem salva: {filePath}");
                    }
                }
                else
                {
                    using (FileStream fs = new FileStream(nomeArquivo + t, FileMode.Create))
                    {
                        fs.Write(bytes, 0, bytes.Length);
                    }

                    Console.WriteLine($"Arquivo salvo: {nomeArquivo + t}");
                }

                // Abrir o arquivo automaticamente
                try
                {
                    ProcessStartInfo psi = new ProcessStartInfo
                    {
                        FileName = nomeArquivo + t,
                        UseShellExecute = true
                    };
                    Process.Start(psi);
                }
                catch (Exception ex)
                {
                    MsgErro(ex.InnerException?.Message ?? ex.Message);
                }
            }
            catch (Exception ex)
            {
                MsgErro(ex.InnerException?.Message ?? ex.Message);
            }
        }

        public static void MesclarDataTable(DataTable dt1, DataTable dt2)
        {

            int a = dt1.Columns.Count;

            for (int x = 0; x < dt2.Columns.Count; x++)
            {
                for (int i = 0; i < dt1.Columns.Count; i++)
                {
                    if (dt1.Columns[i].ColumnName == dt2.Columns[x].ColumnName)
                    {
                        dt2.Columns[x].DataType = dt1.Columns[i].DataType;
                        a--;
                        continue;
                    }
                }

            }

            dt2.Merge(dt1);
        }
        */

        public static string NormalizarNumero(string Valor)
        {
            return Regex.Replace(Valor, "[^0-9,-]", string.Empty);
        }


        public static string Criptografar(string Texto)
        {
            using (SHA256 hash = SHA256.Create())
            {
                byte[] bytes = hash.ComputeHash(Encoding.UTF8.GetBytes(Texto));

                StringBuilder retorno = new StringBuilder();

                foreach (byte b in bytes)
                {
                    retorno.Append(b.ToString("x2"));
                }

                return retorno.ToString();
            }
        }

        public static async Task SlideEntrada(Form frm, string direcao = "esquerda", byte velocidade = 50)
        {
            direcao = direcao.ToLower();

            int finalTop = Screen.PrimaryScreen.Bounds.Top + (Screen.PrimaryScreen.Bounds.Height / 2) - (frm.Height / 2);
            int finalLeft = Screen.PrimaryScreen.Bounds.Left + (Screen.PrimaryScreen.Bounds.Width / 2) - (frm.Width / 2);

            int step = velocidade < 1 ? 1 : velocidade; // Garante que a velocidade seja ao menos 1

            if (direcao == "esquerda")
            {
                frm.Left = 0;
                for (int i = 0; i <= finalLeft; i += step)
                {
                    frm.Left = i;
                    await Task.Delay(1);
                }
            }
            else if (direcao == "direita")
            {
                frm.Left = Screen.PrimaryScreen.Bounds.Width + (frm.Width / 2);
                for (int i = frm.Left; i >= finalLeft; i -= step)
                {
                    frm.Left = i;
                    await Task.Delay(1);
                }
            }
            else if (direcao == "cima")
            {
                frm.Top = Screen.PrimaryScreen.Bounds.Top - frm.Height;
                for (int i = frm.Top; i <= finalTop; i += step)
                {
                    frm.Top = i;
                    await Task.Delay(1);
                }
            }
            else if (direcao == "baixo")
            {
                frm.Top = Screen.PrimaryScreen.Bounds.Height + (frm.Height / 2);
                for (int i = frm.Top; i >= finalTop; i -= step)
                {
                    frm.Top = i;
                    await Task.Delay(1);
                }
            }
        }

        public static async Task SlideSaida(Form frm, string direcao = "direita", byte velocidade = 50)
        {
            direcao = direcao.ToLower();

            int finalTop = Screen.PrimaryScreen.Bounds.Top + (Screen.PrimaryScreen.Bounds.Height / 2) - (frm.Height / 2);
            int finalLeft = Screen.PrimaryScreen.Bounds.Left + (Screen.PrimaryScreen.Bounds.Width / 2) - (frm.Width / 2);

            if (frm.FormBorderStyle == FormBorderStyle.None)
                finalTop = finalTop - 24;

            frm.Left = finalLeft;
            frm.Top = finalTop;

            int step = velocidade < 1 ? 1 : velocidade;

            if (direcao == "esquerda")
            {
                for (int i = finalLeft; i >= Screen.PrimaryScreen.Bounds.Left - frm.Width; i -= step)
                {
                    frm.Left = i;
                    await Task.Delay(1);
                }
            }
            else if (direcao == "direita")
            {
                for (int i = finalLeft; i <= Screen.PrimaryScreen.Bounds.Width + (frm.Width / 2); i += step)
                {
                    frm.Left = i;
                    await Task.Delay(1);
                }
            }
            else if (direcao == "cima")
            {
                for (int i = finalTop; i >= Screen.PrimaryScreen.Bounds.Top - frm.Height; i -= step)
                {
                    frm.Top = i;
                    await Task.Delay(1);
                }
            }
            else if (direcao == "baixo")
            {
                for (int i = finalTop; i <= Screen.PrimaryScreen.Bounds.Height + frm.Height; i += step)
                {
                    frm.Top = i;
                    await Task.Delay(1);
                }
            }

            frm.Dispose();
        }

    }
}
