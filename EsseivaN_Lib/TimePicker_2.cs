using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EsseivaN
{
    public partial class TimePicker_2 : UserControl
    {
        public Color ItemColor { get; set; } = Color.LightBlue;
        public int ItemRadius { get; set; } = 10;
        public int ItemCount { get; set; } = 12;

        public List<ItemPair> Pairs = new List<ItemPair>();

        private const double Deg2Rad = Math.PI / 180;

        private int lastIndex = -1;

        public TimePicker_2()
        {
            InitializeComponent();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (Pairs.Count != 0)
            {
                Graphics g = e.Graphics;
                Pen pen = new Pen(ItemColor);
                Pen textPen = new Pen(ForeColor);
                Point mid = new Point(Width / 2, Height / 2);
                StringFormat format = new StringFormat()
                {
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Center,
                };

                foreach (var item in Pairs)
                {
                    Point pt = item.location;
                    g.FillEllipse(pen.Brush, pt.X + mid.X, pt.Y + mid.Y, ItemRadius * 2, ItemRadius* 2);
                    g.DrawString(item.text, this.Font, textPen.Brush, pt.X + mid.X + ItemRadius, pt.Y + mid.Y + ItemRadius, format);
                }
                pen.Dispose();
                textPen.Dispose();
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            Invalidate();
        }

        public void Clear()
        {
            Pairs.Clear();
            Invalidate();
        }

        public void Populate(int count, int radius, float angleOffset = 0)
        {
            float AngleStep = 360f / count;
            float angle = angleOffset;
            for (int i = 0; i < count; i++)
            {
                // Calculer x et y
                int x = (int)Math.Round(radius * (float)Math.Sin(angle * Deg2Rad));
                int y = -(int)Math.Round(radius * (float)Math.Cos(angle * Deg2Rad));
                Pairs.Add(new ItemPair("?", new Point(x, y)));
                angle += AngleStep;
            }
            Invalidate();
        }

        public void SetTexts(string[] texts, int startAt = 0)
        {
            if (startAt < 0)
                return;

            for (int i = 0; i < Pairs.Count && i < texts.Length; i++)
            {
                var t = Pairs[i + startAt];
                t.text = texts[i];
                Pairs[i + startAt] = t;
            }
            Invalidate();
        }

        public void SetTextNext(string text, int index = -1)
        {
            int thisIndex;
            if(index == -1)
            {
                lastIndex++;
                thisIndex = lastIndex;
            }
            else
            {
                thisIndex = index;
            }

            if (thisIndex >= Pairs.Count)
                thisIndex = 0;

            var t = Pairs[thisIndex];
            t.text = text;
            Pairs[thisIndex] = t;
            lastIndex = thisIndex;
        }

        public struct ItemPair
        {
            public string text;
            public Point location;

            public ItemPair(string text, Point location)
            {
                this.text = text;
                this.location = location;
            }
        }

    }
}
