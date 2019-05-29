using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StockBuyer.Services;

namespace StockBuyer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuyerController : ControllerBase
    {
        private readonly IBuyerService _BuyerService;

        public BuyerController(IBuyerService BuyerService)
        {
            _BuyerService = BuyerService;
        }
    }
}