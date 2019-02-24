/*
 * File     : DialogInputForm.cs
 * Author   : Esseiva Nicolas
 * Date     : 18.11.2017
 * 
 * Allows the user to enter an input and
 * use the result in the software
 */

using EsseivaN.Tools;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace EsseivaN.Controls
{
    public partial class DialogForm : Form
    {
        private static Dialog.DialogResult Result;
        private static Dialog.ButtonType Btn1, Btn2, Btn3;
        private static string custom1_t, custom2_t, custom3_t;

        const double MaximumSizeRatio = 2d / 3d;

        #region initialization

        /// <summary>
        /// Dialog input window
        /// </summary>
        public DialogForm()
        {
            InitializeComponent();
        }

        public static void SetButton(int button, string text)
        {
            switch (button)
            {
                case 1:
                    custom1_t = text;
                    break;
                case 2:
                    custom2_t = text;
                    break;
                case 3:
                    custom3_t = text;
                    break;
                case 255:
                    custom1_t = custom2_t = custom3_t = text;
                    break;
                default:
                    break;
            }
        }

        public static void RemoveButton(int button)
        {
            switch (button)
            {
                case 1:
                    custom1_t = string.Empty;
                    break;
                case 2:
                    custom2_t = string.Empty;
                    break;
                case 3:
                    custom3_t = string.Empty;
                    break;
                case 255:
                    custom1_t = custom2_t = custom3_t = string.Empty;
                    break;
                default:
                    break;
            }
        }

        public static Dialog.DialogResult ShowDialog(string Message, string Title, Dialog.ButtonType Button1, Dialog.ButtonType Button2, Dialog.ButtonType Button3, Dialog.DialogIcon Icon)
        {
            Btn1 = Button1;
            Btn2 = Button2;
            Btn3 = Button3;

            DialogForm dialogForm = new DialogForm();

            // Button 1
            if (Btn1 == Dialog.ButtonType.None)
            {
                dialogForm.button1.Visible = false;
                dialogForm.button1.Enabled = false;
            }
            else
            {
                dialogForm.CancelButton = dialogForm.button1;
                if (Btn1 >= Dialog.ButtonType.Custom1)
                {
                    dialogForm.button1.Text = GetTextForCustom(Btn1);
                }
                else
                {
                    dialogForm.button1.Text = Btn1.ToString();
                }
            }

            // Button 2
            if (Btn2 == Dialog.ButtonType.None)
            {
                dialogForm.button2.Visible = false;
                dialogForm.button2.Enabled = false;
            }
            else
            {
                dialogForm.CancelButton = dialogForm.button2;
                if (Btn2 >= Dialog.ButtonType.Custom1)
                {
                    dialogForm.button2.Text = GetTextForCustom(Btn2);
                }
                else
                {
                    dialogForm.button2.Text = Btn2.ToString();
                }
            }

            // Button 3
            if (Btn3 == Dialog.ButtonType.None)
            {
                dialogForm.button3.Visible = false;
                dialogForm.button3.Enabled = false;
            }
            else
            {
                dialogForm.CancelButton = dialogForm.button3;
                if (Btn3 >= Dialog.ButtonType.Custom1)
                {
                    dialogForm.button3.Text = GetTextForCustom(Btn3);
                }
                else
                {
                    dialogForm.button3.Text = Btn3.ToString();
                }
            }

            Result = Dialog.DialogResult.None;

            dialogForm.Text = Title;
            dialogForm.label_text.Text = Message;

            dialogForm.pictureBox1.Visible = true;

            Icon t_icon = null;
            switch (Icon)
            {
                case Dialog.DialogIcon.None:
                    dialogForm.pictureBox1.Visible = false;
                    break;
                case Dialog.DialogIcon.Application:
                    t_icon = SystemIcons.Application;
                    break;
                case Dialog.DialogIcon.Asterisk:
                    t_icon = SystemIcons.Asterisk;
                    break;
                case Dialog.DialogIcon.Error:
                    t_icon = SystemIcons.Error;
                    break;
                case Dialog.DialogIcon.Hand:
                    t_icon = SystemIcons.Hand;
                    break;
                case Dialog.DialogIcon.Exclamation:
                    t_icon = SystemIcons.Exclamation;
                    break;
                case Dialog.DialogIcon.Shield:
                    t_icon = SystemIcons.Shield;
                    break;
                case Dialog.DialogIcon.Question:
                    t_icon = SystemIcons.Question;
                    break;
                case Dialog.DialogIcon.Warning:
                    t_icon = SystemIcons.Warning;
                    break;
                case Dialog.DialogIcon.Information:
                    t_icon = SystemIcons.Information;
                    break;
                case Dialog.DialogIcon.WinLogo:
                    t_icon = SystemIcons.WinLogo;
                    break;
                default:
                    break;
            }
            if (t_icon != null)
                dialogForm.pictureBox1.Image = t_icon.ToBitmap();

            dialogForm.ShowDialog();
            return Result;
        }

        private static string GetTextForCustom(Dialog.ButtonType buttonType)
        {
            switch (buttonType)
            {
                case Dialog.ButtonType.Custom1:
                    return custom1_t;
                case Dialog.ButtonType.Custom2:
                    return custom2_t;
                case Dialog.ButtonType.Custom3:
                    return custom3_t;
                default:
                    return string.Empty;
            }
        }

        private void DialogForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape && CancelButton == null)
            {
                Close();
            }
        }

        #endregion

        #region Main

        // Execute on load
        private void DialogInputForm_Load(object sender, EventArgs e)
        {
            button1.Focus();
        }

        #endregion

        private void Button1_Click(object sender, EventArgs e)
        {
            Result = (Dialog.DialogResult)Btn1;
            Close();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Result = (Dialog.DialogResult)Btn2;
            Close();
        }

        private void Button3_CLick(object sender, EventArgs e)
        {
            Result = (Dialog.DialogResult)Btn3;
            Close();
        }
    }
}
