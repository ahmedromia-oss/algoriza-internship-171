using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain
{
    public class PatientDiscount
    {
        public virtual Patient Patient { get; set; }
        public virtual Discount Discount { get; set; }
        public string discountId { get; set; }
        public string PatientId { get; set; }
    }
}
