﻿using System;

namespace EsseivaN.Tools
{
    public class Message_Config
    {
        internal Message_Config()
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
        /// Config of the dialog
        /// </summary>
        public class DialogConfig
        {
            public string Message { get; set; } = string.Empty;
            public string Title { get; set; } = "Information";
            public string DefaultInput { get; set; } = "";
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
        }
    }
}