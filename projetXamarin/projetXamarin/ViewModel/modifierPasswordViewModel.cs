using projetXamarin.Models;
using Storm.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace projetXamarin.ViewModel
{
    class modifierPasswordViewModel:ViewModelBase
    {
        private string ancienPassword;
        public string  AncienPassword
        {
            get => ancienPassword;
            set => SetProperty(ref ancienPassword, value);
        }
        private string nvPassword;
        public string NvPassword
        {
            get => nvPassword;
            set => SetProperty(ref nvPassword, value);
        }
        
              public ICommand btnModifierPassword { get; }
        public modifierPasswordViewModel()
        {
            btnModifierPassword = new Command(btnModifierPasswordAction);
            AncienPassword = "";
            NvPassword = "";
        }

        private async void btnModifierPasswordAction(object obj)
        {
             await DependencyService.Resolve<IAuthService>().modifierPassword(AncienPassword, NvPassword);
        }
    }
}
