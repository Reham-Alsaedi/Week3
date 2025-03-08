namespace DocumentVerificationAPI.Data
{
    public class Document
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public required string Title { get; set; }
        public required string FilePath { get; set; }
        public required string VerificationCode { get; set; }
        public required string Status { get; set; }
        public DateTime CreatedAt { get; set; }

        public User User { get; set; } = null!;  // Use null! to indicate this won't be null
        public ICollection<VerificationLog> VerificationLogs { get; set; } = new List<VerificationLog>();  // Initialize as empty list
    }
}
