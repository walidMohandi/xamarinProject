
using projetXamarin.ViewModel;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace projetXamarin
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();


            DependencyService.Register<Models.IAuthService, Models.IAuthModel>();
            MainPage = new NavigationPage( new MainPage() );
            

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
