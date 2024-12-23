using IF_VeloxFramework.Codigos;
using System.ComponentModel;

namespace IF_VeloxFramework.Controles
{
    public class IF_Velox_MaskedTextBox : MaskedTextBox
    {
        

        private bool vSalvarMascara = false;

        [DisplayName("_Salvar Mascara")]        
        [Category("_IF_VeloxFramework")]
        public bool SalvarMascara
        {
            get { return vSalvarMascara ; }
            set { vSalvarMascara = value; }
        }


        protected override void OnEnter(EventArgs e)
        {
            base.OnEnter(e);
            BackColor = Config.CorEntrada;
        }

        protected override void OnLeave(EventArgs e)
        {
            BackColor =  Config.CorSaida;
        }
        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            ForeColor = Config.CorDeFontes;
            Font = Config.FontPadrao;

            if (SalvarMascara == true)
            {
                TextMaskFormat = MaskFormat.IncludeLiterals;
            }
            else
            {
                TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
            }
            
        }
        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            if (e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("{TAB}");
                e.SuppressKeyPress = true;
            }
        }
    }
}
