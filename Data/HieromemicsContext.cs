using Microsoft.EntityFrameworkCore;
using Hieromemics.Models;


namespace Hieromemics.Data {
    public class HieromemicsContext : DbContext {
        public HieromemicsContext (DbContextOptions<HieromemicsContext> options) : base (options) {}
        public DbSet <users> users {get; set;}
        public DbSet <SavedPics> SavedPics {get; set;}
        public DbSet <templates> templates {get; set;}
        public DbSet <pictures> pictures {get; set;}
        public DbSet <messages> messages {get; set;}
        public DbSet <friendList> friendList {get; set;}
        public DbSet<Hieromemics.Models.pendingMatch> pendingMatch { get; set; }
    }
}