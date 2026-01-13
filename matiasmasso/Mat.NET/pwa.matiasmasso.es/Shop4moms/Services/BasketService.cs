using DTO;
using DTO.Helpers;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace Shop4moms.Services
{
    public class BasketService
    {
        public event EventHandler<MatEventArgs<int>>? ItemsCountChanged;
        private readonly ProtectedSessionStorage _sessionStorage;
        private ShoppingBasketModel? _basket;

        public BasketService(ProtectedSessionStorage sessionStorage)
        {
            _sessionStorage = sessionStorage;
        }

        public async Task<ShoppingBasketModel> GetBasketAsync(LangDTO? lang = null)
        {
            if (_basket == null)
            {
                var result = await _sessionStorage.GetAsync<ShoppingBasketModel>("Basket");
                if (result.Success && result.Value != null)
                {
                    _basket = result.Value ?? new();
                    NotifyItemsCountChanged();
                }
                _basket ??= new();
            }

            if (lang != null) _basket.Lang = lang;
            _basket.Country ??= lang?.DefaultCountry()?.Guid;
            return _basket;
        }

        public async Task SaveBasketToSessionStorageAsync(LangDTO lang, UserModel? user = null)
        {
            if (_basket != null)
            {
                _basket.User = user;
                _basket.Lang = lang;
                try
                {
                    await _sessionStorage.SetAsync("Basket", _basket ?? new());
                }
                catch (Exception ex)
                {

                }
            }
        }

        public async Task<ShoppingBasketModel.Item?> GetItemAsync(ProductSkuModel? sku) => await GetItemAsync(sku?.Guid);

        public async Task<ShoppingBasketModel.Item?> GetItemAsync(Guid? skuGuid)
        {
            var basket = await GetBasketAsync();
            return basket.Items.FirstOrDefault(x => x.Sku == skuGuid);
        }

        public async Task<ShoppingBasketModel.Item> UpdateItemAsync(ProductSkuModel sku, int qty = 1, decimal? price = null, decimal? dto = null)
        {
            var item = await GetItemAsync(sku);
            if (item == null)
            {
                item = new ShoppingBasketModel.Item() { Sku = sku.Guid, Qty = qty, Price = price };
                _basket!.Items.Add(item);
            }
            else
            {
                item.Qty = qty;
                if (price != null) item.Price = price;
                if (dto != null) item.Dto = dto;
            }

            if (qty <= 0) await RemoveItemAsync(item);

            ItemsCountChanged?.Invoke(this, new MatEventArgs<int>(_basket!.Items.Count));
            return item;
        }

        public async Task RemoveItemAsync(ShoppingBasketModel.Item? item)
        {
            var basket = await GetBasketAsync();
            basket.Items.RemoveAll(x => x.Sku == item?.Sku);
            ItemsCountChanged?.Invoke(this, new MatEventArgs<int>(basket.Items.Count));
        }

        public async Task ClearBasketAsync()
        {
            var basket = await GetBasketAsync();
            basket.Items.Clear();
            NotifyItemsCountChanged();
        }

        public void SetBasket(ShoppingBasketModel? value)
        {
            _basket = value;
        }



        public void NotifyItemsCountChanged()
        {
            ItemsCountChanged?.Invoke(this, new MatEventArgs<int>(_basket!.Items.Count));
        }
    }
}
