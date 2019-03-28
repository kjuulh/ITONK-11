using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Authentication.Controllers {
    [Route ("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase {
        private readonly AuthenticationContext _context;

        public UsersController (AuthenticationContext context) {
            _context = context;
        }
    }
}