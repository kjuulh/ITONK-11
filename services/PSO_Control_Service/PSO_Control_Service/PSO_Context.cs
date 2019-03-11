using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PSO_Control_Service.Models;

namespace PSO_Control_Service
{
    public class PSO_Context : DbContext
    {
        public PSO_Context(DbContextOptions<PSO_Context> options) : base(options)
        {
        }
        public DbSet<UserModel> users;
        public DbSet<ShareModel> shares;
        public DbSet<PSO_Control_Service.Models.UserModel> UserModel { get; set; }
    }
}
