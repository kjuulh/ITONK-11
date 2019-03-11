using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace PSO_Control_Service.Models
{
    public class UserModel
    {
        [Key]
        public int UserId { get; set; }
        public string Username { get; set; }
        [DataType(DataType.Password)]
        public string UserPassword { get; set; }
        public List<ShareModel> shares { get; set; }       
    }
}
