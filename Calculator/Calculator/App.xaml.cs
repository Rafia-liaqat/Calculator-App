using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly:ExportFont("digital-7.ttf")]
namespace Calculator
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
        


             // MainPage = new MainPage();
            MainPage = new Calculatorro();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
