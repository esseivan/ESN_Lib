using EsseivaN.Controls;

namespace EsseivaN.Tools
{
    /// <summary>
    /// Display a message to the user. Buttons can be customized
    /// </summary>
    public class MessageDialog : Message_Config
    {
        private MessageDialog()
        {
        }

        /// <summary>
        /// Show dialog with config class
        /// </summary>
        public static DialogResult ShowDialog(DialogConfig Config)
        {
            // Set custom buttons
            DialogForm.SetButton(1, Config.CustomButton1Text);
            DialogForm.SetButton(2, Config.CustomButton2Text);
            DialogForm.SetButton(3, Config.CustomButton3Text);

            // Show dialog
            return DialogForm.ShowDialog(Config.Message, Config.Title, Config.Button1, Config.Button2, Config.Button3, Config.Icon);
        }

        /// <summary>
        /// Show dialog with config parameters
        /// </summary>
        public static DialogResult ShowDialog(string Message, string Title = "Information",
            ButtonType Btn1 = ButtonType.OK, ButtonType Btn2 = ButtonType.None, ButtonType Btn3 = ButtonType.None, DialogIcon Icon = DialogIcon.None,
            string CB1_Text = "Custom1", string CB2_Text = "Custom2", string CB3_Text = "Custom3")
        {
            // Set custom buttons
            DialogForm.SetButton(1, CB1_Text);
            DialogForm.SetButton(2, CB2_Text);
            DialogForm.SetButton(3, CB3_Text);

            // Show dialog
            return DialogForm.ShowDialog(Message, Title, Btn1, Btn2, Btn3, Icon);
        }
    }
}
