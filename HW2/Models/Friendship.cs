namespace HW2.Models
{
    public class Friendship
    {
        //Using "Friendship" instead of "Friend" is a little longer and more annoying, but I thought it was much clearer, because saying "Friend"
        //made it seem like we were talking more about the actual user's profiles and less about the connection between them.


        public int Id { get; set; } = 0;
        public string User { get; set; } = String.Empty;
        public string Other { get; set; } = String.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public Friendship() { }
        public Friendship(string user, string other, DateTime dt)
        {
            this.User = user;
            this.Other = other;
            this.CreatedAt = dt;
        }
    }

}
