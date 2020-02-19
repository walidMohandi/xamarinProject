using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using projetXamarin.Models;
using Storm.Mvvm;
using Xamarin.Forms;

namespace projetXamarin.ViewModel
{
    class registerViewModel:ViewModelBase
    {
        private string ajouterEmail;
        public string AjouterEmail
        {
            get => ajouterEmail;
            set => SetProperty(ref ajouterEmail, value);
        }
        private string ajouterFirstname;
        public string AjouterFirstname
        {
            get => ajouterFirstname;
            set => SetProperty(ref ajouterFirstname, value);
        }
        private string ajouterLastname;
        public string AjouterLastname
        {
            get => ajouterLastname;
            set => SetProperty(ref ajouterLastname, value);
        }
        private string ajouterPassword;
        public string AjouterPassword
        {
            get => ajouterPassword;
            set => SetProperty(ref ajouterPassword, value);
        }

        public ICommand btnInscription { get; set; }
        public registerViewModel()
        {
            btnInscription = new Command(btnInscriptionAction);
            AjouterEmail = "";
            AjouterFirstname = "";
            AjouterLastname= "";
            AjouterPassword = "";
            


        }

        private void btnInscriptionAction(object obj)
        {
            DependencyService.Resolve<IAuthService>().register(AjouterEmail, AjouterFirstname, AjouterLastname, AjouterPassword);

        }
    }
}
