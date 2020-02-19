using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using TD.Api.Dtos;
using System.Linq;
using Xamarin.Forms.Xaml;
using System.Collections.ObjectModel;
using Common.Api.Dtos;
using System.Diagnostics;
using System.IO;
using SixLabors.ImageSharp.Formats.Jpeg;
using Xamarin.Forms;
using SixLabors.ImageSharp;
using Xamarin.Essentials;
using projetXamarin.ViewModel;

namespace projetXamarin.Models
{
    class IAuthModel : IAuthService
    {
        public const string URL = "https://td-api.julienmialon.com/";
        

        public async Task<LoginResult> Auth(string login, string password)
        {
            HttpClient client = new HttpClient();
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, URL+"auth/login");           

            var content = new LoginRequest
            {
                Email=login,
                Password=password,
            };


            request.Content = new StringContent(JsonConvert.SerializeObject(content),Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.SendAsync(request);
            

            string result = await response.Content.ReadAsStringAsync();

            Response<LoginResult> r= JsonConvert.DeserializeObject<Response<LoginResult>>(result);



            if (response.StatusCode==System.Net.HttpStatusCode.OK )
            
            {

                Console.WriteLine("YEEEEEEEEEEEEEEEEEEEEEEs");
                await Application.Current.MainPage.Navigation.PushAsync(new listeLieuxPage());
                Console.WriteLine(r.Data.AccessToken);
                Console.WriteLine("YEEEEEEEEEEEEEEEEEEEEEEs");

                return r.Data;


            }
            else
            {
                Console.WriteLine("Nooooooooooooooooooooo");
                return r.Data;
            }


        }

        public async Task<List<PlaceItemSummary>> getListLieux()
        {
            ApiClient apiClient = new ApiClient();
            HttpResponseMessage reponse = await apiClient.Execute(HttpMethod.Get, URL + "places", null, null);
            Response<List<PlaceItemSummary>> c = await apiClient.ReadFromResponse<Response<List<PlaceItemSummary>>>(reponse);
            //Console.WriteLine(c);
            return c.Data;
           
        }
        public async Task<byte[]> getImage(int id)
        {
            ApiClient apiClient = new ApiClient();
            HttpResponseMessage reponse = await apiClient.Execute(HttpMethod.Get, URL + "images/"+ Convert.ToString(id), null, null);
            
            byte[] result = await reponse.Content.ReadAsByteArrayAsync();
         
            using (var image = SixLabors.ImageSharp.Image.Load(new MemoryStream(result)))
            using (var outputStream = new MemoryStream())
            {
                image.SaveAsJpeg(outputStream, new JpegEncoder { Quality = 95 });
                
                return outputStream.ToArray();
            }

          
        }

        public async Task ajouterPlace(string title, string description, string idImage, string latitude, string longitude)
        {
            
            if (!title.Equals("") && !description.Equals("") && !idImage.Equals(""))
            {
                if (latitude == "" || longitude == "")
                {
                    try
                    {
                        var location = await Geolocation.GetLastKnownLocationAsync();

                        if (location != null)
                        {
                            Console.WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}");
                            latitude = location.Latitude.ToString();
                            longitude = location.Longitude.ToString();


                        }
                    }
                    catch (FeatureNotSupportedException fnsEx)
                    {
                        // Handle not supported on device exception 
                        Console.WriteLine(fnsEx.InnerException);
                    }
                    catch (FeatureNotEnabledException fneEx)
                    {
                        // Handle not enabled on device exception
                        Console.WriteLine(fneEx.InnerException);
                    }
                    catch (PermissionException pEx)
                    {
                        // Handle permission exception
                        Console.WriteLine(pEx.InnerException);
                    }
                    catch (Exception ex)
                    {
                        // Unable to get location
                        Console.WriteLine(ex.InnerException);
                    }
                }

                CreatePlaceRequest content = new CreatePlaceRequest
                {
                    Title = title,
                    Description = description,
                    ImageId = int.Parse(idImage),
                    Latitude = double.Parse(latitude),
                    Longitude = double.Parse(longitude),
                };


                ApiClient apiClient = new ApiClient();
                HttpResponseMessage response = await apiClient.Execute(HttpMethod.Post, URL + "places", content, loginViewModel.infoLogin.AccessToken);

                string result = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == System.Net.HttpStatusCode.OK)

                {

                    Console.WriteLine("YEEEEEEEEEEEEEEEEEEEEEEs");
                    await App.Current.MainPage.DisplayAlert("Alert", "Lieu ajouté !", "OK");
                    await Application.Current.MainPage.Navigation.PushAsync(new listeLieuxPage());


                }
                else
                {
                    Console.WriteLine("Nooooooooooooooooooooo");
                    await App.Current.MainPage.DisplayAlert("Alert", "Echec !", "OK");
                }

            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Alert", "Veuillez remplir au minimum les 3 premiers champs", "OK");
            }


        }

        public async Task register(string email, string firstname, string lastname, string password)
        {
            if(!email.Equals( "") && !firstname.Equals("") && !lastname.Equals("") && !password.Equals(""))
            {
                HttpClient client = new HttpClient();
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, URL + "auth/register");

                RegisterRequest content = new RegisterRequest
                {
                    Email = email,
                    FirstName = firstname,
                    LastName = lastname,
                    Password = password,

                };
                request.Content = new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.SendAsync(request);


                string result = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == System.Net.HttpStatusCode.OK)

                {

                    Console.WriteLine("YEEEEEEEEEEEEEEEEEEEEEEs");

                    await App.Current.MainPage.DisplayAlert("Bravo", "Inscription bien reçue, bienvenue dans l'équipage !", "OK");
                    await Application.Current.MainPage.Navigation.PushAsync(new MainPage());


                }
                else
                {
                    Console.WriteLine("Nooooooooooooooooooooo");
                    await App.Current.MainPage.DisplayAlert("Alert", "Inscription échouée ! veuillez réessayer", "OK");
                }
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Alert", "Veuillez remplir tous champs", "OK");
            }

        }

       

        public async Task<PlaceItem> getComments(int id)

        {
            ApiClient apiClient = new ApiClient();
            HttpResponseMessage reponse = await apiClient.Execute(HttpMethod.Get, URL + "places/"+ Convert.ToString(id) , null, null);
            Response<PlaceItem> c = await apiClient.ReadFromResponse<Response<PlaceItem>>(reponse);
            //string result = await reponse.Content.ReadAsStringAsync();
            //Response<List<PlaceItem>> c = JsonConvert.DeserializeObject<Response<List<PlaceItem>>>(result);
            //Console.WriteLine(c);
            return c.Data;
        }

        public async Task commenter(string commentaire, int id)
        {
            if (!commentaire.Equals(""))
            {
                CreateCommentRequest content = new CreateCommentRequest
                {
                    Text = commentaire,


                };
                

                ApiClient apiClient = new ApiClient();
                HttpResponseMessage response = await apiClient.Execute(HttpMethod.Post, URL + "places/" + Convert.ToString(id) + "/comments", content, loginViewModel.infoLogin.AccessToken);
               
                string result = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == System.Net.HttpStatusCode.OK)

                {

                    Console.WriteLine("YEEEEEEEEEEEEEEEEEEEEEEs");
                    await App.Current.MainPage.DisplayAlert("Alert", "Commentaire ajouté !", "OK");
                    await Application.Current.MainPage.Navigation.PushAsync(new commentView());


                }
                else
                {
                    Console.WriteLine("Nooooooooooooooooooooo");
                    Console.WriteLine(response.StatusCode);
                    await App.Current.MainPage.DisplayAlert("Alert", "Erreur ", "OK");
                    Console.WriteLine("Nooooooooooooooooooooo");
                }
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Alert", "Veuillez remplir le champs", "OK");
            }
            
        }

        public  async Task<UserItem> getMonProfils()
        {
            ApiClient apiClient = new ApiClient();
             HttpResponseMessage reponse = await apiClient.Execute(HttpMethod.Get, URL + "me", null, loginViewModel.infoLogin.AccessToken);
            //HttpResponseMessage reponse = await apiClient.Execute(HttpMethod.Get, URL + "places", null, null);
            Response<UserItem> c = await apiClient.ReadFromResponse<Response<UserItem>>(reponse);


            return c.Data;
        }

        public async Task modifierPassword(string old, string nv)
        {

            if (!old.Equals("") && !nv.Equals(""))
            {
                UpdatePasswordRequest content = new UpdatePasswordRequest
                {
                    OldPassword = old,
                    NewPassword = nv,


                };



                ApiClient apiClient = new ApiClient();

                var method = new HttpMethod("PATCH");

                HttpResponseMessage response = await apiClient.Execute(method, URL + "me/password", content, loginViewModel.infoLogin.AccessToken);


                if (response.StatusCode == System.Net.HttpStatusCode.OK)

                {

                    Console.WriteLine("YEEEEEEEEEEEEEEEEEEEEEEs");
                    await App.Current.MainPage.DisplayAlert("Alert", "Mot de passe modifié  ", "OK");
                    await Application.Current.MainPage.Navigation.PushAsync(new monProfilsView());


                }
                else
                {
                    Console.WriteLine("Nooooooooooooooooooooo");
                    Console.WriteLine(response.StatusCode);
                    await App.Current.MainPage.DisplayAlert("Alert", "Erreur ", "OK");
                    Console.WriteLine("Nooooooooooooooooooooo");
                }
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Alert", "Veuillez remplir tous les champs ! ", "OK");
            }
                
        }

        public async Task modifierProfils(string firstname, string lastname, string id)
        {
            if (!firstname.Equals("") && !lastname.Equals("")&& !id.Equals(""))
            {
                UpdateProfileRequest content = new UpdateProfileRequest
                {
                    FirstName = firstname,
                    LastName = lastname,
                    ImageId = int.Parse(id),


                };



                ApiClient apiClient = new ApiClient();

                var method = new HttpMethod("PATCH");

                HttpResponseMessage response = await apiClient.Execute(method, URL + "me", content, loginViewModel.infoLogin.AccessToken);


                if (response.StatusCode == System.Net.HttpStatusCode.OK)

                {

                    Console.WriteLine("YEEEEEEEEEEEEEEEEEEEEEEs");
                    await App.Current.MainPage.DisplayAlert("Alert", "Profile modifié  ", "OK");
                    await Application.Current.MainPage.Navigation.PushAsync(new monProfilsView());


                }
                else
                {
                    Console.WriteLine("Nooooooooooooooooooooo");
                    Console.WriteLine(response.StatusCode);
                    await App.Current.MainPage.DisplayAlert("Alert", "Erreur ", "OK");
                    Console.WriteLine("Nooooooooooooooooooooo");
                }
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Alert", "Veuillez remplir tous les champs ! ", "OK");
            }
        }

        
    }

}
