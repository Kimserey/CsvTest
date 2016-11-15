using System;
using System.IO;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Xamarin.Forms;

[assembly: Dependency(typeof(CsvTest.Droid.FileService))]
namespace CsvTest.Droid
{
	public class FileService : IFileService
	{
		// Returns null if file does not exists.
		public StreamReader GetFileStream(string name)
		{
			var path = Path.Combine(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath, "CsvTest", "test.csv");
			if(File.Exists(path))
				return new StreamReader(File.OpenRead(path));
			else 
				return null;
		}
	}

	[Activity(Label = "CsvTest.Droid", Icon = "@drawable/icon", Theme = "@style/MyTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
	{
		protected override void OnCreate(Bundle bundle)
		{
			TabLayoutResource = Resource.Layout.Tabbar;
			ToolbarResource = Resource.Layout.Toolbar;

			base.OnCreate(bundle);

			global::Xamarin.Forms.Forms.Init(this, bundle);

			LoadApplication(new App());
		}
	}
}
