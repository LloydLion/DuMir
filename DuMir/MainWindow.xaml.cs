using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DuMir
{
	public partial class MainWindow : Window
	{
		private StringBuilder inputText = new StringBuilder();


		public MainWindow()
		{
			InitializeComponent();

			ConsoleHandler.InitGlobal(new ConsoleHandler(ReadFromConsole, WriteToConsole));

			Logger.LogMessage("Privet Mir", Logger.LogLevel.Warning);
		}

		private void MainWindow_KeyDown(object sender, KeyEventArgs e)
		{
			if(e.Key == Key.Enter && e.IsDown)
			{
				inputText.Append(consoleInputTextBox.Text);
				Logger.LogMessage(consoleInputTextBox.Text, Logger.LogLevel.UserInput);
				Logger.LogMessage("Privet Mir", Logger.LogLevel.Info);
				consoleInputTextBox.Clear();
			}
		}

		private void WriteToConsole(string arg)
		{
			consoleTextBlock.Text += arg;
		}

		private string ReadFromConsole(int length)
		{
			while(inputText.Length < length) Thread.Sleep(5);

			var result = inputText.ToString().Substring(0, length);
			inputText = inputText.Remove(0, length);

			return result;
		}
	}
}
