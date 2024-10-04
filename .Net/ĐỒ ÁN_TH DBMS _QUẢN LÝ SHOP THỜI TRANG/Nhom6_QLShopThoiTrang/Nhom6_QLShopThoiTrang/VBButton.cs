using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.ComponentModel;

namespace Nhom6_QLShopThoiTrang
{
    class VBButton: Button
    {
        //Fields
        private int borderSize = 0;
        private int borderRadius = 20;
        private Color borderColor = Color.PaleGreen;

        //properties
        //BorderSize
        [Category("Custom Props")]
        public int BorderSize
        {
            get { return borderSize; }
            set
            {
                borderSize = value;
                this.Invalidate();
            }
        }

        //BorderRadius
        [Category("Custom Props")]
        public int BorderRadius
        {
            get { return borderRadius; }
            set
            {
                borderRadius = value;
                this.Invalidate(); 
            }
        }

        //BorderColor
        [Category("Custom Props")]
        public Color BorderColor
        {
            get { return borderColor; }
            set
            {
                borderColor = value;
                this.Invalidate();
            }
        }

        //BackgroundColor
        [Category("Custom Props")]
        public Color BackgroundColor
        {
            get { return this.BackColor; }
            set
            {
                this.BackColor = value;
            }
        }

        //TextColor
        [Category("Custom Props")]
        public Color TextColor
        {
            get { return this.ForeColor; }
            set
            {
                this.ForeColor = value;
            }
        }

        //constructor: build

        //VBButton
        public VBButton()
        {
            this.FlatStyle = FlatStyle.Flat;
            this.FlatAppearance.BorderSize = 0;
            this.Size = new Size(150, 40);
            this.BackColor = Color.MediumSlateBlue;
            this.ForeColor = Color.Black;
            this.Resize += new EventHandler(Button_Resize);
        }

        //Method: GetFigurePath
        private GraphicsPath GetFigurePath(Rectangle rect, float radius)
        {
            GraphicsPath path = new GraphicsPath();
            float curveSize = radius * 2F;

            path.StartFigure();
            path.AddArc(rect.X, rect.Y, curveSize, curveSize, 180, 90);
            path.AddArc(rect.Right - curveSize, rect.Y, curveSize, curveSize, 270, 90);
            path.AddArc(rect.Right - curveSize, rect.Bottom - curveSize, curveSize, curveSize, 0, 90);
            path.AddArc(rect.X, rect.Bottom - curveSize, curveSize, curveSize, 90, 90);
            path.CloseFigure();
            return path;
        }

        //Methods: OnPaint
        protected override void OnPaint(PaintEventArgs pevent)
        {
            base.OnPaint(pevent);
            pevent.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            Rectangle rectSurface = this.ClientRectangle;
            Rectangle rectBoder = Rectangle.Inflate(rectSurface, -borderSize, -borderSize);
            int smoothSize = 2;
            if (borderSize > 0)
                smoothSize = borderSize;

            if(borderRadius > 2) //Rounded button: nút tròn
            {
                using (GraphicsPath pathSurface = GetFigurePath(rectSurface, borderRadius))
                using (GraphicsPath pathBoder = GetFigurePath(rectBoder, borderRadius - borderSize))
                using (Pen penSurface = new Pen(this.Parent.BackColor, smoothSize))
                using (Pen penBorder = new Pen(borderColor, borderSize))
                {
                    //Button surface
                    this.Region = new Region(pathSurface);

                    //draw surface border for HD result
                    pevent.Graphics.DrawPath(penSurface, pathSurface);

                    //Button border
                    if (borderSize >= 1)
                        //Draw control border
                        pevent.Graphics.DrawPath(penBorder, pathBoder);
                }    
            } 
            else //normal button
            {
                //button surface
                this.Region = new Region(rectSurface);
                //button border
                if(borderSize >= 1)
                {
                    using (Pen penBoder = new Pen(borderColor, borderSize))
                    {
                        penBoder.Alignment = PenAlignment.Inset;
                        pevent.Graphics.DrawRectangle(penBoder, 0, 0, this.Width - 1, this.Height - 1);
                    }    
                }  
            }    
        }

        //Methods: OnHandleCreated
        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            this.Parent.BackColorChanged += new EventHandler(Container_BackColorChanged);
        }
        private void Container_BackColorChanged(object sender, EventArgs e)
        {
            this.Invalidate();
        }
        //Button_Resize
        private void Button_Resize(object sender, EventArgs e)
        {
            if (BorderRadius > this.Height)
                borderRadius = this.Height;
        }

    }
}
