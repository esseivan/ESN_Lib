using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EsseivaN.Deprecated
{
    public class CircleControlPlacer<T> where T : Control
    {

        /// <summary>
        /// Angle offset for the first element. In degree
        /// </summary>
        public float AngleOffset { get; set; } = 0;
        /// <summary>
        /// Indice offset for the first element (and so on)
        /// </summary>
        public float IndiceOffset { get; set; } = 0;
        /// <summary>
        /// Step of angle in manual mode. In degree
        /// </summary>
        public float AngleStep { get; set; } = 30;
        /// <summary>
        /// Radius of the circle
        /// </summary>
        public float Radius { get; set; } = 50;
        /// <summary>
        /// Inform the recommended width to the user
        /// </summary>
        public int RecommendedWidth { get; } = 0;
        /// <summary>
        /// Offset for the position of the controls (recommended center of parent)
        /// </summary>
        public Point PositionOffset { get; set; } = default;
        /// <summary>
        /// Parent container
        /// </summary>
        public Control Parent { get; set; } = default;
        /// <summary>
        /// Whether to automatically decide the angle required to complete a full circle
        /// </summary>
        public bool Auto { get; set; } = false;

        private const double Deg2Rad = Math.PI / 180;

        // List of linked controls
        public List<T> Controls = new List<T>();

        public CircleControlPlacer() { }

        public CircleControlPlacer(Control parent)
        {
            this.Parent = parent;
        }

        /// <summary>
        /// Generate the specified amount of controls
        /// </summary>
        public void Populate(int count)
        {
            if (Parent == null)
                throw new NullReferenceException("Parent cannot be null");

            foreach (T item in Controls)
            {
                item.Dispose();
            }

            Controls.Clear();

            for (int i = 0; i < count; i++)
            {
                T control = (T)Activator.CreateInstance(typeof(T));
                Controls.Add(control);
                Parent.Controls.Add(control);
            }
        }

        // Deleted functions
        //public void AddChilds(Control parent, Type filterType = default)
        //{
        //    foreach (Control child in parent.Controls)
        //    {
        //        if (filterType != default)
        //        {
        //            if (!filterType.IsInstanceOfType(child))
        //                continue;
        //        }

        //        Controls.Add(child);
        //    }
        //}

        //public void AddControl(Control control)
        //{
        //    Controls.Add(control);
        //}

        //public void Clear()
        //{
        //    Controls.Clear();
        //}

        //public void RemoveControl(Control control)
        //{
        //    Controls.Remove(control);
        //}

        //public void RemoveControl(int index)
        //{
        //    Controls.RemoveAt(index);
        //}

        //public Control GetControl(int index)
        //{
        //    return Controls.ElementAtOrDefault(index);
        //}

        private Size DivideSize(Size size, double divider)
        {
            return new Size((int)Math.Round(size.Width / divider),
                            (int)Math.Round(size.Height / divider));
        }

        /// <summary>
        /// Get the locations for each controls, in the same order as Controls
        /// </summary>
        public List<Point> GetPoints()
        {
            // Get count
            int count = Controls.Count;
            // Get angle
            if (Auto)
            {
                AngleStep = 360f / count;
            }

            List<Point> points = new List<Point>();

            float angle = AngleOffset + IndiceOffset * AngleStep;
            for (int i = 0; i < count; i++)
            {
                // Calculer x et y
                int x = (int)Math.Round(Radius * (float)Math.Sin(angle * Deg2Rad)) + PositionOffset.X;
                int y = -(int)Math.Round(Radius * (float)Math.Cos(angle * Deg2Rad)) + PositionOffset.Y;
                points.Add(new Point(x, y));
                angle += AngleStep;
            }

            return points;
        }

        public void Place()
        {
            List<Point> points = GetPoints();

            for (int i = 0; i < Controls.Count; i++)
            {
                Control control = Controls[i];
                control.Location = points[i] - DivideSize(control.Size, 2);
            }
        }

        public void Place(List<Point> points)
        {
            for (int i = 0; i < Controls.Count; i++)
            {
                Controls[i].Location = points[i];
            }
        }
    }
}
