using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using projetXamarin.Models;
using Storm.Mvvm;
using Xamarin.Forms;

namespace projetXamarin.ViewModel
{
    class ajouterLieuViewModel : ViewModelBase
    {
        private string ajouterTitre;
        public string AjouterTitre
        {
            get => ajouterTitre;
            set => SetProperty(ref ajouterTitre, value);
        }
        private string ajouterDescription;
        public string AjouterDescription
        {
            get => ajouterDescription;
            set => SetProperty(ref ajouterDescription, value);
        }
        private string ajouterIdImage;
        public string AjouterIdImage { 
            get => ajouterIdImage;
            set => SetProperty(ref ajouterIdImage, value);
        }
        private string ajouterLatitude;
        public string AjouterLatitude
        {
            get => ajouterLatitude;
            set => SetProperty(ref ajouterLatitude, value);
        }
        private string ajouterLongitude;
        public string AjouterLongitude
        {
            get => ajouterLongitude;
            set => SetProperty(ref ajouterLongitude, value);
        }


        private ImageSource img;
        public ImageSource Img
        {
            get => img;
            set => SetProperty(ref img, value);
        }
        public ICommand ValiderLieu { get; set; }
        public ICommand btnTakePhoto { get; set; }
        public ajouterLieuViewModel()
        {
            
               ValiderLieu = new Command(ValiderLieuAction);
            btnTakePhoto= new Command(btnTakePhotoAction);
            AjouterDescription = "";
            AjouterIdImage = "";
            AjouterLatitude = "";
            AjouterLongitude = "";
            AjouterTitre = "";


        }

        private async void btnTakePhotoAction(object obj)
        {
            var photo = await Plugin.Media.CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions() { });

            if (photo != null)
                Img = ImageSource.FromStream(() => { return photo.GetStream(); });
        }

        public void ValiderLieuAction()
        {
            DependencyService.Resolve<IAuthService>().ajouterPlace(AjouterTitre, AjouterDescription, AjouterIdImage, ajouterLatitude, AjouterLongitude);
        }

    }
}
