using System.Collections.Generic;

namespace IF_VeloxFramework.Codigos
{
    public class Funcoes
    {
        
        //Método que cria a label embaixo das textbox de forma automaticá
        public static void CriarLabel(Control ctr, string texto,string tipo="Erro", Color cor= default)
        {
            Label lbl = new Label()
            {
                Name = "lbl" + ctr.Name,
                Text = texto,              
                BackColor= Color.Transparent,
                Font = new Font("Century Gothic", 9F , FontStyle.Bold),
                AutoSize = true,
                Location = new Point(ctr.Location.X , ctr.Location.Y + ctr.Height),
            };

            if (cor== default)
            {
                if (tipo == "Erro")
                {
                    lbl.ForeColor = Color.OrangeRed;
                }
                else if (tipo == "Info")
                {
                    lbl.ForeColor = Color.SkyBlue;
                }
                else if (tipo == "Alerta")
                {
                    lbl.ForeColor = Color.OrangeRed;
                }
            }
            else
            {
                lbl.ForeColor = cor;
            }
           
            ctr.Parent.Controls.Add(lbl);
        }

        //Método que remove a label
        public static void RemoverLabel(Control ctr)
        {
            if (ctr.Parent == null)
            {
                return;
            }
            ctr.Parent.Controls.Remove(ctr.Parent.Controls["lbl" + ctr.Name]);
        }


       
    }
}
