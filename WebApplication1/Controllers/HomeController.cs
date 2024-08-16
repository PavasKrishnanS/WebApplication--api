using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly HttpClient _httpClient;

        public HomeController(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _httpClient.BaseAddress = new Uri("https://localhost:44358/api/customer");
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            IEnumerable<CustomerViewModel> customers = Enumerable.Empty<CustomerViewModel>();

            try
            {
                var response = await _httpClient.GetStringAsync("customer");

                if (!string.IsNullOrEmpty(response))
                {
                    var deserializedCustomers = JsonSerializer.Deserialize<List<CustomerViewModel>>(response, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                    if (deserializedCustomers != null)
                    {
                        customers = deserializedCustomers;
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Failed to deserialize data from server.");
                        return View(customers);
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "No data received from the server.");
                    return View(customers);
                }
            }
            catch (HttpRequestException ex)
            {
                ModelState.AddModelError(string.Empty, $"Request error: {ex.Message}");
                return View(customers);
            }
            catch (JsonException ex)
            {
                ModelState.AddModelError(string.Empty, $"Deserialization error: {ex.Message}");
                return View(customers);
            }

            return View(customers);
        }
    }
}
