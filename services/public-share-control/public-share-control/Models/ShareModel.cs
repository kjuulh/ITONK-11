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
    public Guid Id { get; set; }
    public Guid ShareType { get; set; }
    public int Count { get; set; }
  }
}
