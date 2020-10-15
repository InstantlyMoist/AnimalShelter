using Microsoft.AspNetCore.Mvc.Rendering;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Models
{
    public class AnimalIndexModel
    {

        public List<Animal> animalList { get; set; }

        public SelectList animalTypes { get; set; }
        public SelectList genders { get; set; }
        public SelectList canBeWithKidsList { get; set; }

        public string animalType { get; set; }
        public string gender { get; set; }
        public string canBeWithKids { get; set; }
    }
}
