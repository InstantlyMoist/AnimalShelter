using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Shared.Models
{
    public class Remark
    {

        public int Id { get; set; }

        public int AnimalId { get; set; }

        public string Description { get; set; }

        [DataType(DataType.Date)]
        [Required, DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime EntryDate { get; set; }
        
        public string MadeBy { get; set; }
    }
}
