using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CallbackReceiverApi
{
    public class UpdatedPraticaStatusEvent
    {
        public int PraticaId { get; set; }
        public UpdatedStatus UpdatedStatus { get; set; }
        public DateTime UpdatedDate { get; set; }
    }

    public enum UpdatedStatus
    {
        Processing = 1,
        Successful,
        Unsuccessful
    }
}