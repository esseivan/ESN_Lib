using EsseivaN.Controls;

namespace EsseivaN.Tools
{
    public class DialogInput : Dialog
    {
        internal DialogInput()
        {

        }

        // DialogInput
        public static DialogInputResult ShowDialog(DialogConfig Config)
        {
            // Set custom buttons
            DialogInputForm.SetButton(1, Config.CustomButton1Text);
            DialogInputForm.SetButton(2, Config.CustomButton2Text);
            DialogInputForm.SetButton(3, Config.CustomButton3Text);

            // Show dialog
            return DialogInputForm.ShowDialog(Config.Message, Config.Title, Config.DefaultInput, Config.Button1, Config.Button2, Config.Button3);
        }

        // Dialog
        public static DialogInputResult ShowDialog(string Message, string Title = "Information", string DefaultInput = "",
            ButtonType Btn1 = ButtonType.OK, ButtonType Btn2 = ButtonType.None, ButtonType Btn3 = ButtonType.None,
            string CB1_Text = "Custom1", string CB2_Text = "Custom2", string CB3_Text = "Custom3")
        {
            // Set custom buttons
            DialogForm.SetButton(1, CB1_Text);
            DialogForm.SetButton(2, CB2_Text);
            DialogForm.SetButton(3, CB3_Text);

            // Show dialog
            return DialogInputForm.ShowDialog(Message, Title, DefaultInput, Btn1, Btn2, Btn3);
        }
        /// <summary>
        /// Result of the call of ShowDialogInput
        /// </summary>
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

    }
}
