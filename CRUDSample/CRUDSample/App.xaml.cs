using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace CRUDSample
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            DependencyService.Get<ISQLite>().GetConnectionWithCreateDatabase();
            MainPage = new NavigationPage(new NavBar());
        }

        protected override void OnStart() { }

        protected override void OnSleep() { }

        protected override void OnResume() { }
    }
}
