using DTO;
using Microsoft.Extensions.DependencyInjection;
using System.Text;
using System.Text.RegularExpressions;

namespace Web.Services
{


    public class TpvService
    {
        public DTO.Integracions.Redsys.Tpv.Ids Id { get; set; } = DTO.Integracions.Redsys.Tpv.Ids.MatiasMasso;

        public const string managementUrl = "https://sis-t.redsys.es:25443/canales/";
        public DTO.Integracions.Redsys.Common.Environments Environment { get; set; } = DTO.Integracions.Redsys.Common.Environments.Production;
        public TpvService(DTO.Integracions.Redsys.Tpv.Ids tpvId)
        {
            Id = tpvId;
        }

        public DTO.Integracions.Redsys.Tpv Tpv(LangDTO lang)
        {
            return new DTO.Integracions.Redsys.Tpv(this.Id, this.Environment, lang);
        }

        async Task Submit(LangDTO lang, decimal? eur, string? concept, UserModel user)
        {
            //var tpv = Tpv(lang);
            //if (eur == null || eur == 0) throw new Exception("empty amount not allowed");
            //if (string.IsNullOrEmpty(concept)) throw new Exception("empty concept not allowed");

            //var tpvBookRequest = tpv!.BookRequest(user, concept, (decimal)eur);
            //try
            //{
            //    var tpvOrderNum = await appstate.GetAsync<List<NominaModel>>("Nominas") ?? new();

            //}
            //catch (Exception ex)
            //{

            //}

            //var tpvResponse = await PostAsync<DTO.Integracions.Redsys.TpvLog, string>(tpvBookRequest, "Redsys/BookRequest");
            //if (tpvResponse.Success())
            //{
            //    basket.TpvOrderNum = tpvResponse.Value!;

            //    var apiResponse = await PostAsync<ShoppingBasketModel, bool>(basket, "ShoppingBasket");
            //    if (apiResponse.Success())
            //    {
            //        var request = tpv!.Request(basket.TpvOrderNum, basket.OrderNum!, basket.Cash(), basket.Lang.ToString());
            //        formData = request.FormData();
            //    }
            //    else
            //    {
            //        problemDetails = apiResponse.ProblemDetails;
            //        isSubmitting = false;
            //    }
            //}
            //else
            //{
            //    problemDetails = tpvResponse.ProblemDetails;
            //    isSubmitting = false;
            //}

        }
    }
}

