using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using EsseivaN.Controls;

namespace EsseivaN.Controls
{
    public class Dialog : Component
    {

        /// <summary>
        /// Type of buttons
        /// </summary>
        public enum ButtonType
        {
            None = 0,
            OK = 1,
            Skip = 2,
            Ignore = 3,
            Continue = 4,
            Accept = 5,
            Previous = 6,
            Next = 7,
            Cancel = 8,
            Abort = 9,

            Custom1 = 253,
            Custom2 = 254,
            Custom3 = 255,
        }

        /// <summary>
        /// Result of the dialog
        /// </summary>
        public enum DialogResult
        {
            None = 0,
            OK = 1,
            Skip = 2,
            Ignore = 3,
            Continue = 4,
            Accept = 5,
            Previous = 6,
            Next = 7,
            Cancel = 8,
            Abort = 9,

            Custom1 = 253,
            Custom2 = 254,
            Custom3 = 255,
        }

        public enum Button
        {
            Button1,
            Button2,
            Button3,
            All = 255
        }

        private Dialog()
        {

        }

        public static DialogResult ShowDialog(string Message)
        {
            return ShowDialog_Core(Message, "Information", ButtonType.OK, ButtonType.None, ButtonType.None);
        }

        public static DialogResult ShowDialog(string Message, string Title)
        {
            return ShowDialog_Core(Message, Title, ButtonType.OK, ButtonType.None, ButtonType.None);
        }

        public static DialogResult ShowDialog(string Message, string Title, ButtonType Button1)
        {
            return ShowDialog_Core(Message, Title, Button1, ButtonType.None, ButtonType.None);
        }

        public static DialogResult ShowDialog(string Message, string Title, ButtonType Button1, ButtonType Button2)
        {
            return ShowDialog_Core(Message, Title, Button1, Button2, ButtonType.None);
        }

        public static DialogResult ShowDialog(string Message, string Title, ButtonType Button1, ButtonType Button2, ButtonType Button3)
        {
            return ShowDialog_Core(Message, Title, Button1, Button2, Button3);
        }

        public static void SetButton(Button button, string text)
        {
            DialogForm.SetButton(button, text);
        }

        public static void RemoveButton(Button button)
        {
            DialogForm.RemoveButton(button);
        }

        private static DialogResult ShowDialog_Core(string message, string title, ButtonType btn1, ButtonType btn2, ButtonType btn3)
        {
            return DialogForm.ShowDialog(message, title, btn1, btn2, btn3);
        }
    }
}
