using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PSO_Control_Service.Models
{
    public class ShareModel
    {
        [Key]
        public int ShareId { get; set; }
        public string ShareName { get; set; }
        public int ShareCount { get; set; }
        public int ShareValue { get; set; }

    }
}
