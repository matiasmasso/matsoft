using DocumentFormat.OpenXml.Bibliography;
using DTO;
using System.Runtime.Versioning;
using Web.LocalComponents;

namespace Web.Services
{
    public class DeliveriesService:IDisposable
    {
        public List<DeliveryModel>? Values { get; set; }
        public List<int>? Years { get; set; }
        public DbState State { get; set; } = DbState.StandBy;

        public event Action<Exception?>? OnChange;
        private AppStateService appstate;
        private AtlasService atlasService;
        private ProductsService productsService;

        public DeliveriesService(AppStateService appstate, AtlasService atlasService, ProductsService productsService)
        {
            this.appstate = appstate;
            this.atlasService = atlasService;
            this.productsService = productsService;

            atlasService.OnChange += NotifyChange;
            productsService.OnChange += NotifyChange;
        }

        public async Task FetchAsync(EmpModel.EmpIds emp, int? year = null)
        {
            if (State != DbState.IsLoading)
            {
                State = DbState.IsLoading;
                try
                {
                    if (year == null)
                    {
                        Tuple<List<int>, List<DeliveryModel>>? tuple = await appstate.GetAsync<Tuple<List<int>, List<DeliveryModel>>?>("Deliveries", ((int)emp).ToString());
                        Years = tuple?.Item1;
                        Values = tuple?.Item2;
                    }
                    else
                        Values = await appstate.GetAsync<List<DeliveryModel>>("Deliveries", ((int)emp).ToString(), year.ToString()!);

                    State = DbState.StandBy;
                    OnChange?.Invoke(null);
                }
                catch (Exception ex)
                {
                    State = DbState.StandBy;
                    OnChange?.Invoke(ex);
                }
            }
        }

        public async Task<DeliveryModel?> FindAsync(Guid guid)
        {
           return await appstate.GetAsync<DeliveryModel>("Delivery", guid.ToString());
        }

        public DeliveryModel? GetValue(Guid? guid) => guid == null ? null : Values?.FirstOrDefault(x => x.Guid == guid);

        public async Task<bool> UpdateAsync(DeliveryModel value)
        {
            bool retval = false;
            if (value != null)
            {
                retval = await appstate.PostAsync<DeliveryModel, bool>(value, "Delivery");
                if (retval)
                {
                    if (value.IsNew) Values?.Add(value);
                    value.IsNew = false;
                }
            }
            return retval;
        }


        public async Task<List<DeliveryModel>> UpdateAsync(List<DeliveryModel> values)
        {
            List<DeliveryModel> retval = new();
            if (values != null && values.Count != 0)
            {
                retval = await appstate.PostAsync<List<DeliveryModel>, List<DeliveryModel>>(values, "Deliveries");
                //if (retval)
                //{
                //    if (value.IsNew) Values?.Add(value);
                //    value.IsNew = false;
                //}
            }
            return retval;
        }


        public async Task<bool> DeleteAsync(DeliveryModel? value)
        {
            bool retval = false;
            if (value != null)
            {
                retval = await appstate.GetAsync<bool>("Delivery/delete", value.Guid.ToString());
                if (retval) Values?.Remove(value);
            }
            return retval;

        }

        public int Bultos(DeliveryModel.Item item)
        {
            int retval;
            int innerPack = productsService.InnerPackOrInherited(item.Sku);
            if (innerPack <= 0)
                retval = 1;
            else if (item.Qty <= innerPack)
                retval = 1;
            else if (item.Qty % innerPack == 0)
                retval = (item?.Qty ?? 0) / innerPack;
            else
                retval = item?.Qty ?? 0 / innerPack + 1;
            return retval;
        }

        public string Destination(DeliveryModel? item)
        {
            var locationNom = LocationNom(item);
            locationNom = string.IsNullOrEmpty(locationNom) ? string.Empty : "(" + locationNom + ")";
            return $"{item?.Nom} {locationNom}".Trim();
        }

        public string? LocationNom(DeliveryModel? item)
        {
            var zipGuid = item?.Address?.ZipGuid == null ? item?.Address?.Zip?.Guid : item?.Address?.ZipGuid;
            var zip = atlasService.Zip(zipGuid);
            var retval = atlasService.LocationNom(zip?.Location);
            //TO DO: append provincia i pais LocationFullNom
            return retval;
        }



        #region Integracions

        public bool MayPrintLabels(DeliveryModel delivery)
        {
            var contact = atlasService.Contact(delivery.Contact?.Guid);
            var candidates = new List<CustomerModel.Wellknowns>() {
                CustomerModel.Wellknowns.sonae,
                CustomerModel.Wellknowns.wells,
                CustomerModel.Wellknowns.continente
                 };
            return candidates.Any(x =>
            CustomerModel.Wellknown(x)?.Guid == contact?.Guid || CustomerModel.Wellknown(x)?.Guid == contact?.Ccx
            );
        }

        public bool ShouldPrintSonaeLabels(DeliveryModel delivery)
        {
            var contact = atlasService.Contact(delivery.Contact?.Guid);
            var candidates = new List<CustomerModel.Wellknowns>() {
                CustomerModel.Wellknowns.sonae,
                CustomerModel.Wellknowns.wells,
                CustomerModel.Wellknowns.continente
                 };
            return candidates.Any(x =>
            CustomerModel.Wellknown(x)?.Guid == contact?.Guid || CustomerModel.Wellknown(x)?.Guid == contact?.Ccx
            );
        }


        [SupportedOSPlatform("windows")]
        public byte[]? ShippingLabels(DeliveryModel? delivery)
        {
            byte[]? retval = null;
            if(delivery != null)
            {
                if (ShouldPrintSonaeLabels(delivery))
                    retval = ShippingLabelsSonae(delivery);
            }
            return retval;
        }

        [SupportedOSPlatform("windows")]
        public byte[] ShippingLabelsSonae(DeliveryModel? delivery)
        {
            var lang = LangDTO.Por();
            var contact = atlasService.Contact(delivery?.Contact?.Guid);
            var labels = new List<Integracions.Sonae.ShippingLabel.Model>();
            foreach(var item in delivery?.Items ?? new())
            {
                var sku = productsService.Sku(item.Sku);
                var bultos = Bultos(item);
                var qty = item.Qty ?? 0;
                for(var bulto=1;bulto<=bultos; bulto++)
                {
                    var qtyxbulto = Math.Max(productsService.InnerPackOrInherited(item.Sku),1);

                    var madeinCountryGuid = productsService.MadeInOrInherited(sku);
                    var label = new Integracions.Sonae.ShippingLabel.Model();
                    label.ProveedorNum = contact?.SuProveedorNum;
                    label.PurchaseOrder = delivery?.PurchaseOrder(item)?.Concept;
                    label.Bultos = bultos;
                    label.Bulto = bulto;
                    label.Qty = Math.Min(qty, qtyxbulto);
                    label.SkuNom = sku?.NomLlarg?.Tradueix(lang);
                    label.SkuRefCustomer = productsService.CustomerSkuRef(contact?.Guid,sku?.Guid);
                    label.Ean13 = sku?.Ean;
                    label.MadeIn = atlasService.Country(madeinCountryGuid)?.ISO;

                    labels.Add(label);
                    qty -= qtyxbulto;
                }
            }
            return Integracions.Sonae.ShippingLabel.Pdf(labels);
        }

        #endregion


        void NotifyChange(Exception? ex = null)
        {
            OnChange?.Invoke(ex);
        }

        public void Dispose()
        {
            atlasService.OnChange -= NotifyChange;
            productsService.OnChange -= NotifyChange;
        }
    }
}


