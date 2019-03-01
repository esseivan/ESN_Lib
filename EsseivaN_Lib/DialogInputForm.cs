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
    internal partial class DialogInputForm : Form
    {
        private static Message_Config.DialogResult Result;
        private static Message_Config.ButtonType Btn1, Btn2, Btn3;
        private static string custom1_t, custom2_t, custom3_t;
        public string Input { get; set; }

        const double MaximumSizeRatio = 2d / 3d;

        #region initialization

        /// <summary>
        /// Dialog input window
        /// </summary>
        public DialogInputForm()
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

        public static MessageInput.DialogInputResult ShowDialog(string Message, string Title, string DefaultInput, Message_Config.ButtonType Button1, Message_Config.ButtonType Button2, Message_Config.ButtonType Button3, Message_Config.DialogIcon Icon)
        {
            Btn1 = Button1;
            Btn2 = Button2;
            Btn3 = Button3;

            DialogInputForm dialogForm = new DialogInputForm();
            //dialogForm.txtInput.Text = DefaultInput;

            // Button 1
            if (Btn1 == Message_Config.ButtonType.None)
            {
                dialogForm.button1.Visible = false;
                dialogForm.button1.Enabled = false;
            }
            else
            {
                dialogForm.CancelButton = dialogForm.button1;
                if (Btn1 >= Message_Config.ButtonType.Custom1)
                {
                    dialogForm.button1.Text = GetTextForCustom(Btn1);
                }
                else
                {
                    dialogForm.button1.Text = Btn1.ToString();
                }
            }

            // Button 2
            if (Btn2 == Message_Config.ButtonType.None)
            {
                dialogForm.button2.Visible = false;
                dialogForm.button2.Enabled = false;
            }
            else
            {
                dialogForm.CancelButton = dialogForm.button2;
                if (Btn2 >= Message_Config.ButtonType.Custom1)
                {
                    dialogForm.button2.Text = GetTextForCustom(Btn2);
                }
                else
                {
                    dialogForm.button2.Text = Btn2.ToString();
                }
            }

            // Button 3
            if (Btn3 == Message_Config.ButtonType.None)
            {
                dialogForm.button3.Visible = false;
                dialogForm.button3.Enabled = false;
            }
            else
            {
                dialogForm.CancelButton = dialogForm.button3;
                if (Btn3 >= Message_Config.ButtonType.Custom1)
                {
                    dialogForm.button3.Text = GetTextForCustom(Btn3);
                }
                else
                {
                    dialogForm.button3.Text = Btn3.ToString();
                }
            }

            Result = Message_Config.DialogResult.None;

            dialogForm.Text = Title;
            dialogForm.label_text.Text = Message;

            dialogForm.pictureBox1.Visible = true;

            Icon t_icon = null;
            switch (Icon)
            {
                case Message_Config.DialogIcon.None:
                    dialogForm.pictureBox1.Visible = false;
                    break;
                case Message_Config.DialogIcon.Application:
                    t_icon = SystemIcons.Application;
                    break;
                case Message_Config.DialogIcon.Asterisk:
                    t_icon = SystemIcons.Asterisk;
                    break;
                case Message_Config.DialogIcon.Error:
                    t_icon = SystemIcons.Error;
                    break;
                case Message_Config.DialogIcon.Hand:
                    t_icon = SystemIcons.Hand;
                    break;
                case Message_Config.DialogIcon.Exclamation:
                    t_icon = SystemIcons.Exclamation;
                    break;
                case Message_Config.DialogIcon.Shield:
                    t_icon = SystemIcons.Shield;
                    break;
                case Message_Config.DialogIcon.Question:
                    t_icon = SystemIcons.Question;
                    break;
                case Message_Config.DialogIcon.Warning:
                    t_icon = SystemIcons.Warning;
                    break;
                case Message_Config.DialogIcon.Information:
                    t_icon = SystemIcons.Information;
                    break;
                case Message_Config.DialogIcon.WinLogo:
                    t_icon = SystemIcons.WinLogo;
                    break;
                default:
                    break;
            }
            if (t_icon != null)
                dialogForm.pictureBox1.Image = t_icon.ToBitmap();

            dialogForm.ShowDialog();
            return new MessageInput.DialogInputResult(dialogForm.Input, Result);
        }

        private static string GetTextForCustom(Message_Config.ButtonType buttonType)
        {
            switch (buttonType)
            {
                case Message_Config.ButtonType.Custom1:
                    return custom1_t;
                case Message_Config.ButtonType.Custom2:
                    return custom2_t;
                case Message_Config.ButtonType.Custom3:
                    return custom3_t;
                default:
                    return string.Empty;
            }
        }

        #endregion

        #region Main

        // Execute on load
        private void DialogInputForm_Load(object sender, EventArgs e)
        {
        }

        #endregion

        private void DialogInputForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape && CancelButton == null)
            {
                Close();
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Result = (Message_Config.DialogResult)Btn1;
            Close();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Result = (Message_Config.DialogResult)Btn2;
            Close();
        }

        private void Button3_CLick(object sender, EventArgs e)
        {
            Result = (Message_Config.DialogResult)Btn3;
            Close();
        }

        private void DialogInputForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Input = richTextBox1.Text;
        }
    }
}
