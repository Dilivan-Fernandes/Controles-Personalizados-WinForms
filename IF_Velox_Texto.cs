using IF_VeloxFramework.Codigos;
using System.Drawing.Design;
using static System.Net.Mime.MediaTypeNames;

namespace IF_VeloxFramework.Controles
{
    public class IF_Velox_Texto:TextBox

    {
        protected override void OnEnter(EventArgs e)
        {
            base.OnEnter(e);
            BackColor = Config.CorEntrada;
        }

        protected override void OnLeave(EventArgs e)
        {
            BackColor = Config.CorSaida;
        }
        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            ForeColor = Config.CorDeFontes;
            Font = Config.FontPadrao;
            BackColor = Config.CorPrimaria;
            


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
        protected override void OnTextAlignChanged(EventArgs e)
        {
            base.OnTextAlignChanged(e);
            if (Text == string.Empty)
            {
                Funcoes.RemoverLabel(this);
            }
        }
    }
}
