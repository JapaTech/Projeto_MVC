﻿using EstudoMVC.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace EstudoMVC.DataContent
{
    public class MVC_DbContext : IdentityDbContext<User>
    {
        public MVC_DbContext(DbContextOptions<MVC_DbContext> options) : base(options)
        {

        }
         
        public DbSet<Review> Reviews { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<TouristAttraction> TouristAttractions { get; set; }
    }
}

