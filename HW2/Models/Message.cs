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
        //How would we limit size? Would that be on the front end, so that you just couldn't type out any more characters
        //after a certain limit, or would we make it so that SendMessage rejects it if it's over the character limit? ~A
        public string Body { get; set; } = String.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public Message() { }
        //Why do we want to allow null messages to exist? ~A
        public Message(string to, string from, string body, DateTime dt)
        {
            this.From = from;
            this.To = to;
            this.Body = body;
            // Allow this to be set for now. Final implementation may not allow time to be set by the caller.
            //If it wasn't set by the caller, would this just be this.CreatedAt = DateTime.UtcNow?
            //And why would we allow it to be set? ~A
            this.CreatedAt = dt;
        }
    }

}
