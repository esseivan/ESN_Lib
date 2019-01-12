using System;
using System.Collections.Generic;
using Model = System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EsseivaN.Controls
{
    public class TextboxWatermark : TextBox
    {
        private string watermarkText = "Type here...";
        private bool watermarkActive = false;
        private Color watermarkColor = SystemColors.GrayText;
        private Color textColor = SystemColors.ControlText;

        [Model.Browsable(true), Model.Description("Watermark Text to be displayed"), Model.Category("Watermark"),]
        public string WatermarkText
        { get { return watermarkText; } set { watermarkText = value; } }

        [Model.Browsable(true), Model.Description("Watermark Text to be displayed"), Model.Category("Watermark")]
        public Color WatermarkColor
        { get { return watermarkColor; } set { watermarkColor = value; } }

        [Model.Browsable(true), Model.Description("Normal Text color"), Model.Category("Appearance")]
        public Color TextColor
        { get { return textColor; } set { textColor = value; } }

        public TextboxWatermark()
        {
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();

            watermarkActive = base.Text == string.Empty;

            if (watermarkActive)
            {
                ForeColor = WatermarkColor;
                base.Text = WatermarkText;
            }
            else
                ForeColor = textColor;
        }

        [Model.Browsable(true), Model.Description("Normal Text color"), Model.Category("Appearance")]
        public override string Text
        {
            get
            {
                return watermarkActive ? string.Empty : base.Text;
            }
            set
            {
                watermarkActive = (value == string.Empty);
                ForeColor = watermarkActive ? WatermarkColor : textColor;
                base.Text = value ?? WatermarkText;
            }
        }

        [Model.Browsable(false)]
        public override int TextLength
        {
            get
            {
                return watermarkActive ? 0 : base.TextLength;
            }
        }

        public override void ResetText()
        {
            watermarkActive = true;
            ForeColor = WatermarkColor;
            base.Text = WatermarkText;
        }

        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);

            //if (WatermarkActive)
            //{
            //    WatermarkActive = false;
            //    ForeColor = defaultColor;
            //    base.Text = string.Empty;
            //}
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (watermarkActive)
            {
                watermarkActive = false;
                ForeColor = textColor;
                base.Text = string.Empty;
            }

            base.OnMouseDown(e);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            e.Handled = true;

            if (watermarkActive)
            {
                watermarkActive = false;
                ForeColor = textColor;
                base.Text = string.Empty;
            }

            base.OnKeyDown(e);

            Application.DoEvents();

            if (base.Text == string.Empty)
            {
                watermarkActive = true;
                ForeColor = WatermarkColor;
                base.Text = WatermarkText;
            }
        }

        protected override void OnLostFocus(EventArgs e)
        {
            base.OnLostFocus(e);

            if (base.Text == string.Empty)
            {
                watermarkActive = true;
                ForeColor = WatermarkColor;
                base.Text = WatermarkText;
            }
        }
    }
}
