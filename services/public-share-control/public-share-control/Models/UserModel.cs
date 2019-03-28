using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
namespace PublicShareControl.Models
{
  public class UserModel
  {
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }

    [DataType(DataType.Password)]
    public string Password { get; set; }
    public List<ShareModel> Shares { get; set; }
  }
}