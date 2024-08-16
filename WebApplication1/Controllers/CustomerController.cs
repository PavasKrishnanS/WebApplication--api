using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace WebApplication1.Controllers
{
    public class CustomerController : Controller
    {
        private readonly HttpClient _httpClient;


        public CustomerController(HttpClient httpClient)
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






        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CustomerViewModel customer)
        {
            if (!ModelState.IsValid)
            {
                return View(customer);
            }

            try
            {
                var response = await _httpClient.PostAsJsonAsync("customer", customer);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Error creating customer.");
                }
            }
            catch (HttpRequestException ex)
            {
                ModelState.AddModelError(string.Empty, $"Request error: {ex.Message}");
            }
            catch (JsonException ex)
            {
                ModelState.AddModelError(string.Empty, $"Deserialization error: {ex.Message}");
            }

            return View(customer);
        }































        // POST: /Customer/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _httpClient.DeleteAsync($"customer/{id}");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError(string.Empty, "Error deleting customer.");
            return RedirectToAction(nameof(Index));
        }


















        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var response = await _httpClient.GetStringAsync($"customer/{id}");
            if (string.IsNullOrEmpty(response))
            {
                return NotFound();
            }

            var customer = JsonSerializer.Deserialize<CustomerViewModel>(response, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CustomerViewModel customer)
        {
            if (!ModelState.IsValid)
            {
                return View(customer);
            }

            var response = await _httpClient.PutAsJsonAsync($"customer/{customer.Id}", customer);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError(string.Empty, "Error updating customer.");
            return View(customer);
        }

    }



}












   

