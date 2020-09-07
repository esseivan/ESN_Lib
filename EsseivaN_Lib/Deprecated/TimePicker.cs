using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EsseivaN.Deprecated
{
    public partial class TimePicker : UserControl
    {
        CircleControlPlacer<RoundButton> placerInner = new CircleControlPlacer<RoundButton>()
        {
            AngleOffset = 0,
            Auto = true,
            Radius = 80,
        };

        CircleControlPlacer<RoundButton> placerOuter = new CircleControlPlacer<RoundButton>()
        {
            AngleOffset = 0,
            Auto = true,
            Radius = 120,
        };

        public enum Mode
        {
            Hours_0_to_12,
            Minutes_0_to_60,
            Hours_0_to_24,
        }

        public TimePicker()
        {
            InitializeComponent();

            placerInner.Parent = placerOuter.Parent = mainPanel;
            placerInner.PositionOffset = placerOuter.PositionOffset = new Point(mainPanel.Width / 2, mainPanel.Height / 2);
            SetMode(Mode.Hours_0_to_24);
        }

        public void SetMode(Mode mode)
        {
            int step = 1;
            int offset = 0;
            int inner = 0;
            int outer = 12;
            bool reverse = false;
            switch (mode)
            {
                case Mode.Hours_0_to_12:
                    break;
                case Mode.Hours_0_to_24:
                    inner = 12;
                    break;
                case Mode.Minutes_0_to_60:
                    step = 5;
                    break;
                default:
                    return;
            }
            placerInner.Populate(inner);
            placerOuter.Populate(outer);
            placerInner.Place();
            placerOuter.Place();

            List<RoundButton> list = reverse ? placerInner.Controls : placerOuter.Controls;

            for (int i = 0; i < list.Count; i++)
            {
                list[i].Text = offset.ToString("00");
                offset += step;
            }
            list = !reverse ? placerInner.Controls : placerOuter.Controls;

            for (int i = 0; i < list.Count; i++)
            {
                list[i].Text = offset.ToString("00");
                offset += step;
            }
        }
    }
}
