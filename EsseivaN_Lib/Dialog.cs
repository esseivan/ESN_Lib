using EsseivaN.Controls;
using System.ComponentModel;

namespace EsseivaN.Tools
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

        public struct DialogInputResult
        {
            public string input;
            public DialogResult dialogResult;

            public DialogInputResult(string input)
            {
                this.input = input;
                dialogResult = DialogResult.None;
            }

            public DialogInputResult(string input, DialogResult dialogResult)
            {
                this.input = input;
                this.dialogResult = dialogResult;
            }
        }

        private Dialog()
        {
        }

        // Dialog

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

        /// <summary>
        /// Set custom button
        /// </summary>
        public static void Dialog_SetButton(Button button, string text)
        {
            DialogForm.SetButton(button, text);
        }

        /// <summary>
        /// Remove custom button
        /// </summary>
        public static void Dialog_RemoveButton(Button button)
        {
            DialogForm.RemoveButton(button);
        }

        private static DialogResult ShowDialog_Core(string message, string title, ButtonType btn1, ButtonType btn2, ButtonType btn3)
        {
            return DialogForm.ShowDialog(message, title, btn1, btn2, btn3);
        }

        // DialogInput

        public static DialogInputResult ShowDialogInput(string message)
        {
            return ShowDialogInput_core(message, "Information", "", ButtonType.OK, ButtonType.Cancel, ButtonType.None);
        }

        public static DialogInputResult ShowDialogInput(string message, string title)
        {
            return ShowDialogInput_core(message, title, "", ButtonType.OK, ButtonType.Cancel, ButtonType.None);
        }

        public static DialogInputResult ShowDialogInput(string message, string title, string defaultInput)
        {
            return ShowDialogInput_core(message, title, defaultInput, ButtonType.OK, ButtonType.Cancel, ButtonType.None);
        }

        public static DialogInputResult ShowDialogInput(string message, string title, string defaultInput, ButtonType btn1)
        {
            return ShowDialogInput_core(message, title, defaultInput, btn1, ButtonType.Cancel, ButtonType.None);
        }

        public static DialogInputResult ShowDialogInput(string message, string title, string defaultInput, ButtonType btn1, ButtonType btn2)
        {
            return ShowDialogInput_core(message, title, defaultInput, btn1, btn2, ButtonType.None);
        }

        public static DialogInputResult ShowDialogInput(string message, string title, string defaultInput, ButtonType btn1, ButtonType btn2, ButtonType btn3)
        {
            return ShowDialogInput_core(message, title, defaultInput, btn1, btn2, btn3);
        }

        /// <summary>
        /// Set custom button
        /// </summary>
        public static void DialogInput_SetButton(Button button, string text)
        {
            DialogInputForm.SetButton(button, text);
        }

        /// <summary>
        /// Remove custom button
        /// </summary>
        public static void DialogInput_RemoveButton(Button button)
        {
            DialogInputForm.RemoveButton(button);
        }

        private static DialogInputResult ShowDialogInput_core(string message, string title, string defaultInput, ButtonType btn1, ButtonType btn2, ButtonType btn3)
        {
            return DialogInputForm.ShowDialog(message, title, defaultInput, btn1, btn2, btn3);
        }
    }
}