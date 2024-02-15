using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GestionePraticheApiDonini.Requests
{
    public class CreatePraticaRequest
    {
        [Required]
        public DateTime BirthDate { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public IFormFile Attachment { get; set; }
    }
}