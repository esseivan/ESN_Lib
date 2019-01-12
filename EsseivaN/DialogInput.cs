/*
 * File     : DialogInput.cs
 * Author   : Esseiva Nicolas
 * Date     : 18.11.2017
 * 
 * Allows the user to enter an input and
 * use the result in the software
 */

using System.ComponentModel;
using System.Drawing;

namespace EsseivaN.Controls
{
	public class DialogInput : Component
	{
		private DialogInputForm dialogInput = new DialogInputForm();

		private string _Title = "Dialog Input";
		/// <summary>
		/// Title of the window
		/// </summary>
		public string Title
		{
			get { return _Title; }
			set { _Title = value; }
		}

		private string _Question = "Enter Input";
		/// <summary>
		/// Question of the window
		/// </summary>
		public string Question
		{
			get { return _Question; }
			set { _Question = value; }
		}

		private string _DefaultInput = "";
		/// <summary>
		/// Question of the window
		/// </summary>
		public string DefaultInput
		{
			get { return _DefaultInput; }
			set { _DefaultInput = value; }
		}

		private string _Input = null;
		/// <summary>
		/// Input of the user
		/// </summary>
		public string Input
		{
			get { return _Input; }
		}

		public enum Localizations
		{
			FR,
			EN
		}
		private Localizations _Localization = Localizations.EN;
		/// <summary>
		/// Localization
		/// </summary>
		public Localizations Localization
		{
			get { return _Localization; }
			set { _Localization = value; }
		}

		private ButtonType _Button1 = ButtonType.OK;
		public ButtonType Button1 { get => _Button1; set => _Button1 = value; }

		private ButtonType _Button2 = ButtonType.Skip;
		public ButtonType Button2 { get => _Button2; set => _Button2 = value; }

		private CancelButtonType _ButtonCancel = CancelButtonType.Cancel;
		public CancelButtonType ButtonCancel { get => _ButtonCancel; set => _ButtonCancel = value; }

		private WindowPositions _WindowPosition = WindowPositions.CenterFirstRun;

		public Point LastLocation { get => dialogInput.lastLocation; }

		/// <summary>
		/// Position of the window, default centered the first run
		/// </summary>
		public WindowPositions WindowPosition
		{
			get { return _WindowPosition; }
			set { _WindowPosition = value; }
		}

		/// <summary>
		/// Positions of the window
		/// </summary>
		public enum WindowPositions
		{
			CenterAlways,
			CenterFirstRun,
			DefaultAlways,
			DefaultFirstRun,
			Manual
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
		}

		/// <summary>
		/// Type of cancel buttons
		/// </summary>
		public enum CancelButtonType
		{
			None = 0,
			Cancel = 8,
			Abort = 9,
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
		}

		internal enum ButtonTypeFR
		{
			None = 0,
			OK = 1,
			Passer = 2,
			Ignorer = 3,
			Continuer = 4,
			Accepter = 5,
			Précédent = 6,
			Suivant = 7,
			Annuler = 8,
			Interrompre = 9,
		}

		/// <summary>
		/// Ask the user for an input
		/// </summary>
		public DialogInput()
		{

		}

		/// <summary>
		/// Initialize window
		/// </summary>
		/// <param name="Question">Question</param>
		/// <param name="Title">Title</param>
		/// <param name="DefaultInput">Default input</param>
		/// <param name="WindowPosition">Window's position</param>
		public void Initialize(string Question = "Enter input", string Title = "Dialog input", string DefaultInput = "", DialogInput.WindowPositions WindowPosition = DialogInput.WindowPositions.DefaultFirstRun, bool FirstRun = false)
		{
			this.Question = Question;
			this.Title = Title;
			this.DefaultInput = DefaultInput;
			this._WindowPosition = WindowPosition;
		}

		/// <summary>
		/// Show the window and await user's input
		/// </summary>
		/*/// <param name="FirstRun">Wheter or not the window place as if it's the first run</param>*/
		public DialogResult ShowDialog(/*bool FirstRun = false*/)
		{   // Reset the input
			_Input = null;
			// Set the question and title
			dialogInput.Initialize(Question, Title, DefaultInput);
			// Set the window location
			dialogInput.WindowPosition(WindowPosition);
			dialogInput.FR = (Localization == Localizations.FR);
			dialogInput.button1 = _Button1;
			dialogInput.button2 = _Button2;
			dialogInput.buttonCancel = _ButtonCancel;
			// Show the window
			DialogResult dialogresult = dialogInput.ShowDialog();

			// Get the result
			_Input = dialogInput.Value;
			// Hide the window
			dialogInput.Hide();

			return dialogresult;
		}
	}
}
