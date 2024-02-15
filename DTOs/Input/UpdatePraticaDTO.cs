using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionePraticheApiDonini.DTOs.Input
{
    public class UpdatePraticaDTO
    {
        public int PraticaId { get; set; }
        public DateTime? BirthDate { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public byte[] Attachment { get; set; }
    }
}