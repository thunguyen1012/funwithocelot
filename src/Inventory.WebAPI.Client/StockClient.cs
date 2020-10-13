using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Inventory.WebAPI.Client
{
    public class StockClient
    {
        private static readonly HttpClient client = new HttpClient();
        private static readonly string baseUrl = "http://localhost:5001";

        public async Task<bool> DecreaseProductQuantity(Guid productId, int quantity)
        {
            var decreaseProductQuantityDTO = new DecreaseProductQuantityDTO();
            decreaseProductQuantityDTO.ProductId = productId;
            decreaseProductQuantityDTO.Quantity = quantity;

            var json = JsonSerializer.Serialize(decreaseProductQuantityDTO);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PutAsync($"{baseUrl}/api/stock/", data);

            var result = bool.Parse(response.Content.ReadAsStringAsync().Result);
            return result;
        }
    }

    public class DecreaseProductQuantityDTO
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}