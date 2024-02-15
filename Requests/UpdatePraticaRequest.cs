using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionePraticheApiDonini.Requests
{
    public class UpdatePraticaRequest
    {
        public DateTime? BirthDate { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public byte[] Attachment { get; set; }
    }
}