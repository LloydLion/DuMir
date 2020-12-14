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
using System.Windows.Threading;

namespace DuMir
{
	partial class MainWindow : Window
	{
		private StringBuilder inputText = new StringBuilder();
		private StringBuilder prepareOutputText = new StringBuilder();
		private DispatcherTimer timer = new DispatcherTimer();


		public MainWindow()
		{
			InitializeComponent();

			ConsoleHandler.InitGlobal(new ConsoleHandler(ReadFromConsole, WriteToConsole));

			Logger.LogMessage("Privet Mir", Logger.LogLevel.Warning);

			consoleTextBox.TextChanged += (sender, e) =>
			{
				consoleTextBox.ScrollToEnd();
			};

			timer.Interval = new TimeSpan(2000000);
			timer.Tick += Timer_Tick;
			timer.Start();
		}


		private void Timer_Tick(object sender, EventArgs e)
		{
			consoleTextBox.AppendText(prepareOutputText.ToString());
			prepareOutputText.Clear();
		}

		private async void MainWindow_KeyDown(object sender, KeyEventArgs e)
		{
			if(e.Key == Key.Enter && e.IsDown)
			{
				inputText.Append(consoleInputTextBox.Text + "\n");
				Logger.LogMessage(consoleInputTextBox.Text, Logger.LogLevel.UserInput);
				consoleInputTextBox.Clear();
			}

			if(e.Key == Key.Tab && e.IsDown)
			{
				var project = await new PreLoader().Run();
				project = await new PostLoader().Run(project);
				var analysedProject = await new CodeAnalyser().Run(project);
				new Interpretator().Run(analysedProject);
			}
		}

		private void WriteToConsole(string arg)
		{
			prepareOutputText.Append(arg);
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
