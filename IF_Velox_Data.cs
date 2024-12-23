using IF_VeloxFramework.Codigos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IF_VeloxFramework.Controles
{
    public class IF_Velox_Data : IF_Velox_Texto
    {
        public IF_Velox_Data()
        {
            TextAlign = HorizontalAlignment.Center;
        }
        [DisplayName("_Iniciar com a Data Atual")]
        [Description("Se ativado o controle já será iniciado com a data atual.")]
        [Category("_IF_Veliox")]
        public bool DataAtual { get; set; }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();

            ForeColor = Config.CorDeFontes;
            Font = Config.FontPadrao;

            if (DataAtual ==true)
            {
                Text = DateTime.Now.ToShortDateString();
            }
        }
        protected override void OnValidating(CancelEventArgs e)
        {
            base.OnValidating(e);

            Funcoes.RemoverLabel(this);

            if (Text == string.Empty)
            {
                return;
            }

            try
            {
                Text = Convert.ToDateTime(Text).ToShortDateString();
            }
            catch (Exception)
            {

                Funcoes.CriarLabel(this,"Data Inválida!");
                e.Cancel = true;
                return;
            }

            string[] q = Text.Split('/'); // Divide a data em 3 pardes usando a barra como separador

            if (Convert.ToInt32(q[2]) < 1582)
            {
                Funcoes.CriarLabel(this, "Ano Inválida!");
                SelectionStart = Text.Length;
                e.Cancel = true;
            }
        }

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            base.OnKeyPress(e);
            // Liberar a tecla Backspace
            if (e.KeyChar == (char)Keys.Back)
            {
                return;
            }

            int t = Text.Length; // variável que conta a quantidade de caracteres 

            //Bloqueia quando a data estiver completa

            if (t == 10) goto Continuar;

            string[] q = Text.Split('/');

            // Melhorias ao digitar a barra de forma manual

            if (e.KeyChar.ToString()== "/")
            {
                if (t == 0) goto Continuar;

                if(Text == "0") goto Continuar;

                if (t ==1)
                {
                    Text = "0" + Text + "/";

                }
                else if (t == 2)
                {
                    Text  += "/";
                }else if(t == 4)
                {
                    Text = q[0] + "/0" + q[1] + "/";
                }


                SelectionStart = Text.Length;
            }
            // Bloquear a entrada de caracteres
            if (char.IsDigit(e.KeyChar) == false) goto Continuar;

            // Colocar as barras automaticamente
            if (t == 2 || t ==5)
            {
                Text += "/";
            }
            
            SelectionStart = Text.Length;

            return;
            Continuar:
            e.Handled = true;
        }
    }
}
