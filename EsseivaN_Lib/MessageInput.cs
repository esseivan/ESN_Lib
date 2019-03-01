using EsseivaN.Controls;

namespace EsseivaN.Tools
{
    /// <summary>
    /// Display a message to the user and ask a text input. Buttons can be customized
    /// </summary>
    public class MessageInput : Message_Config
    {
        private MessageInput()
        {
        }

        /// <summary>
        /// Show dialog input with config class
        /// </summary>
        public static DialogInputResult ShowDialog(DialogConfig Config)
        {
            // Set custom buttons
            DialogInputForm.SetButton(1, Config.CustomButton1Text);
            DialogInputForm.SetButton(2, Config.CustomButton2Text);
            DialogInputForm.SetButton(3, Config.CustomButton3Text);

            // Show dialog
            return DialogInputForm.ShowDialog(Config.Message, Config.Title, Config.DefaultInput, Config.Button1, Config.Button2, Config.Button3, Config.Icon);
        }

        /// <summary>
        /// Show dialog input with config parameters
        /// </summary>
        public static DialogInputResult ShowDialog(string Message, string Title = "Information", string DefaultInput = "",
            ButtonType Btn1 = ButtonType.OK, ButtonType Btn2 = ButtonType.None, ButtonType Btn3 = ButtonType.None, DialogIcon Icon = DialogIcon.None,
            string CB1_Text = "Custom1", string CB2_Text = "Custom2", string CB3_Text = "Custom3")
        {
            // Set custom buttons
            DialogForm.SetButton(1, CB1_Text);
            DialogForm.SetButton(2, CB2_Text);
            DialogForm.SetButton(3, CB3_Text);

            // Show dialog
            return DialogInputForm.ShowDialog(Message, Title, DefaultInput, Btn1, Btn2, Btn3, Icon);
        }

        /// <summary>
        /// Result of the call of ShowDialogInput
        /// </summary>
        public struct DialogInputResult
        {
            /// <summary>
            /// The text set by the user
            /// </summary>
            public string text;
            /// <summary>
            /// The button clicked
            /// </summary>
            public DialogResult dialogResult;

            public DialogInputResult(string input)
            {
                this.text = input;
                dialogResult = DialogResult.None;
            }

            public DialogInputResult(string input, DialogResult dialogResult)
            {
                this.text = input;
                this.dialogResult = dialogResult;
            }
        }

    }
}
