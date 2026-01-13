using DTO;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;

namespace Shop4moms
{
    public class SessionState : Components.SessionState
    {

        public ShoppingBasketModel Basket { get; set; }
        public DateTime FchCreated { get; set; } = DateTime.Now;


        public SessionState(HttpClient http):base(http) 
        {
            Guid = Guid.NewGuid();
            Basket = new ShoppingBasketModel()
            {
                Fch = DateTime.Now,
                MarketPlace = MarketPlaceModel.Wellknown(MarketPlaceModel.Wellknowns.Shop4moms),
                //Country = lang.IsPOR() ? DTO.CountryModel.Wellknown(CountryModel.Wellknowns.Portugal)!.Guid : DTO.CountryModel.Wellknown(CountryModel.Wellknowns.Spain)!.Guid
            };
        }



    }
}
