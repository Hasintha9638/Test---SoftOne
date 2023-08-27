using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace web_api.Models
{
    public class Students
    {
         public int Id { get; set; }
         public string? First_Name { get; set; }
         public string? Last_Name { get; set; } 
         public string? Email { get; set; } 
         public string? Phone { get; set; }
         public string? NIC { get; set; }
         public DateTime? DOB { get; set; }
         public string? Address { get; set; }
         public string? Profile_URL { get; set; }

    }
}