using Microsoft.AspNetCore.Mvc;
using Dapper;
using DocumentVerificationAPI.Data;
using System.Data;
using System.IO;
using System.Diagnostics;

namespace DocumentVerificationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class DocumentsController : ControllerBase
    {
        private readonly IDbConnection _dbConnection;
        private readonly ILogger<DocumentsController> _logger;

        // Constructor to inject DB connection (Dapper)
        public DocumentsController(IDbConnection dbConnection, ILogger<DocumentsController> logger)
        {
            _dbConnection = dbConnection;
            _logger = logger;
        }

        // POST: /api/documents
        [HttpPost]
        public IActionResult UploadDocument([FromForm] IFormFile file, [FromForm] string title)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded.");

            if (file.Length > 10 * 1024 * 1024) // Limiting file size to 10MB
            {
                return BadRequest("File size exceeds the maximum limit of 10MB.");
            }

            // Generate a unique verification code
            var verificationCode = Guid.NewGuid().ToString();

            // Save the file to the server
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "uploads", file.FileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(fileStream);
            }

            // Save document details to the database using Dapper
            var document = new
            {
                UserId = 1, // This would come from the authenticated user
                Title = title,
                FilePath = filePath,
                VerificationCode = verificationCode,
                Status = "Pending",
                CreatedAt = DateTime.Now
            };

            var sql = @"
                INSERT INTO Documents (UserId, Title, FilePath, VerificationCode, Status, CreatedAt)
                VALUES (@UserId, @Title, @FilePath, @VerificationCode, @Status, @CreatedAt);
            ";

            // Use transaction to ensure atomic operations
            using (var transaction = _dbConnection.BeginTransaction())
            {
                try
                {
                    var stopwatch = Stopwatch.StartNew();
                    _dbConnection.Execute(sql, document, transaction);
                    stopwatch.Stop();
                    _logger.LogInformation($"Dapper INSERT query took: {stopwatch.ElapsedMilliseconds} ms");

                    // Commit the transaction
                    transaction.Commit();

                    return Ok(new { VerificationCode = verificationCode });
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    _logger.LogError($"Error occurred while saving the document: {ex.Message}");
                    return StatusCode(500, "An error occurred while saving the document.");
                }
            }
        }

        // GET: /api/documents/{id}
        [HttpGet("{id}")]
        public IActionResult GetDocument(int id)
        {
            var sql = "SELECT * FROM Documents WHERE Id = @Id";
            var stopwatch = Stopwatch.StartNew();
            var document = _dbConnection.QueryFirstOrDefault(sql, new { Id = id });
            stopwatch.Stop();
            _logger.LogInformation($"Dapper SELECT query took: {stopwatch.ElapsedMilliseconds} ms");

            if (document == null)
                return NotFound("Document not found.");

            return Ok(document);
        }

        // POST: /api/verify
        [HttpPost("verify")]
        public IActionResult VerifyDocument([FromBody] VerifyDocumentRequest request)
        {
            var sql = "SELECT * FROM Documents WHERE VerificationCode = @VerificationCode";
            var stopwatch = Stopwatch.StartNew();
            var document = _dbConnection.QueryFirstOrDefault(sql, new { VerificationCode = request.VerificationCode });
            stopwatch.Stop();
            _logger.LogInformation($"Dapper SELECT query took: {stopwatch.ElapsedMilliseconds} ms");

            if (document == null)
                return BadRequest("Invalid verification code.");

            // Update the document status to "Verified"
            var updateSql = "UPDATE Documents SET Status = 'Verified' WHERE Id = @Id";
            _dbConnection.Execute(updateSql, new { Id = document.Id });

            return Ok(new { Message = "Document verified successfully." });
        }
    }

    // Request model for document verification
    public class VerifyDocumentRequest
    {
        public required string VerificationCode { get; set; }
    }
}



