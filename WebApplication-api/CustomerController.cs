using Microsoft.AspNetCore.Mvc;
using WebApplication_api; // Ensure this namespace is used
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace WebApplication_api.Controllers
{
    public class CustomerController : Controller
    {
        private readonly HttpClient _httpClient;

        public CustomerController(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _httpClient.BaseAddress = new Uri("https://localhost:44358/api/");
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            IEnumerable<Customer> customers = Enumerable.Empty<Customer>();

            try
            {
                var response = await _httpClient.GetStringAsync("customer");

                if (!string.IsNullOrEmpty(response))
                {
                    var deserializedCustomers = JsonSerializer.Deserialize<List<Customer>>(response, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
                    if (deserializedCustomers != null)
                    {
                        customers = deserializedCustomers;
                    }
                    else
                    {
                        return BadRequest("Failed to deserialize data from server.");
                    }
                }
                else
                {
                    return NoContent(); // No data available
                }
            }
            catch (HttpRequestException ex)
            {
                return StatusCode(500, $"Request error: {ex.Message}");
            }
            catch (JsonException ex)
            {
                return StatusCode(500, $"Deserialization error: {ex.Message}");
            }

            return Json(customers); // Return JSON response
        }
    }
}
