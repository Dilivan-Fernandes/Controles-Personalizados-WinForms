using IF_VeloxFramework.Codigos;
using System.Drawing.Design;
using static System.Net.Mime.MediaTypeNames;

namespace IF_VeloxFramework.Controles
{
    public class IF_Velox_Texto:TextBox
    {
            /*As cores do controle vem de uma classe chamada de Config,
             mais você pode substituir a chamada das cor que vem da classe
             por propriedades que você mesmo pode criar aqui, ou, você pode
             adicionar as cores dentro dos métodos.
             */
             
        protected override void OnEnter(EventArgs e)
        {
            base.OnEnter(e);
            BackColor = Config.CorEntrada;
             /*A cor do BackColor, vem de uma classe chamada Config,
             mais você pode substituir a chamada da cor que vem da classe pela
             cor de sua preferência aqui mesmo no método, ou,
             você pode criar propriedades de cores do seu gosto.             
             */
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
