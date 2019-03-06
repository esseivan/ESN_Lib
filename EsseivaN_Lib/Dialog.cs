using EsseivaN.Controls;

namespace EsseivaN.Tools
{
    public class Dialog
    {
        private Dialog()
        {
        }

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
            Retry = 10,

            Custom1 = 253,
            Custom2 = 254,
            Custom3 = 255,
        }

        /// <summary>
        /// Type of icon
        /// </summary>
        public enum DialogIcon
        {
            None = 0,
            Application,
            Asterisk,
            Error,
            Hand,
            Exclamation,
            Shield,
            Question,
            Warning,
            Information,
            WinLogo
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
            Retry = 10,

            Custom1 = 253,
            Custom2 = 254,
            Custom3 = 255,
        }

        /// <summary>
        /// Show dialog with config class
        /// </summary>
        public static DialogInputResult ShowDialog(DialogConfig Config)
        {
            // Set custom buttons
            DialogInputForm.SetButton(1, Config.CustomButton1Text);
            DialogInputForm.SetButton(2, Config.CustomButton2Text);
            DialogInputForm.SetButton(3, Config.CustomButton3Text);

            // Show dialog
            return DialogInputForm.ShowDialog(Config.Message,
                Config.Title,
                Config.DefaultInput,
                Config.Input,
                Config.Button1,
                Config.Button2,
                Config.Button3,
                Config.Icon);
        }

        /// <summary>
        /// Show dialog with config parameters
        /// </summary>
        public static DialogInputResult ShowDialog(string Message,
            string Title = "Information",
            string DefaultInput = "",
            bool Input = false,
            ButtonType Btn1 = ButtonType.OK,
            ButtonType Btn2 = ButtonType.None,
            ButtonType Btn3 = ButtonType.None,
            DialogIcon Icon = DialogIcon.None,
            string CB1_Text = "Custom1",
            string CB2_Text = "Custom2",
            string CB3_Text = "Custom3")
        {
            // Set custom buttons
            DialogInputForm.SetButton(1, CB1_Text);
            DialogInputForm.SetButton(2, CB2_Text);
            DialogInputForm.SetButton(3, CB3_Text);

            // Show dialog
            return DialogInputForm.ShowDialog(Message,
                Title,
                DefaultInput,
                Input,
                Btn1,
                Btn2,
                Btn3,
                Icon);
        }

        /// <summary>
        /// Config of the dialog
        /// </summary>
        public class DialogConfig
        {
            public string Message { get; set; } = string.Empty;
            public string Title { get; set; } = "Information";
            public string DefaultInput { get; set; } = "";
            public bool Input { get; set; } = false;
            public ButtonType Button1 { get; set; } = ButtonType.OK;
            public ButtonType Button2 { get; set; } = ButtonType.None;
            public ButtonType Button3 { get; set; } = ButtonType.None;
            public DialogIcon Icon { get; set; } = DialogIcon.None;
            public string CustomButton1Text { get; set; } = "Custom1";
            public string CustomButton2Text { get; set; } = "Custom2";
            public string CustomButton3Text { get; set; } = "Custom3";

            public DialogConfig()
            {

            }

            public DialogConfig(string Message)
            {
                this.Message = Message;
            }

            public DialogConfig(string Message, string Title)
            {
                this.Message = Message;
                this.Title = Title;
            }

            public DialogConfig(string Message, string Title, bool Input)
            {
                this.Message = Message;
                this.Title = Title;
                this.Input = false;
            }
        }

        /// <summary>
        /// Result of the call of ShowDialogInput
        /// </summary>
        public struct DialogInputResult
        {
            /// <summary>
            /// The text set by the user
            /// </summary>
            public string text { get; set; }
            /// <summary>
            /// The button clicked
            /// </summary>
            public DialogResult DialogResult { get; set; }

            public DialogInputResult(string input)
            {
                this.text = input;
                DialogResult = DialogResult.None;
            }

            public DialogInputResult(string input, DialogResult dialogResult)
            {
                this.text = input;
                this.DialogResult = dialogResult;
            }
        }
    }
}