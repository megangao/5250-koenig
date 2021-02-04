using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Mine.Services;
using Mine.Views;

namespace Mine
{
    public partial class App : Application
    {
        /// <summary>
        /// Initializes app and sets data dependency service
        /// </summary>
        public App()
        {
            InitializeComponent();

            // Uncomment next line for mock data
            //DependencyService.Register<MockDataStore>();
            
            // Uncomment next line for real data
            DependencyService.Register<DatabaseService>();

            MainPage = new MainPage();
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
