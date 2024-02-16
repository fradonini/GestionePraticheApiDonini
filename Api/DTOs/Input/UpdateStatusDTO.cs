using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionePraticheApiDonini.DTOs.Input
{
    public class UpdateStatusDTO
    {
        public int PraticaId { get; set; }
        public NewStatusDTO NewStatus { get; set; }
    }


    public enum NewStatusDTO
    {
        Processing = 1,
        Successful,
        Unsuccessful
    }
}