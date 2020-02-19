using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using projetXamarin.Models;
using Storm.Mvvm;
using TD.Api.Dtos;
using Xamarin.Forms;

namespace projetXamarin.ViewModel
{
    public class commentViewModel: ViewModelBase
    {

        private List<CommentItem> listComments;
        public List<CommentItem> ListComments
        {
            get => listComments;
            set => SetProperty(ref listComments, value);
        }
        private PlaceItem getPlaceItem;
        public PlaceItem GetPlaceItem
        {
            get => getPlaceItem;
            set => SetProperty(ref getPlaceItem, value);
        }
        
            

        private string leId;
        public  string LeId
        {
            get => leId;
            set => SetProperty(ref leId, value);
        }

        public ICommand btnAjouterCommentaire { get; }
        public commentViewModel()
        {
            btnAjouterCommentaire = new Command(btnAjouterCommentaireAction);

            ListComments = new List<CommentItem>();
            
            
            getComments(lieuxViewModel.leIdSelected);
            
        }

       
        //
        public async void getComments(int id)
        {
            GetPlaceItem = await DependencyService.Resolve<IAuthService>().getComments(id);
            ListComments = GetPlaceItem.Comments;
        }

        

        private async void btnAjouterCommentaireAction(object obj)
        {
            await Application.Current.MainPage.Navigation.PushAsync(new ajouterCommentView());
        }
    }
}
