using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionePraticheApiDonini.DTOs.Output
{
    public class GottenPraticaDTO
    {
        public int PraticaId { get; set; }
        public DateTime BirthDate { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string CurrentStatus { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? StartedProcessingDate { get; set; }
        public DateTime? CompletedDate { get; set; }
    }
}