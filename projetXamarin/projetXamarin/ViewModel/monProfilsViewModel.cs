using projetXamarin.Models;
using Storm.Mvvm;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Input;
using TD.Api.Dtos;
using Xamarin.Forms;

namespace projetXamarin.ViewModel
{
    class monProfilsViewModel:ViewModelBase
    {
        private int monImage;
        public int MonImage
        {
            get => monImage;
            set => SetProperty(ref monImage, value);
        }
        private string monFirstName;
        public string MonFirstName
        {
            get => monFirstName;
            set => SetProperty(ref monFirstName, value);
        }
        private string monLastName;
        public string MonLastName
        {
            get => monLastName;
            set => SetProperty(ref monLastName, value);
        }
        private string monEmail;
        public string MonEmail
        {
            get => monEmail;
            set => SetProperty(ref monEmail, value);
        }

        public ICommand btnModifierProfils { get; }
        public ICommand btnModifierPassword { get; }
        public monProfilsViewModel()
        {
            getProfils();

            btnModifierProfils = new Command(btnModifierProfilsAction);
            btnModifierPassword = new Command(btnModifierPasswordAction);
        }

        private async void btnModifierPasswordAction(object obj)
        {
            await Application.Current.MainPage.Navigation.PushAsync(new modifierPasswordView());
        }

        public async void getProfils()
        {
            UserItem u = new UserItem();
            u = await DependencyService.Resolve<IAuthService>().getMonProfils();
            MonFirstName =u.FirstName;
            MonLastName =u.LastName;
            MonEmail =u.Email;
            MonImage = Convert.ToInt32(u.ImageId);
            //byte[] img = await DependencyService.Resolve<IAuthService>().getImage(Convert.ToInt32(u.ImageId));
            //MonImage = ImageSource.FromStream(() => new MemoryStream(img));
        }

        private async void btnModifierProfilsAction(object obj)
        {
            await Application.Current.MainPage.Navigation.PushAsync(new modifierProfilsView());
        }
    }
}
