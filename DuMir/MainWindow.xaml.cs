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
		private readonly StringBuilder prepareOutputText = new StringBuilder();
		private readonly DispatcherTimer timer = new DispatcherTimer();


		public MainWindow()
		{
			InitializeComponent();

			ConsoleHandler.InitGlobal(new ConsoleHandler(ReadFromConsole, WriteToConsole));

			Logger.LogMessage("Welcome to DuMir Interpretator", Logger.LogLevel.Console);
			Logger.LogMessage("Press [TAB] to run program", Logger.LogLevel.Console);

			consoleTextBox.TextChanged += (sender, e) =>
			{
				consoleTextBox.ScrollToEnd();
			};

			timer.Interval = new TimeSpan(200000);
			timer.Tick += Timer_Tick;
			timer.Start();
		}


		private void Timer_Tick(object sender, EventArgs e)
		{
			consoleTextBox.AppendText(prepareOutputText.ToString());
			prepareOutputText.Clear();
		}
		private void Window_KeyDown(object sender, KeyEventArgs e)
		{
			if(e.Key == Key.Enter && e.IsDown)
			{
				inputText.Append(consoleInputTextBox.Text + "\n");
				Logger.LogMessage(consoleInputTextBox.Text, Logger.LogLevel.UserInput);
				consoleInputTextBox.Clear();
			}

			if (e.Key == Key.Tab && e.IsDown)
			{
				Run();
			}

		}

		private async void Window_Loaded(object sender, RoutedEventArgs e)
		{
			if((bool)Static.LaunchArguments["IsAutoRun"])
				Logger.LogMessage("Auto run is ENABLED", Logger.LogLevel.Console);

			if((bool)Static.LaunchArguments["EnableDebug"])
				Logger.LogMessage("Debug is ENABLED", Logger.LogLevel.Console);

			if((bool)Static.LaunchArguments["IsAutoClosing"])
				Logger.LogMessage("Auto closing is ENABLED", Logger.LogLevel.Console);

			if((bool)Static.LaunchArguments["IsAutoRun"])
			{
				await Task.Delay(500);
				Run();
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

		private async void Run()
		{
			await Task.Run(() =>
			{
				var project = new PreLoader().Run();
				project = new PostLoader().Run(project);
				var analysedProject = new CodeAnalyser().Run(project);
				new Interpretator().Run(analysedProject);
			});

			if((bool)Static.LaunchArguments["IsAutoClosing"])
			{
				Logger.LogMessage("Window closing after 3 seconds", Logger.LogLevel.Console);
				await Task.Delay(3000);
				Close();
			}
		}
	}
}
