using EsseivaN.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EsseivaN.Tools
{
    public class MessageDialog : Dialog
    {
        internal MessageDialog()
        {

        }

        // Dialog
        public static DialogResult ShowDialog(DialogConfig Config)
        {
            // Set custom buttons
            DialogForm.SetButton(1, Config.CustomButton1Text);
            DialogForm.SetButton(2, Config.CustomButton2Text);
            DialogForm.SetButton(3, Config.CustomButton3Text);

            // Show dialog
            return DialogForm.ShowDialog(Config.Message, Config.Title, Config.Button1, Config.Button2, Config.Button3);
        }
    }
}
