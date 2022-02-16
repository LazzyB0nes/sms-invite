using System;
using Microsoft.EntityFrameworkCore;
using sms_invite.Models;

namespace sms_invite
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Invite> Invites { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            _ = modelBuilder.Entity<Invite>( e => _ = e.HasKey(x => x.Id));
        }
    }


}
