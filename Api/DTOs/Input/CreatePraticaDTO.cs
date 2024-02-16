using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionePraticheApiDonini.DTOs.Input
{
    public class CreatePraticaDTO
    {
        public DateTime BirthDate { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public IFormFile Attachment { get; set; }
    }
}