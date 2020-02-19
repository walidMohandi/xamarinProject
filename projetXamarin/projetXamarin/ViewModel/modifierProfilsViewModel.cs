using projetXamarin.Models;
using Storm.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace projetXamarin.ViewModel
{
    class modifierProfilsViewModel:ViewModelBase
    {
        private string monImage;
        public string MonImage
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
        

        public ICommand btnModifierProfils { get; }
       
        public modifierProfilsViewModel()
        {
            MonFirstName = "";
            MonLastName = "";
            MonImage = "";
            

            btnModifierProfils = new Command(btnModifierProfilsAction);
           
        }

        private async void btnModifierProfilsAction(object obj)
        {
            await DependencyService.Resolve<IAuthService>().modifierProfils(monFirstName, MonLastName, MonImage);
        }

        
    }
}
