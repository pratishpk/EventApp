using EventManagement.Model;
using Microsoft.EntityFrameworkCore;

namespace EventManagement.Context
{
    public class EventDbContext : DbContext
    {
        public DbSet<UserInfo> Users { get; set; } 
        public DbSet<EventDetails> Events { get; set; }
        public DbSet<SpeakersDetails> Speakers { get; set; }
        public DbSet<SessionInfo> Sessions { get; set; }
        public DbSet<ParticipantEventDetails> ParticipantEvents { get; set; }

        public EventDbContext(DbContextOptions<EventDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Primary key for UserInfo
            modelBuilder.Entity<UserInfo>()
                .HasKey(u => u.EmailId);

            // ParticipantEventDetails - configure foreign keys and navigation
            modelBuilder.Entity<ParticipantEventDetails>()
                .HasOne(p => p.Participant)
                .WithMany() 
                .HasForeignKey(p => p.ParticipantEmailId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ParticipantEventDetails>()
                .HasOne(p => p.Event)
                .WithMany() 
                .HasForeignKey(p => p.EventId)
                .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);
        }
    }
}
