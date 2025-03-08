namespace DocumentVerificationAPI.Data
{
    public class VerificationLog
    {
        public int Id { get; set; }
        public int DocumentId { get; set; }
        public required string VerifiedBy { get; set; }
        public DateTime Timestamp { get; set; }
        public required string Status { get; set; }

        public Document Document { get; set; } = null!;  // Use null! to indicate this won't be null
    }
}
