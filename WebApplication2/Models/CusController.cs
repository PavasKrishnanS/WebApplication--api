//using Microsoft.AspNetCore.Mvc;

//using Newtonsoft.Json;
//using System.Collections.Generic;
//using System.Net.Http;
//using System.Net.Http.Headers;
//using System.Threading.Tasks;
//using WebApplication2.Models;

//namespace WebApplication2.Controllers
//{
//    public class CusController : Controller
//    {
//        private readonly HttpClient _httpClient;

//        public CusController(HttpClient httpClient)
//        {
//            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
//            _httpClient.BaseAddress = new Uri("https://localhost:44358/api/customer");
//        }

//        public async Task<IActionResult> Index()
//        {
//            IEnumerable<Customer> customers = Enumerable.Empty<Customer>();

//            try
//            {
//                var response = await _httpClient.GetStringAsync("customer");

//                if (!string.IsNullOrEmpty(response))
//                {
//                    var deserializedCustomers = JsonConvert.DeserializeObject<List<Customer>>(response);
//                    if (deserializedCustomers != null)
//                    {
//                        customers = deserializedCustomers;
//                    }
//                    else
//                    {
//                        ModelState.AddModelError(string.Empty, "Failed to deserialize data from server.");
//                    }
//                }
//                else
//                {
//                    ModelState.AddModelError(string.Empty, "No data received from the server.");
//                }
//            }
//            catch (HttpRequestException ex)
//            {
//                ModelState.AddModelError(string.Empty, $"Request error: {ex.Message}");
//            }
//            catch (JsonException ex)
//            {
//                ModelState.AddModelError(string.Empty, $"Deserialization error: {ex.Message}");
//            }

//            return View(customers);
//        }

//    }
//}





   
