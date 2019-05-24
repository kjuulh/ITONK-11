using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PublicShareControl.Models
{
    public class PortfolioModel
    {
        [Key] public Guid Id { get; set; }

        public Guid Owner { get; set; }
        public List<ShareModel> Shares { get; set; }
    }
}