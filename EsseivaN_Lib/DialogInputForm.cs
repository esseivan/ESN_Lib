﻿/*
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
    public partial class DialogInputForm : Form
    {
        private static Dialog.DialogResult Result;
        private static Dialog.ButtonType Btn1, Btn2, Btn3;
        private static string Btn1_t, Btn2_t, Btn3_t;
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
                    Btn1_t = text;
                    break;
                case 2:
                    Btn2_t = text;
                    break;
                case 3:
                    Btn3_t = text;
                    break;
                case 255:
                    Btn1_t = Btn2_t = Btn3_t = text;
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
                    Btn1_t = string.Empty;
                    break;
                case 2:
                    Btn2_t = string.Empty;
                    break;
                case 3:
                    Btn3_t = string.Empty;
                    break;
                case 255:
                    Btn1_t = Btn2_t = Btn3_t = string.Empty;
                    break;
                default:
                    break;
            }
        }

        public static DialogInput.DialogInputResult ShowDialog(string Message, string Title, string DefaultInput, Dialog.ButtonType Button1, Dialog.ButtonType Button2, Dialog.ButtonType Button3)
        {
            Btn1 = Button1;
            Btn2 = Button2;
            Btn3 = Button3;

            DialogInputForm dialogForm = new DialogInputForm();
            dialogForm.txtInput.Text = DefaultInput;

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
                    dialogForm.button1.Text = Btn1_t;
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
                    dialogForm.button2.Text = Btn2_t;
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
                    dialogForm.button3.Text = Btn3_t;
                }
                else
                {
                    dialogForm.button3.Text = Btn3.ToString();
                }
            }

            Result = Dialog.DialogResult.None;

            dialogForm.Text = Title;
            dialogForm.label_text.Text = Message;

            dialogForm.ShowDialog();
            return new DialogInput.DialogInputResult(dialogForm.Input, Result);
        }

        #endregion

        #region Main

        // Execute on load
        private void DialogInputForm_Load(object sender, EventArgs e)
        {
            int width = Screen.PrimaryScreen.WorkingArea.Width;
            int height = Screen.PrimaryScreen.WorkingArea.Height;

            MaximumSize = label_text.MaximumSize = new Size((int)(width * MaximumSizeRatio), (int)(height * MaximumSizeRatio));

            int posX = (width / 2 - Size.Width / 2);
            int posY = (height / 2 - Size.Height / 2);

            Location = new Point(posX, posY);
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

        private void DialogInputForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Input = txtInput.Text;
        }
    }
}
