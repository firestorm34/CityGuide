using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CityGuide.Shared.Models
{
    public class City
    {

        [Required]
        public int Id { get; set; }
        public string? Name { get; set; }
        public int Population { get; set; }
        public DateTime FoundationDate { get; set; }
    }
}
