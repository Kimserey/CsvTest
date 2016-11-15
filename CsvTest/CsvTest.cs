using System;
using CsvHelper;
using Xamarin.Forms;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CsvTest
{
	public interface IFileService {

		StreamReader GetFileStream(string name);
	}

	public class Record
	{
		public string Title { get; set; }
		public decimal Amount { get; set; }
		public DateTime Date { get; set; }
	}

	public class MainPage : ContentPage
	{
		public MainPage()
		{
			var csvName = "test.csv";
			var button = new Button { Text = "Try read CSV" };

			button.Clicked += async (sender, e) => {
				var list = new List<Record>();
				using (var reader = DependencyService.Get<IFileService>().GetFileStream(csvName))
				{
					if (reader != null)
					{
						using (var csv = new CsvReader(reader))
						{
							while (csv.Read())
							{
								list.Add(new Record
								{
									Date = csv.GetField<DateTime>(0),
									Title = csv.GetField<string>(1),
									Amount = csv.GetField<decimal>(2)
								});
							}
						}
					}
				}
				await this.DisplayAlert("Test", "worked", "OK");
			};

			Title = "Csv reader test";
			Content = new StackLayout
			{
				Children = {
					button
				}
			};
		}
	}

	public class App : Application
	{
		public App()
		{
			MainPage = new NavigationPage(new MainPage());
		}
	}
}
