using Message.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Message.Infrastructure.Persistence
{
    public class MessageDBContext:DbContext
    {
        public MessageDBContext(DbContextOptions<MessageDBContext> options) : base(options) { }
        protected override async void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        #region DB Set
        public DbSet<UserMailLog> userMailLogs { get; set; }
        #endregion
    }
}
