using System;
using LastyTestProject.Services;
using LastyTestProject.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LastyTestProject
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
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
