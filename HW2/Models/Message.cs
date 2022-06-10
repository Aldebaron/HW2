namespace HW2.Models
{
    public class Message
    {
        // Indexes needed on From and To.
        // Maybe Created At -- for ordering by

        
        public int Id { get; set; }
        public string From { get; set; }
        public string To { get; set; }

        // What is the max size here? Think MySQL database.
        public string Body { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        // UpdatedAt (not necessary-- once created cannot be modified)


        public Message(string to, string from, string body)
        {
            this.From = from;
            this.To = to;
            this.Body = body;
            
        }
    }

}
