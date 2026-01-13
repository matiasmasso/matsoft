using DTO;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Formatting;
using System.Net.Http.Json;
using System.Text;

namespace Spa.ViewModels
{
    public class ProductLandingPageViewModel
    {
        private Shared.AppState AppState;
        private string Catchall;
        public ProductLandingPageDTO? Model;
        private string Url;

        public Globals.LoadStatusEnum LoadStatus = Globals.LoadStatusEnum.Empty;
        public string? Title { get; set; }
        public string? Content { get; set; }

        public delegate void StateChange();
        public event StateChange? OnStateChange;


        public ProductLandingPageViewModel(Shared.AppState appstate, string catchall)
        {
            AppState = appstate;
            Catchall = catchall;
        }

        public async void Fetch(HttpClient http)
        {
            LoadStatus = Globals.LoadStatusEnum.Loading;
            AppState.ProblemDetails = null;
            try
            {
                var url = AppState.ApiUrl("ProductLandingPage");
               
                var dto = Request(Catchall);
                var response = await http.PostAsync(url, dto, new JsonMediaTypeFormatter());
                if (response.IsSuccessStatusCode)
                {
                    var responseText = await response.Content.ReadAsStringAsync();
                    Model = Newtonsoft.Json.JsonConvert.DeserializeObject<ProductLandingPageDTO>(responseText);
                    Model.Url = dto.UrlSegment;
                    Model.Lang = dto.Lang!;
                    Content = Model.Content;
                    var contextMenuItems = ContextMenuItems();
                    AppState.SetPublicMenuCustomItems(contextMenuItems);
                    LoadStatus = Globals.LoadStatusEnum.Loaded;
                    OnStateChange?.Invoke();
                }
                else
                {
                    AppState.ProblemDetails = await response.Content.ReadFromJsonAsync<ProblemDetails>() ?? new ProblemDetails();
                    LoadStatus = Globals.LoadStatusEnum.Failed;
                    OnStateChange?.Invoke();
                }
            }
            catch (Exception ex)
            {
                AppState.ProblemDetails = new ProblemDetails
                {
                    Title = "Error on content download",
                    Detail = ex.Message
                };
                LoadStatus = Globals.LoadStatusEnum.Failed;
            }

        }

        private ContentRequestDTO Request(string catchall)
        {
            var retval = new ContentRequestDTO();
            var urlSegment = catchall.Replace(".html", "");
            var firstSegment = urlSegment.Split("/").First();
            var langSegments = new string[]{ "es","ca","en","pt"};
            if(langSegments.Contains(firstSegment))
            {
                retval.Lang = LangDTO.FromCultureInfo(firstSegment);
                retval.UrlSegment = urlSegment.Substring(urlSegment.IndexOf('/') + 1);
            } else
            {
                retval.Lang = LangDTO.Default();
                retval.UrlSegment = urlSegment;
            }
            return retval;
        }

        private List<MenuItemModel> ContextMenuItems()
        {
            var retval = new List<MenuItemModel>();
            if(Model?.Product != null)
            {
                if (Model.Product.Src == ProductModel.SourceCods.Brand)
                    retval = MenuItems(new ProductModel.Tabs[] { ProductModel.Tabs.distribuidores, ProductModel.Tabs.galeria, ProductModel.Tabs.descargas, ProductModel.Tabs.videos, ProductModel.Tabs.bloggerposts });
                if (Model.Product.Src == ProductModel.SourceCods.Dept)
                    retval = MenuItems(new ProductModel.Tabs[] { ProductModel.Tabs.distribuidores, ProductModel.Tabs.galeria, ProductModel.Tabs.descargas, ProductModel.Tabs.videos, ProductModel.Tabs.bloggerposts });
                if (Model.Product.Src == ProductModel.SourceCods.Category)
                    retval = MenuItems(new ProductModel.Tabs[] { ProductModel.Tabs.distribuidores, ProductModel.Tabs.coleccion, ProductModel.Tabs.galeria, ProductModel.Tabs.descargas, ProductModel.Tabs.videos, ProductModel.Tabs.bloggerposts });
                if (Model.Product.Src == ProductModel.SourceCods.Sku)
                    retval = MenuItems(new ProductModel.Tabs[] { ProductModel.Tabs.distribuidores, ProductModel.Tabs.descargas, ProductModel.Tabs.videos, ProductModel.Tabs.bloggerposts });
            }
            return retval;
        }

        private List<MenuItemModel> MenuItems(ProductModel.Tabs[] tabs) {
            var retval = new List<MenuItemModel>();
            foreach (var tab in tabs) {
                var menuItem = MenuItem(tab);
                retval.Add(menuItem);
            }
            //tabs.Select(x => MenuItem(x)).ToList();
            return retval;
        }

        private MenuItemModel MenuItem(ProductModel.Tabs tab)
        {
            LangTextModel caption ;
            switch(tab)
            {
                case ProductModel.Tabs.distribuidores:
                    caption = new LangTextModel("¿Donde comprar?", "On comprar?", "Where to buy?");
                    break;
                case ProductModel.Tabs.coleccion:
                    caption = new LangTextModel("Colección", "Col·lecció", "Collection");
                    break;
                case ProductModel.Tabs.galeria:
                    caption = new LangTextModel("Galería de imágenes", "Galeria de imatges", "Image gallery");
                    break;
                case ProductModel.Tabs.descargas:
                    caption = new LangTextModel("Descargas", "Descàrregues", "Downloads");
                    break;
                case ProductModel.Tabs.videos:
                    caption = new LangTextModel("Videos");
                    break;
                case ProductModel.Tabs.bloggerposts:
                    caption = new LangTextModel("Posts relacionados", "Pots relacionats", "Related posts");
                    break;
                default:
                    caption = new LangTextModel();
                    break;
            }
            return new MenuItemModel()
            {
                Caption = caption, 
                Action = string.Format("{0}/{1}",TablessUrl(),tab.ToString())
            };

        }
        private string TablessUrl()
        {
            var retval = Model.Url;
            var lastSegment = retval.Substring(retval.LastIndexOf('/') + 1);
            if (Enum.IsDefined(typeof(ProductModel.Tabs), lastSegment))
                retval = retval.Substring(0, retval.LastIndexOf('/'));
            return retval;
        }

    }
}
