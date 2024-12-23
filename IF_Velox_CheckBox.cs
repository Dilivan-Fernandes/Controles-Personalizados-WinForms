using System.ComponentModel;
using System.Drawing.Drawing2D;

namespace IF_VeloxFramework.Controles
{
    public class IF_Velox_CheckBox : CheckBox
    {

       

        private Color onBackColor = Color.MediumSlateBlue;
        private Color ontoggleColor = Color.WhiteSmoke;
        private Color offBackColor = Color.Gray;
        private Color offToggleColor = Color.Gainsboro;
        private bool solidStyle = true;

        [DisplayName("_Configurações de cores")]
        [Description("Selecione a cor principal do componente")]
        [Category("_IF_Velox")]
        public Color OnBackColor
        {
            get
            {
                return onBackColor;
            }
            set
            {
                onBackColor = value;
                this.Invalidate();
            }
        }
        public Color OntoggleColor 
        {
            get
            {
                return ontoggleColor;
            }
            set
            {
                ontoggleColor = value;
                this.Invalidate();
            }
        }
        public Color OffBackColor
        {   
            get
            { 
               return offBackColor; 
            
            }
            set
            {
                offBackColor = value;
                this.Invalidate();
            } 
        }
        public Color OffToggleColor 
        {
            get 
            {
               return offToggleColor;
            }

            set
            {
                offToggleColor = value;
                this.Invalidate();

            }
        }

        [DefaultValue(true)]
        public bool SolidStyle 
        {
            get
            {
                return solidStyle;
            }

            set
            {
                solidStyle = value;
                this.Invalidate();

            }
                
        }

        public IF_Velox_CheckBox()
        {
            this.MinimumSize = new Size(20, 12);
        }

        private GraphicsPath GetFigurePath()
        {
            int arcSize = this.Height - 1;

            Rectangle leftArc = new Rectangle(0, 0, arcSize, arcSize);
            Rectangle righArc = new Rectangle(this.Width - arcSize - 2, 0, arcSize, arcSize);

            GraphicsPath path = new GraphicsPath();
            path.StartFigure();
            path.AddArc(leftArc, 90, 180);
            path.AddArc(righArc, 270, 180);
            path.CloseFigure();

            return path;
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            int toggleSize = this.Height - 5;
            pevent.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            pevent.Graphics.Clear(this.Parent.BackColor);

            if (this.Checked) // ON
            {
                if (SolidStyle)
                    pevent.Graphics.FillPath(new SolidBrush(onBackColor), GetFigurePath());
                else pevent.Graphics.DrawPath(new Pen(onBackColor, 2) ,GetFigurePath());
                pevent.Graphics.FillEllipse(new SolidBrush(ontoggleColor),
                    new Rectangle(this.Width - this.Height + 1, 2, toggleSize, toggleSize));
            }
            else
            {
                if(SolidStyle)
                pevent.Graphics.FillPath(new SolidBrush(offBackColor), GetFigurePath());
                else pevent.Graphics.DrawPath(new Pen(offBackColor, 2), GetFigurePath());
                pevent.Graphics.FillEllipse(new SolidBrush(offToggleColor),
                    new Rectangle(2, 2, toggleSize, toggleSize));
            }
        }
    }
}
