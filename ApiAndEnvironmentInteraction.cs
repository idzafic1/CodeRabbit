using System;
using System.Threading.Tasks;

// This using statement creates a potential for name shadowing.
// A linter should flag this as confusing.
using File = System.IO.File;

namespace MyProject
{
    // ERROR: Name Shadowing. This class 'File' has the same name as 'System.IO.File'.
    // This can lead to confusion and bugs if a developer uses the wrong one by mistake.
    public class File
    {
        public string Content { get; set; }
    }

    public class EnvironmentIssues
    {
        public void ProcessFile()
        {
            // FIX: The compiler error is resolved by using the full, unambiguous name for the method.
            // The "name shadowing" issue is still present for a tool to detect, but the code is now valid.
            var content = System.IO.File.ReadAllText("data.txt");
        }

        // ERROR: Time zone issue. Using 'DateTime.Now' records the server's local time.
        // This causes major problems for applications used across different time zones.
        public void RecordPurchase()
        {
            var purchaseRecord = new
            {
                Timestamp = DateTime.Now, // This should be DateTime.UtcNow
                Amount = 99.99
            };
            Console.WriteLine($"Purchase recorded at: {purchaseRecord.Timestamp}");
        }
    }

    // This class represents a simple API controller.
    // ERROR: Lack of API Versioning. This API has no versioning strategy.
    // If we change the 'Submit' method (e.g., change the parameter type), all clients will break.
    // A robust API would use a versioning scheme (e.g., "/api/v1/data").
    public class ApiController
    {
        public async Task<string> Submit(string data)
        {
            // In a real API, breaking changes here are dangerous without versioning.
            return await Task.FromResult("Data processed successfully");
        }
    }
}