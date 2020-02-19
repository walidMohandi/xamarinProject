using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;

namespace projetXamarin
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class mapsPage : ContentPage
    {
        public mapsPage()
        {
            InitializeComponent();

           
             
        }
        public async Task NavigateToBuilding25()
        {
            var location = new Location(47.645160, -122.1306032);
            var options = new MapLaunchOptions { Name = "Microsoft Building 25" };

            await Map.OpenAsync(location, options);
        }

        private async void ButtonOpenCoords_Clicked(object sender, EventArgs e)
        {
            if (!double.TryParse(EntryLatitude.Text, out double lat)) return;
            if (!double.TryParse(EntryLongitude.Text, out double lng)) return;

            await Map.OpenAsync(lat, lng, new MapLaunchOptions
            {
                Name = EntryName.Text,
                NavigationMode=NavigationMode.None
            }) ;
        }
    }

   
    
}