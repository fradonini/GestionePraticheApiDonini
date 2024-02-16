using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GestionePraticheApiDonini.Data
{
    public class Pratica
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public byte[] Attachment { get; set; }
        public DateTime BirthDate { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public PraticaStatus Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? StartedProcessingDate { get; set; } = null;
        public DateTime? CompletedDate { get; set; } = null;
    }

    public enum PraticaStatus
    {
        Created,
        Processing,
        Successful,
        Unsuccessful
    }
}