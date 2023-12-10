using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain
{
    public class PatientTime
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public string Id { get; set; }
        public string patientId { get; set; }
        
        public int status { get; set; }
        public string timeId { get; set; }
        public virtual Patient patient { get; set; }
        public virtual Time time { get; set; }

        public virtual Discount discount { get; set; }
        public string DiscountId { get; set; }
        public double finalPrice { get; set; }
        
    }
}
