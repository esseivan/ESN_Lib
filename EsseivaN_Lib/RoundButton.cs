using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EsseivaN
{
    public partial class RoundButton : Button
    {
        private bool keepRound = false;
        [Browsable(true), Description("Keep a circle form"), Category("Layout"),]
        public bool KeepRound
        {
            get
            {
                return keepRound;
            }
            set
            {
                AutoSize = true;
                keepRound = value;
                UpdateSize();
            }
        }

        public RoundButton()
        {
            this.BackColor = Color.LightBlue;
            this.Width = this.Height = 50;
            this.ForeColor = Color.Black;
        }

        public void UpdateSize()
        {
            if (AutoSize)
            {
                this.Size = GetPreferredSize(this.Size);
            }
            Invalidate();
        }

        public override Size GetPreferredSize(Size proposedSize)
        {
            Size size = proposedSize;
            if (AutoSize)
                size = base.GetPreferredSize(proposedSize);

            if (!KeepRound)
                return size;

            if (size.Width >= size.Height)
                size.Height = size.Width;
            else
                size.Width = size.Height;
            return size;
        }

        public Size GetPreferredSize()
        {
            return GetPreferredSize(new Size(Width, Height));
        }

        public Point GetMiddle(Size size)
        {
            return new Point(
                x: size.Width / 2,
                y: size.Height / 2);
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            Graphics g = pe.Graphics;
            g.Clear(Parent.BackColor);

            Pen backPen = new Pen(BackColor);
            Size size = GetPreferredSize();

            g.FillEllipse(backPen.Brush, 0, 0, size.Width, size.Height);

            Pen forePen = new Pen(ForeColor);
            StringFormat format = new StringFormat
            {
                LineAlignment = StringAlignment.Center,
                Alignment = StringAlignment.Center
            };

            g.DrawString(Text, Font, forePen.Brush, GetMiddle(size), format);

            backPen.Dispose();
        }
    }
}
