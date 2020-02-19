using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using projetXamarin.Models;
using Storm.Mvvm;
using TD.Api.Dtos;
using Xamarin.Forms;

namespace projetXamarin.ViewModel
{
    class loginViewModel:ViewModelBase
    {
        private string username;
        public string Username
        {
            get => username;
            set => SetProperty(ref username, value);
        }
        private string password;
        public string Password
        {
            get => password;
            set => SetProperty(ref password, value);
        }

        public ICommand btnSubmit { get; private set; }
        public ICommand btnCreer{ get; private set; }

        public loginViewModel()
        {
            btnSubmit = new Command
                (async () => await ValiderAuth());
            btnCreer = new Command(btnCreerAction);
        }

        private async void btnCreerAction(object obj)
        {
            await Application.Current.MainPage.Navigation.PushAsync(new registerView());
        }

        public static LoginResult infoLogin;

        async Task ValiderAuth()
        {
            infoLogin=await DependencyService.Resolve<IAuthService>().Auth(Username,Password);
        }
    }

   
}
