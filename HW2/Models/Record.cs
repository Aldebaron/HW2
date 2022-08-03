namespace HW2.Models
{
    public class Record
    {
        
        public int Id { get; set; } = 0;
        public string User { get; set; } = String.Empty;
        public string Item { get; set; } = String.Empty;
        public int Quantity { get; set; } = 0;
        //Should this be null or empty in a different way?
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public Record() { }
        public Record(string user, string item, int quantity, DateTime dt)
        {
            this.User = user;
            this.Item = item;
            this.Quantity = quantity;
            this.CreatedAt = dt;
        }
    }

}
