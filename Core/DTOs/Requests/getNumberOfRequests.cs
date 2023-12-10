using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs.Requests
{
    public class getNumberOfRequests
    {
        public int CompletedRequest { get; set; }
        public int CancelledRequests { get; set; }
        public int PendingRequest { get; set; }
    }
}
