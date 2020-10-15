using Microsoft.AspNetCore.Http;
using Shared.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Shared.Models
{
    public class Animal
    {

        public int Id { get; set; }
        [Required(ErrorMessage = "Name required")]
        public string Name { get; set; }

        public string Image { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Birth date")]
        public DateTime BirthDate { get; set; }
        [Required(ErrorMessage = "Description required")]
        public string Description { get; set; }
        [Required(ErrorMessage = "AnimalType required")]
        [Display(Name = "Animal Type")]
        public AnimalType AnimalType { get; set; }
        public string Breed { get; set; }
        [Required(ErrorMessage = "Gender required")]
        public Gender Gender { get; set; }
        [DataType(DataType.Date)]
        [Required, DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [Display(Name = "Entry Date")]
        public DateTime EntryDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime AdoptionDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime DeathDate { get; set; }
        [Display(Name = "Helped their private parts")]
        [Required(ErrorMessage = "HelpedTheirPrivateParts required")]
        public bool HelpedTheirPrivateParts { get; set; }
        [Required(ErrorMessage = "CanBeWithKids required")]
        [Display(Name = "Can be with kids?")]
        public DeepBoolean CanBeWithKids { get; set; }
        public string Treatments { get; set; }
        [Required(ErrorMessage = "Reason required")]
        [Display(Name = "Reason for adoption")]
        public string Reason { get; set; }
        [Required(ErrorMessage = "Adoptable required")]
        public bool Adoptable { get; set; }
        public string AdoptedBy { get; set; }
        public ICollection<Remark> Remarks { get; set; }
    }
}
