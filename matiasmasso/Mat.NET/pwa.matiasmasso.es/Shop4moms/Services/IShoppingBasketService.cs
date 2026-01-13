using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop4moms.Services
{
    public interface IShoppingBasketService
    {
        public event EventHandler<DTO.Helpers.MatEventArgs<int>>? ItemsCountChanged;

        public Task<ShoppingBasketModel> GetBasketAsync();

        public Task ClearBasketAsync();

        public Task SaveBasketAsync(ShoppingBasketModel basket);

        public Task<ShoppingBasketModel.Item?> GetItemAsync(ProductSkuModel sku);

        public Task<ShoppingBasketModel.Item?> GetItemOrNewAsync(ProductSkuModel sku);

        public Task<ShoppingBasketModel.Item> UpdateItemAsync(ProductSkuModel sku, int qty = 1, decimal? price = null, decimal? dto = null);

        public Task RemoveItemAsync(ShoppingBasketModel.Item item);

        public Task IncrementBasketItemAsync(ShoppingBasketModel.Item item);
        public Task DecrementBasketItemAsync(ShoppingBasketModel.Item item);


    }
}
