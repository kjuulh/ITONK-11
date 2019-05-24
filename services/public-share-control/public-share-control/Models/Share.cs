using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PublicShareControl.Models
{
    public class Share
    {
        public Guid ShareId { get; set; }

        public int Count { get; set; }

        public Guid PortfolioId { get; set; }
        public Portfolio Portfolio { get; set; }
    }
}