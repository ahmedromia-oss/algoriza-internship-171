using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain
{
    public class Discount
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public string Id { get; set; }
        public int Type { get; set; }
        
        public string DiscountCode { get; set; }
        public double value { get; set; }
        public int NumOfRequests { get; set; }
        public virtual ICollection<Patient> Patients {  get; set; }

        public int status { get; set; }


    }
}
