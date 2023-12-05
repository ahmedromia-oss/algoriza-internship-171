using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain
{
    public class PatientTime
    {
        public string patientId { get; set; }
        
        public int status { get; set; }
        public string timeId { get; set; }
        public virtual Patient patient { get; set; }
        public virtual Time time { get; set; }
        
    }
}
