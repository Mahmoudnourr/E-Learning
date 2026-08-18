using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Abstractions.Persistence;
using Domain.Entities;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {

        public ApplicationDbContext( DbContextOptions<ApplicationDbContext> options) : base(options){ }
        public DbSet<User> Users => Set<User>();
  
        protected override void OnModelCreating( ModelBuilder builder) { base.OnModelCreating(builder);
        builder.Entity<ApplicationUser>()
    .HasOne(x => x.DomainUser)
    .WithOne()
    .HasForeignKey<ApplicationUser>(
        x => x.DomainUserId)
    .OnDelete(DeleteBehavior.Cascade);

        builder.ApplyConfigurationsFromAssembly(
            typeof(ApplicationDbContext).Assembly); }
      
    }
}