using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Windows.Input;
using projetXamarin.Models;
using Storm.Mvvm;
using TD.Api.Dtos;
using Xamarin.Essentials;
using Xamarin.Forms;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms.Xaml;


namespace projetXamarin.ViewModel
{
    public class Item
    {
        private string titreImage;
        public string TitreImage { 
            get=>titreImage; 
            set=> titreImage=value;
        }
        private ImageSource img;
        public ImageSource Img
        {
            get => img;
            set => img = value;
        }
        
    }
    public  class lieuxViewModel:ViewModelBase
    {
        private List<PlaceItemSummary> listLieux;
        private List<ItemLieu> listLieuxFinal;
        public List<ItemLieu> ListLieuxFinal
        {
            get => listLieuxFinal;
            set => SetProperty(ref listLieuxFinal, value);
        }

        public List<PlaceItemSummary> ListLieux
        {
            get => listLieux;
            set => SetProperty(ref listLieux, value);
        }
        public ICommand GoToAjouterLieu { get; }
        public lieuxViewModel()
        {
            listLieux = new List<PlaceItemSummary>();
            listLieuxFinal = new List<ItemLieu>();
            listComments = new List<CommentItem>();


            GoToMaps = new Command(GoToMapsAction);
            GoToComments = new Command(GoToCommentsAction);
            GoToAjouterLieu= new Command(GoToAjouterLieusAction);
            GoToProfils= new Command(GoToProfilsAction);
            GetLieux();
        }

       

        private async void GoToAjouterLieusAction(object obj)
        {
            await Application.Current.MainPage.Navigation.PushAsync(new ajouterLieuView());
        }

        private PlaceItemSummary lieuSelected;
        public  PlaceItemSummary LieuSelected
        {
            get => lieuSelected;
            set => lieuSelected=value;
        }

        private async void GoToMapsAction()
        {
            if (LieuSelected != null)
            {
                await Map.OpenAsync(LieuSelected.Latitude, LieuSelected.Longitude, new MapLaunchOptions
                {
                    Name = LieuSelected.Title,
                    NavigationMode = NavigationMode.None
                });
            }
            
        }
       

        private ImageSource srcImage;
        public ImageSource SrcImage
        {
            get => srcImage;
            set =>SetProperty(ref srcImage,value);
        }

        public ICommand GoToMaps { get; }

       

        public async void GetLieux()
        {

            ListLieux = await DependencyService.Resolve<IAuthService>().getListLieux();

        }
        
        /**************************Comments***********************************/
        
       

        public ICommand GoToComments { get; }

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

        public static int leIdSelected;

        private async void GoToCommentsAction()
        {
            if (LieuSelected != null)
            {
                leIdSelected = LieuSelected.Id;
                await Application.Current.MainPage.Navigation.PushAsync(new commentView());
            }
    
        }

        /*************************************Profils*********************************/
        public ICommand GoToProfils { get; }

        public async void GoToProfilsAction()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new monProfilsView());
        }


    }
}
