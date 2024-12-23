using IF_VeloxFramework.Codigos;
using Microsoft.Win32;
using System.ComponentModel;
using System.Drawing.Drawing2D;

namespace IF_VeloxFramework.Controles
{
    public class IF_Velox_Botao :Button
    {
        private int borderSize = 1;
        private int borderRadius = 30;
       

        [Category("_IF_Velox")]
        public int BorderSize
        {
            get
            {
                return borderSize;
            }
            set
            {
                borderSize = value;
                this.Invalidate();
            }
        }
        [Category("_IF_Velox")]
        [DisplayName("Largura da borda")]
        public int BorderRadius
        {
            get
            {
                return borderRadius;
            }
            set
            {
                if (value <= this.Height)
                    borderRadius = value;
                else borderRadius = this.Height;
                this.Invalidate();
            }
        }
        private Color borderColor = Color.FromArgb(157, 32, 83);
         
        [Category("_IF_Velox")]
        [DisplayName("Cor da borda")]       
        public Color CorDaBorda
        {
            get
            {
                return borderColor;
            }
            set
            {
                borderColor = value;
                this.Invalidate();
            }

        }

        private Color vBackColor = Color.AliceBlue;
        [DisplayName("BackColor")]
       [Category("_IF_Velox")]
        public Color BackgroundColo
        {
            get { return this.BackColor; }
            set { this.BackColor = value; this.Invalidate(); }
        }


        private Color vCorDoTexto = Color.FromArgb(157, 32, 83);

        [Category("_IF_Velox")]
        [DisplayName("Cor do texto")]
        public Color CorDoTexto
        {
            get { return this.vCorDoTexto; }
            set { this.vCorDoTexto = value; this.Invalidate(); }
        }

        public IF_Velox_Botao()
        {
            this.FlatStyle = FlatStyle.Flat;
            this.FlatAppearance.BorderSize = 0;
            this.Size = new Size(150, 40);
            this.BackColor = BackgroundColo;
            this.ForeColor = CorDoTexto;
            this.Resize += new EventHandler(Button_Resize);
            this.borderColor = CorDaBorda;
        }

        //Métodos
        private GraphicsPath GetFigurePath(RectangleF rect, float radius)
        {
            GraphicsPath path = new GraphicsPath();
            path.StartFigure();
            path.AddArc(rect.X, rect.Y, radius, radius, 180, 90);
            path.AddArc(rect.Width - radius, rect.Y, radius, radius, 270, 90);
            path.AddArc(rect.Width - radius, rect.Height - radius, radius, radius, 0, 90);

            path.AddArc(rect.X, rect.Height - radius, radius, radius, 90, 90);
            path.CloseFigure();

            return path;
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            base.OnPaint(pevent);

            pevent.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            RectangleF rectSurface = new RectangleF(0, 0, this.Width, this.Height);
            RectangleF rectBorder = new RectangleF(1, 1, this.Width - 0.8F, this.Height - 1);

            if (borderRadius > 2) // Arredondamento
            {
                using (GraphicsPath pathSurface = GetFigurePath(rectSurface, borderRadius))
                using (GraphicsPath pathBorder = GetFigurePath(rectBorder, borderRadius - 1F))
                using (Pen penSurface = new Pen(this.Parent.BackColor, 2))
                using (Pen penBorder = new Pen(borderColor, borderSize))
                {
                    penBorder.Alignment = PenAlignment.Inset;
                    this.Region = new Region(pathSurface);

                    pevent.Graphics.DrawPath(penSurface, pathSurface);

                    if (borderSize >= 1)
                        pevent.Graphics.DrawPath(penBorder, pathBorder);

                }


            }
            else // Normalizar botão
            {
                this.Region = new Region(rectSurface);

                if (borderSize >= 1)
                {
                    using (Pen penBorder = new Pen(borderColor, borderSize))
                    {
                        penBorder.Alignment = PenAlignment.Inset;
                        pevent.Graphics.DrawRectangle(penBorder, 0, 0, this.Width - 1, this.Height - 1);


                    }
                }


            }
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            this.Parent.BackColorChanged += new EventHandler(ContainerBackColorChanged);
        }

        private void ContainerBackColorChanged(object? sender, EventArgs e)
        {
            if (this.DesignMode)
                this.Invalidate();
        }

        private void Button_Resize(object? sender, EventArgs e)
        {
            if (borderRadius > this.Height)
                borderRadius = this.Height;
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            Cursor = Cursors.Hand;
            ForeColor = CorDoTexto;
            Font = Config.EstiloDeFonteDoBotao;
            BackColor = BackgroundColo;
        }

        protected override void OnMouseHover(EventArgs e)
        {
            base.OnMouseHover(e);

           
            this.BackColor = Config.Cor_EntradaDeFocoBotoes;
            
        }
        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            this.BackColor = Config.Cor_SaidadaDeFocoBotoes;
           
        }
    }
}
