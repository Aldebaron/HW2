namespace HW2.Models
{
    public class Message
    {
        // Indexes needed on From and To.
        // Maybe Created At -- for ordering by


        public int Id { get; set; } = 0;
        public string From { get; set; } = String.Empty;
        public string To { get; set; } = String.Empty;
        // What is the max size here? Think MySQL database.
        public string Body { get; set; } = String.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public Message() { }
        public Message(string to, string from, string body, DateTime dt)
        {
            this.From = from;
            this.To = to;
            this.Body = body;
            // Allow this to be set for now. Final implementation may not allow time to be set by the caller.
            this.CreatedAt = dt;
        }
    }

}
