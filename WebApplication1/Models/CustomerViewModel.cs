﻿using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class CustomerViewModel
    {

        public int Id { get; set; }


        public string FirstName { get; set; }



      
        public string LastName { get; set; }
        
        public string Email { get; set; }
       
        public string Phone { get; set; }

    }
}
