/*
 * File     : DialogInputForm.cs
 * Author   : Esseiva Nicolas
 * Date     : 18.11.2017
 * 
 * Allows the user to enter an input and
 * use the result in the software
 */

using System;
using System.Drawing;
using System.Windows.Forms;

namespace EsseivaN.Controls
{
	internal partial class DialogInputForm : Form
	{
		#region Declarations

		// User's input
		private static string value;
		public string Value
		{
			get
			{
				return value;
			}
		}

		// Saved window's location
		private static Point WindowLocation;
		public Point lastLocation { get => WindowLocation; }

		// Wheter to keep last window's position or not
		private static bool FreezeWindow = false;

		// Wheter this is the first run or not
		public bool FirstRun = true;

		public DialogInput.ButtonType button1 = DialogInput.ButtonType.OK;
		public DialogInput.ButtonType button2 = DialogInput.ButtonType.Ignore;
		public DialogInput.CancelButtonType buttonCancel = DialogInput.CancelButtonType.Cancel;
		private DialogInput.DialogResult button1Result;
		private DialogInput.DialogResult button2Result;
		private DialogInput.DialogResult buttonCancelResult;
		private DialogInput.DialogResult result = DialogInput.DialogResult.None;

		public bool FR = false;

		#endregion

		#region initialization

		/// <summary>
		/// Dialog input window
		/// </summary>
		public DialogInputForm()
		{
			InitializeComponent();
		}

		/// <summary>
		/// Initialize window
		/// </summary>
		/// <param name="Question">Question</param>
		/// <param name="Title">Title</param>
		/// <param name="DefaultInput">Default input</param>
		/// <param name="WindowPosition">Window's position</param>
		public void Initialize(string Question = "Enter input", string Title = "Dialog input", string DefaultInput = "")
		{
			// Question
			lblQuestion.Text = Question;

			// Title
			this.Text = Title;

			// Default input
			this.mTextbox_Input.Text = DefaultInput;
		}

		/// <summary>
		/// Set the window's position
		/// </summary>
		/// <param name="PosX">X Position</param>
		/// <param name="PosY">Y Position</param>
		public void WindowPosition(int PosX, int PosY)
		{
			this.StartPosition = FormStartPosition.Manual;
			WindowLocation = new Point(PosX, PosY);
		}

		/// <summary>
		/// Set the window's position
		/// </summary>
		/// <param name="WindowPosition">Window's position type</param>
		public void WindowPosition(DialogInput.WindowPositions WindowPosition)
		{
			switch (WindowPosition)
			{
				case DialogInput.WindowPositions.CenterAlways:
					{   // Keep the window at center
						this.StartPosition = FormStartPosition.CenterScreen;
						FreezeWindow = true;
						break;
					}
				case DialogInput.WindowPositions.CenterFirstRun:
					{   // Place the window at center the first time
						this.StartPosition = FormStartPosition.CenterScreen;
						FreezeWindow = false;
						break;
					}
				case DialogInput.WindowPositions.DefaultAlways:
					{   // Place the window at default location
						this.StartPosition = FormStartPosition.WindowsDefaultLocation;
						FreezeWindow = true;
						break;
					}
				case DialogInput.WindowPositions.DefaultFirstRun:
					{   // Place the window at default location the first time
						this.StartPosition = FormStartPosition.WindowsDefaultLocation;
						FreezeWindow = false;
						break;
					}
				case DialogInput.WindowPositions.Manual:
					{   // Place the window at default location the first time
						this.StartPosition = FormStartPosition.Manual;
						FreezeWindow = false;
						break;
					}
				default:
					{   // Place the window at center the first time
						this.StartPosition = FormStartPosition.CenterScreen;
						FreezeWindow = false;
						break;
					}
			}
		}

		/// <summary>
		/// Define window's size
		/// <para>Default and minimum size is 300x170</para>
		/// </summary>
		/// <param name="Width">Width</param>
		/// <param name="Height">Height</param>
		public void SetSize(short Width, short Height)
		{   // Si supérieur aux limites
			if ((Width > this.MinimumSize.Width) && (Height > this.MinimumSize.Height))
			{
				this.Size = new Size(Width, Height);
			}
		}

		public new DialogInput.DialogResult ShowDialog()
		{
			result = DialogInput.DialogResult.None;
			base.ShowDialog();
			return result;
		}

		#endregion

		#region Main

		// Execute on load
		private void DialogInputForm_Load(object sender, EventArgs e)
		{
			if (FirstRun)
			{
				FirstRun = false;

				//if (!FreezeWindow)
				//    StartPosition = FormStartPosition.Manual;
			}
			else
			{
				// Set window's position at last one
				if (!FreezeWindow)
					this.Location = WindowLocation;
			}
			// Clear input
			value = "";
			// Set buttons
			setButtons();
		}

		private void setButtons()
		{
			mButton_1.Visible = true;
			mButton_2.Visible = true;
			mButton_Cancel.Visible = true;

			button1Result = button2Result = buttonCancelResult = DialogInput.DialogResult.None;

			// Button 1
			if (button1 == DialogInput.ButtonType.None)
				mButton_1.Visible = false;
			else
			{
				button1Result = (DialogInput.DialogResult)button1;
				mButton_1.Text = FR ? ((DialogInput.ButtonTypeFR)button1).ToString() : button1.ToString();
			}

			// Button 2
			if (button2 == DialogInput.ButtonType.None)
				mButton_2.Visible = false;
			else
			{
				button2Result = (DialogInput.DialogResult)button2;
				mButton_2.Text = FR ? ((DialogInput.ButtonTypeFR)button2).ToString() : button2.ToString();
			}

			// Cancel button
			if (buttonCancel == DialogInput.CancelButtonType.None)
				mButton_Cancel.Visible = false;
			else
			{
				buttonCancelResult = (DialogInput.DialogResult)buttonCancel;
				mButton_Cancel.Text = FR ? ((DialogInput.ButtonTypeFR)buttonCancel).ToString() : buttonCancel.ToString();
			}

		}

		// Execute when a button is clicked
		private void mButton_2_Click(object sender, EventArgs e)
		{
			result = button2Result;
			Close();
		}

		#endregion

		private void mButton_1_Click(object sender, EventArgs e)
		{
			result = button1Result;
			Close();
		}

		private void mButton_Cancel_Click(object sender, EventArgs e)
		{
			result = buttonCancelResult;
			Close();
		}

		private void DialogInputForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			// Save the value
			value = mTextbox_Input.Text;
			// Initialize for the next call
			mTextbox_Input.Clear();
			mTextbox_Input.Focus();
			// Save window's position for the next call
			WindowLocation = this.Location;

			result = result == DialogInput.DialogResult.None ? DialogInput.DialogResult.Cancel : result;
		}
	}
}
