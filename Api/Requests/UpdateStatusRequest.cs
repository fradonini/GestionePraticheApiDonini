using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GestionePraticheApiDonini.Requests
{
    public class UpdateStatusRequest
    {
        [Required]
        public NewStatus NewStatus { get; set; }
    }

    public enum NewStatus
    {
        Processing = 1,
        Successful,
        Unsuccessful
    }
}