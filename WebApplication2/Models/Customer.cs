using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication2.Models
{
    [Table("customer")]
    public class Customer
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

   
        public string LastName { get; set; }

       
        public string Email { get; set; }

        public string Phone { get; set; }
    }
}
