using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PublicShareControl.Models
{
    public class Portfolio
    {
        [Key] public Guid PortfolioId { get; set; }

        public Guid OwnerId { get; set; }
        public List<Share> Shares { get; set; }
    }
}