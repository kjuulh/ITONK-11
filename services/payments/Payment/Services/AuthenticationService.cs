using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Payment.Database;
using Payment.Models;
using Payment.Providers;
using Payment.Repositories;
using Payment.ViewModels;

namespace Payment.Services {
    public class PaymentService : IPaymentService {
        private readonly IUsersService _usersService;
        private readonly IOptions<AppSettings> _appSettings;

        public PaymentService (IUsersService usersService, IOptions<AppSettings> appSettings) {
            _usersService = usersService;
            _appSettings = appSettings;
        }
    }
}