using Calculator.Mobile.Services;
using Calculator.Mobile.Views;
using System;
using System.Threading;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Calculator.Mobile
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
            var cultureVal = "pl-PL";
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(cultureVal);
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(cultureVal);
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
