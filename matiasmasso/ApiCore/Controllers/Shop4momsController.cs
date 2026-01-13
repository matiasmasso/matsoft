using Microsoft.AspNetCore.Mvc;
using DTO;
using Api.Services;
using Api.Extensions;
using Api.Services.Integracions;
using Api.Shared;
using Microsoft.AspNetCore.OutputCaching;
using Azure;
using iText.StyledXmlParser.Jsoup.Helper;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Shop4momsController : ControllerBase
    {

        IOutputCacheStore cache;

        public Shop4momsController(IOutputCacheStore cache)
        {
            this.cache = cache;
        }

        [HttpPost("fromTpvResponse")]
        //4moms.es & 4moms.pt call this endpoint
        //when a successful respose is received from tpv
        //meaning that bank successfully validated consumer payment
        public async Task<IActionResult> FromTpvResponse([FromBody] DTO.Integracions.Redsys.TpvLog tpvLog)
        {
            try
            {
                var basket = Shop4momsService.LogResponse(tpvLog);
                await cache.Clear(OutputCacheExtensions.Tags.TpvLogs);
                await cache.Clear(OutputCacheExtensions.Tags.Baskets);
                return Ok(basket);
            }
            catch (Exception ex) { return BadRequest(ex.ProblemDetails()); }
        }

        [HttpGet("test")]
        //4moms.es & 4moms.pt call this endpoint
        //when a successful respose is received from tpv
        //meaning that bank successfully validated consumer payment
        public async Task<IActionResult> Test()
        {
            try
            {
                var dsOrder = "9999A0006872";
                var basket = ShoppingBasketService.FromTpvOrder(dsOrder);
                var tpv = new DTO.Integracions.Redsys.Tpv(DTO.Integracions.Redsys.Tpv.Ids.Shop4moms, DTO.Integracions.Redsys.Common.Environments.Production, basket.Lang);
                var request = tpv!.Request(basket.TpvOrderNum, basket.OrderNum!, basket.Cash(), basket.Lang.ToString());
                //formData = request.FormData();
                var paramsDictionary = DTO.Helpers.CryptoHelper.FromUrlFriendlyBase64Json(request.Ds_MerchantParameters);
                var tpvLog = new DTO.Integracions.Redsys.TpvLog();
                if (paramsDictionary != null)
                {
                    tpvLog.Ds_Date = $"{basket.Fch:dd/MM/yyyy}";
                    tpvLog.Ds_Hour = $"{basket.Fch:HH:mm}";
                    tpvLog.Ds_Amount = $"{basket.Amount * 100:N0}";
                    tpvLog.Ds_Currency = "978";
                    tpvLog.Ds_Order = dsOrder;
                    tpvLog.Ds_MerchantCode = "357592922";
                    tpvLog.Ds_Terminal = 1;
                    //tpvLog.Ds_Signature = paramsDictionary["Ds_Signature"];
                    //if (paramsDictionary.ContainsKey("Ds_Response"))
                    //    tpvLog.Ds_Response = paramsDictionary["Ds_Response"];
                    //if (paramsDictionary.ContainsKey("Ds_ErrorCode"))
                    //    tpvLog.Ds_ErrorCode = paramsDictionary["Ds_ErrorCode"];
                    //if (paramsDictionary.ContainsKey("Ds_MerchantData"))
                    //    tpvLog.Ds_MerchantData = paramsDictionary["Ds_MerchantData"];
                    //tpvLog.Ds_ProductDescription = paramsDictionary["Ds_ProductDescription"];
                    tpvLog.Ds_SecurePayment = "1";
                    tpvLog.Ds_TransactionType = "0";
                    tpvLog.Ds_Card_Country = "724";
                    tpvLog.Ds_AuthorisationCode = "888888";
                    //if (paramsDictionary.ContainsKey("Ds_ConsumerLanguage"))
                    //    tpvLog.Ds_ConsumerLanguage ="001";
                    //if (paramsDictionary.ContainsKey("Ds_Card_Brand"))
                    //    tpvLog.Ds_Card_Type = paramsDictionary["Ds_Card_Brand"];
                    //if (paramsDictionary.ContainsKey("Ds_ProcessedPayMethod"))
                    //    tpvLog.Ds_ProcessedPayMethod = paramsDictionary["Ds_ProcessedPayMethod"];

                    // response:
                    tpvLog.Ds_MerchantParameters = request.Ds_MerchantParameters;
                    tpvLog.Ds_SignatureReceived = request.Ds_Signature;
                    tpvLog.FchCreated = DateTime.Now;

                };
                var basket2 = Shop4momsService.LogResponse(tpvLog);


                //var response = new DTO.Integracions.Redsys.Request
                //{
                //    Ds_MerchantParameters = basket. Request.Form?["Ds_MerchantParameters"],
                //    Ds_SignatureVersion = Request.Form?["Ds_SignatureVersion"],
                //    Ds_Signature = Request.Form?["Ds_Signature"]
                //};

                //DTO.Integracions.Redsys.TpvLog tpvLog;
                //await cache.Clear(OutputCacheExtensions.Tags.TpvLogs);
                //await cache.Clear(OutputCacheExtensions.Tags.Baskets);
                return Ok(basket2);
            }
            catch (Exception ex) { return BadRequest(ex.ProblemDetails()); }
        }



        [HttpGet("Categories")]
        [OutputCache(PolicyName = "ProductCategories")]
        public IActionResult Categories()
        {
            try { return Ok(Shop4momsService.Categories()); }
            catch (Exception ex) { return BadRequest(ex.ProblemDetails()); }
        }

        [HttpGet("Skus")]
        [OutputCache(PolicyName = "ProductSkus")]
        public IActionResult Skus()
        {
            try { return Ok(Shop4momsService.Skus()); }
            catch (Exception ex) { return BadRequest(ex.ProblemDetails()); }
        }


        [HttpGet("SkuStocks")]
        [OutputCache(PolicyName = "SkuStocks")]
        public IActionResult SkuStocks()
        {
            try { return Ok(Shop4momsService.SkuStocks()); }
            catch (Exception ex) { return BadRequest(ex.ProblemDetails()); }
        }


        [HttpGet("SkuRetails")]
        [OutputCache(PolicyName = "SkuRetails")]
        public IActionResult SkuRetails()
        {
            try { return Ok(Shop4momsService.SkuRetails()); }
            catch (Exception ex) { return BadRequest(ex.ProblemDetails()); }
        }


        [HttpGet("Routes")]
        [OutputCache(PolicyName = "Routes")]
        public IActionResult Routes()
        {
            try { return Ok(Shop4momsService.Routes()); }
            catch (Exception ex) { return BadRequest(ex.ProblemDetails()); }
        }


        [HttpGet("ProductPlugins")]
        [OutputCache(PolicyName = "ProductPlugins")]
        public IActionResult ProductPlugins()
        {
            try { return Ok(Shop4momsService.ProductPlugins()); }
            catch (Exception ex) { return BadRequest(ex.ProblemDetails()); }
        }

        [HttpGet("Videos")]
        [OutputCache(PolicyName = "Videos")]
        public IActionResult Videos()
        {
            var brand = ProductBrandModel.Wellknown(ProductBrandModel.Wellknowns.fourMoms)!;
            try { return Ok(YouTubeMoviesService.BrandVideos(brand)); }
            catch (Exception ex) { return BadRequest(ex.ProblemDetails()); }
        }

        [HttpGet("MediaResources")]
        [OutputCache(PolicyName = "MediaResources")]
        public IActionResult MediaResources()
        {
            var brand = ProductBrandModel.Wellknown(ProductBrandModel.Wellknowns.fourMoms)!;
            try { return Ok(MediaResourcesService.FromBrand(brand)); }
            catch (Exception ex) { return BadRequest(ex.ProblemDetails()); }
        }

        [HttpGet("Reset")]
        public async Task<IActionResult> Reset()
        {
            try { return Ok(await cache.Clear()); }
            catch (Exception ex) { return BadRequest(ex.ProblemDetails()); }
        }


        #region Deprecated


        [HttpGet]
        public IActionResult Fetch()
        {
            IActionResult retval;
            try
            {
                var value = Shop4momsService.Fetch();
                retval = Ok(value);
            }
            catch (Exception ex)
            {
                retval = BadRequest(ex.ProblemDetails());
            }
            return retval;
        }

        [HttpGet("content/{guid}")]
        public IActionResult Content(Guid guid)
        {
            IActionResult retval;
            try
            {
                var value = Shop4momsService.Content(guid);
                retval = Ok(value);
            }
            catch (Exception ex)
            {
                retval = BadRequest(ex.ProblemDetails());
            }
            return retval;
        }

        [HttpPost]
        public IActionResult SaveBasket([FromBody] ShoppingBasketModel basket)
        {
            IActionResult retval;
            try
            {
                Shop4momsService.SaveBasket(basket);
                retval = Ok(true);
            }
            catch (Exception ex)
            {
                retval = BadRequest(ex.ProblemDetails());
            }
            return retval;
        }


        [HttpPost("content")]
        public IActionResult ContentFromSegment([FromBody] string segment)
        {
            IActionResult retval;
            try
            {
                var value = Shop4momsService.Content(segment);
                retval = Ok(value);
            }
            catch (Exception ex)
            {
                retval = BadRequest(ex.ProblemDetails());
            }
            return retval;
        }

        [HttpGet("basket/{tpvOrder}")]
        public IActionResult BasketFromTpvOrder(string tpvOrder)
        {
            IActionResult retval;
            try
            {
                //recover persisted basket from Tpv order number and expand user 
                var value = ShoppingBasketService.FromTpvOrder(tpvOrder)!;
                retval = Ok(value);
            }
            catch (Exception ex)
            {
                retval = BadRequest(ex.ProblemDetails());
            }
            return retval;
        }

        #endregion
    }
}
