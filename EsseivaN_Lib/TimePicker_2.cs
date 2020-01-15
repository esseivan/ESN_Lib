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
        public int ItemRadius { get; set; } = 20;
        public int ItemCount { get; set; } = 12;

        List<ItemPair> Pairs = new List<ItemPair>();

        private const double Deg2Rad = Math.PI / 180;

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
                Point mid = new Point(Width / 2, Height / 2);
                foreach (var item in Pairs)
                {
                    Point pt = item.location;
                    g.FillEllipse(pen.Brush, pt.X + mid.X, pt.Y + mid.Y, ItemRadius, ItemRadius);
                }
                pen.Dispose();
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
