using Components;
using Components.Services;
using DocumentFormat.OpenXml.Spreadsheet;
using DTO;
using DTO.Helpers;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace Shop4moms.Services
{
    public class ShoppingBasketService : IShoppingBasketService
    {

        private readonly ProtectedSessionStorage sessionStorage;

        public event EventHandler<MatEventArgs<int>>? ItemsCountChanged;


        public ShoppingBasketService(ProtectedSessionStorage sessionStorage)
        {
            this.sessionStorage = sessionStorage;
        }

        public async Task<ShoppingBasketModel> GetBasketAsync()
        {
            var storageResult = await sessionStorage.GetAsync<ShoppingBasketModel?>("Basket");
            return storageResult.Value ?? new ShoppingBasketModel();
        }

        public async Task SaveBasketAsync(ShoppingBasketModel basket)
        {
            await sessionStorage.SetAsync("Basket", basket);
        }

        public async Task ClearBasketAsync()
        {
            await sessionStorage.SetAsync("Basket", "");
        }

        public async Task<ShoppingBasketModel.Item?> GetItemAsync(ProductSkuModel sku)
        {
            var basket = await GetBasketAsync();
            var retval = basket.Items.FirstOrDefault(x => x.Sku == sku.Guid);
            return retval;
        }

        public async Task<ShoppingBasketModel.Item?> GetItemOrNewAsync(ProductSkuModel sku)
        {
            var basket = await GetBasketAsync();
            var item = basket.Items.FirstOrDefault(x => x.Sku == sku.Guid);
            if (item == null)
            {
                item = new ShoppingBasketModel.Item() { Sku = sku.Guid, Qty = 0 };
            }
            return item;
        }


        public async Task<ShoppingBasketModel.Item> UpdateItemAsync(ProductSkuModel sku, int qty = 1, decimal? price = null, decimal? dto = null)
        {
            var basket = await GetBasketAsync();
            var item = basket.Items.FirstOrDefault(x => x.Sku == sku.Guid);
            if (item == null)
            {
                item = new ShoppingBasketModel.Item() { Sku = sku.Guid };
                basket.Items.Add(item);
            }
            else
            {
                item.Qty = qty;
                if (price != null) item.Price = price;
                if (dto != null) item.Dto = dto;
            }

            if (qty <= 0)
                await RemoveItemAsync(item);
            else
                await SaveBasketAsync(basket);

            ItemsCountChanged?.Invoke(this, new MatEventArgs<int>(basket.Items.Count));
            return item;
        }


        public async Task<ShoppingBasketModel.Item> AddToBasketAsync(ProductSkuModel sku, int qty = 1, decimal? price = null, decimal? dto = null)
        {
            var basket = await GetBasketAsync();
            var item = basket.Items.FirstOrDefault(x => x.Sku == sku.Guid);
            if (item == null)
            {
                item = new ShoppingBasketModel.Item() { Sku = sku.Guid };
                basket.Items.Add(item);
            }

            if (qty <= 0)
                await RemoveItemAsync(item);
            else
            {
                item.Qty = qty;
                item.Price = price;
                item.Dto = dto;
                await SaveBasketAsync(basket);
                ItemsCountChanged?.Invoke(this, new MatEventArgs<int>(basket.Items.Count));
            }
            return item;
        }

        public async Task IncrementBasketItemAsync(ShoppingBasketModel.Item item)
        {
            var basket = await GetBasketAsync();
            var itm = basket.Items.FirstOrDefault(x => x.Sku == item.Sku);
            if (itm != null)
            {
                itm.Qty += 1;
                await SaveBasketAsync(basket);
                ItemsCountChanged?.Invoke(this, new MatEventArgs<int>(basket.Items.Count));
            }
        }

        public async Task DecrementBasketItemAsync(ShoppingBasketModel.Item item)
        {
            var basket = await GetBasketAsync();
            var itm = basket.Items.FirstOrDefault(x => x.Sku == item.Sku);
            if (itm != null)
            {
                itm.Qty -= 1;
                if (itm.Qty <= 0) basket.Items.Remove(itm);
                await SaveBasketAsync(basket);
                ItemsCountChanged?.Invoke(this, new MatEventArgs<int>(basket.Items.Count));
            }
        }

        public async Task RemoveItemAsync(ShoppingBasketModel.Item item)
        {
            var basket = await GetBasketAsync();
            basket.Items.RemoveAll(x => x.Sku == item.Sku);
            await SaveBasketAsync(basket);
            ItemsCountChanged?.Invoke(this, new MatEventArgs<int>(basket.Items.Count));
        }
    }
}
