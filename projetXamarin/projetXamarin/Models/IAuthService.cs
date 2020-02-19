using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using TD.Api.Dtos;

namespace projetXamarin.Models
{
    interface IAuthService
    {
        Task<LoginResult> Auth(string login, string password);

        Task<List<PlaceItemSummary>> getListLieux();
        
        Task<byte[]> getImage(int id);

        Task<PlaceItem> getComments(int id) ;

        Task ajouterPlace(string title, string description,string idImage,string latitude,string longitude);

        Task register(string email, string firstname, string lastname, string password);

        Task commenter(string commentaire, int id);

        Task<UserItem> getMonProfils();

        Task modifierPassword(string old, string nv);

        Task modifierProfils(string firstname, string lastname,string id);


    }
}
