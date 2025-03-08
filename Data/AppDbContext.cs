using Microsoft.EntityFrameworkCore;
using DocumentVerificationAPI.Data; // Ensure this line is present

namespace DocumentVerificationAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<VerificationLog> VerificationLogs { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(u => u.Documents)
                .WithOne(d => d.User)
                .HasForeignKey(d => d.UserId);

            modelBuilder.Entity<Document>()
                .HasMany(d => d.VerificationLogs)
                .WithOne(v => v.Document)
                .HasForeignKey(v => v.DocumentId);
        }
        public static void SeedData(AppDbContext context)
    {
        if (!context.Users.Any())
        {
            var user1 = new User
            {
                Name = "John Doe",
                Email = "johndoe@example.com",
                Password = "password123", // In a real-world scenario, use hashed passwords
                Role = "Admin"
            };
            var user2 = new User
            {
                Name = "Jane Smith",
                Email = "janesmith@example.com",
                Password = "password456", // In a real-world scenario, use hashed passwords
                Role = "User"
            };

            context.Users.AddRange(user1, user2);
            context.SaveChanges();

            // Create sample documents for each user
            var doc1 = new Document
            {
                UserId = user1.Id,
                Title = "Passport",
                FilePath = "/documents/johndoe/passport.pdf",
                VerificationCode = "DOC123",
                Status = "Pending",
                CreatedAt = DateTime.Now
            };

            var doc2 = new Document
            {
                UserId = user2.Id,
                Title = "ID Card",
                FilePath = "/documents/janesmith/idcard.pdf",
                VerificationCode = "DOC124",
                Status = "Verified",
                CreatedAt = DateTime.Now
            };

            context.Documents.AddRange(doc1, doc2);
            context.SaveChanges();
        }
    }}}
