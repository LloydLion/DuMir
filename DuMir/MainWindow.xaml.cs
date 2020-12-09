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
	partial class MainWindow : Window
	{
		private StringBuilder inputText = new StringBuilder();


		public MainWindow()
		{
			InitializeComponent();

			ConsoleHandler.InitGlobal(new ConsoleHandler(ReadFromConsole, WriteToConsole));

			Logger.LogMessage("Privet Mir", Logger.LogLevel.Warning);

			consoleTextBox.TextChanged += (sender, e) =>
			{
				consoleTextBox.ScrollToEnd();
			};
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

			if(e.Key == Key.Tab && e.IsDown)
			{
				var project = new PreLoader().Run();
				project = new PostLoader().Run(project);
			}
		}

		private void WriteToConsole(string arg)
		{
			consoleTextBox.Text += arg;
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
