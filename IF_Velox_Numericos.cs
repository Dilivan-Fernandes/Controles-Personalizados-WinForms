
using IF_VeloxFramework.Codigos;
using System.ComponentModel;

namespace IF_VeloxFramework.Controles
{
    public class IF_Velox_Numericos : IF_Velox_Texto
    {
        //Propriedades

        //Propriedade para configurar a quantidade de casas decimais
        private int vCasasDecimais =2;

        [DisplayName("_Casas Decimais")]
        [Description("Define a quantidade de casas decimais no controle.")]
        [Category("_IF_Velox")]
        public int CasasDecimais
        {
            get { return vCasasDecimais; }
            set { vCasasDecimais  = value; }
        }

        // Propriedade para formatar como moeda
        private bool vMoeda = false;

        [DisplayName("_Formatar como Moedda")]
        [Description("Define a propriedade de entrada como falores monetário.")]
        [Category("_IF_Velox")]
        public bool Moeda
        {
            get { return vMoeda; }
            set { vMoeda = value; }
        }

        // 

        private bool vForcarFormatacao = false;

        [DisplayName("_Forçar Formatação")]
        [Description("xxx")]
        [Category("_IF_Velox")]
        public bool ForcarFormatacao
        {
            get { return vForcarFormatacao; }
            set { vForcarFormatacao = value; }
        }

        //================================================================

        private bool vPermitirZerado = false;

        [DisplayName("_Permitir Valores Zerados")]
        [Description("xxx.")]
        [Category("_IF_Velox")]
        public bool PermitirZerado
        {
            get { return vPermitirZerado; }
            set { vPermitirZerado = value; }
        }

        //==================================================================


        private bool vPermitirNegativo = false;

        [DisplayName("_Permitir Valores Negativos")]
        [Description("xxx.")]
        [Category("_IF_Velox")]
        public bool PermitirNegativo
        {
            get { return vPermitirNegativo; }
            set { vPermitirNegativo = value; }
        }

        //====================================================================
        private bool vColorirNegativo = true;

        [DisplayName("_Colorir de vermelho Valores Negativos")]
        [Description("xxx.")]
        [Category("_IF_Velox")]
        public bool ColorirNegativo
        {
            get { return vColorirNegativo; }
            set { vColorirNegativo = value; }
        }
        //=====================================================================

       
        
       
    
       

        //Bloquear a entrada de caracteres não numéricos
        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            base.OnKeyPress(e);

            if (e.KeyChar == (char)Keys.Back)// Faz com que se possa apagar com a tecla backspace
            {
                return;
            }


            if (PermitirNegativo == true && e.KeyChar == '-')
            {
                Text = "-" + Text;
                goto Continuar;
            }

            if (e.KeyChar == '+')
            {
                Text = Text.Replace("-", "");
                goto Continuar;
            }

                // Se o valor digitado for caracter
            if (char.IsDigit(e.KeyChar) == false && e.KeyChar != ',')// Libera só números e vírgulas
            {
                // Bloqueia a entrada do valor
                goto Continuar;
               
            }

            if (e.KeyChar ==',')
            {
                if (Text == string.Empty)
                {
                    Text = "0,";
                    goto Continuar;
                }

                if (Text.Contains(','))
                {
                    goto Continuar;
                }
            }

            if (e.KeyChar ==',')
            {
                if (SelectionStart < Text.Length - CasasDecimais)
                {
                    e.Handled = true;
                    return;
                }
            }

            if (Text.Contains(","))
            {
                string[] q = Text.Split(",");

                if (q[1].Length == CasasDecimais)
                {
                    e.Handled = true;
                    return;
                }
            }
            


            return;

        Continuar:
            e.Handled = true;
            SelectionStart = Text.Length;
        }

        protected override void OnValidating(CancelEventArgs e)
        {
            base.OnValidating(e);

            Funcoes.RemoverLabel(this);


            if (Text == string.Empty)
            {
                return;
            }


           

            // Formatar como moeda
            // Letra N significa Number = números
            // Letra C significa currency = Monetário / Moeda


            string v = Text.Replace("R", "")
                           .Replace("$", "")
                           .Replace(" ", "")
                           .Replace(".", "");


            try
            {
                double valor = Convert.ToDouble(v);

                if (Moeda == true)
                    Text = valor.ToString("C" + CasasDecimais);
                else
                    Text = valor.ToString("N" + CasasDecimais);


                if (ForcarFormatacao == false && Moeda == false)
                {
                    Text = Text.TrimEnd('0');
                    Text = Text.TrimEnd(',');
                }

                // 00000,001
                if (valor.ToString("N" + CasasDecimais) == (0).ToString("N" + CasasDecimais) && PermitirZerado == false)
                {
                    goto Continuar;
                }
            }
            catch (Exception)
            {

                goto Continuar;
            }


            return;
            Continuar:
            Funcoes.CriarLabel(this, "Valor inválido");
            e.Cancel = true;
            SelectionStart = Text.Length;

        }

        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);

            if (ColorirNegativo ==true)
            {
                if (Text.Contains('-'))
                    ForeColor = Color.Red;
                else
                    ForeColor = Color.FromArgb(64, 64, 64);
            }
            
        }
    }
}
