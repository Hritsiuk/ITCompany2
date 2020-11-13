using System;
using System.Collections.Generic;
using ITCompany.Data.Entities;
using ITCompany.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ITCompany.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public DbSet<EventItem> EventItems { get; set; }
        public DbSet<EventUser> EventsUsers { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
