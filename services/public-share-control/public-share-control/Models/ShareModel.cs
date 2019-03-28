using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PublicShareControl.Models
{
  public class ShareModel
  {
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public int Count { get; set; }
    public int Value { get; set; }
    public UserModel Owner { get; set; }

  }
}
