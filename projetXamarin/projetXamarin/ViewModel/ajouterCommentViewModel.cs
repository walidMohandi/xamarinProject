using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using projetXamarin.Models;
using Storm.Mvvm;
using Xamarin.Forms;

namespace projetXamarin.ViewModel
{
    public class ajouterCommentViewModel:ViewModelBase
    {
        private string commentaire;
        public string Commentaire
        {
            get => commentaire;
            set => SetProperty(ref commentaire, value);
        }
        
            public ICommand btnAjouterCommentaire { get; }

        public ajouterCommentViewModel()
        {
            btnAjouterCommentaire = new Command (btnAjouterCommentaireAction);
            Commentaire = "";
        }

        private async void btnAjouterCommentaireAction()
        {
            await DependencyService.Resolve<IAuthService>().commenter(Commentaire,lieuxViewModel.leIdSelected);
        }
    }
}
