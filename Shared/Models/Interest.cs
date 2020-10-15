using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Shared.Models
{

    // Details pagina weet of user geinterreseerd is...
    // 
    // Lijst te maken valt van mensen met intresses.............
    public class Interest
    {

        public int Id { get; set; }
        public Animal Animal { get; set; }
        public string Email { get; set; }
    }
}
