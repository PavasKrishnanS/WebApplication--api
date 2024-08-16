//using System.Collections.Generic;
//using System.Net.Http;
//using System.Net.Http.Json;
//using System.Threading.Tasks;
//using WebApplication2.Models;

//public class CustomerService
//{
//    private readonly HttpClient _httpClient;

//    public CustomerService(HttpClient httpClient)
//    {
//        _httpClient = httpClient;
//        _httpClient.BaseAddress = new Uri("https://localhost:44358/api/customer");
//    }

//    public async Task<IEnumerable<Customer>?> GetAllCustomersAsync()
//    {
//        var customers = await _httpClient.GetFromJsonAsync<IEnumerable<Customer>>("customers");
//        return customers ?? Enumerable.Empty<Customer>(); // Return an empty collection if null
//    }


//    public async Task<Customer?> GetCustomerByIdAsync(int id)
//    {
//        var customer = await _httpClient.GetFromJsonAsync<Customer>($"customers/{id}");
//        return customer; // Return null if no customer is found
//    }

//}



