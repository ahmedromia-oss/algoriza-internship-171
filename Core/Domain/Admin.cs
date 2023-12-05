using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain
{
    public class Admin
    {
        public string userId { get; set; }

       
        public virtual User user { get; set; }
    }
}
