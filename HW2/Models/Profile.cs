namespace HW2.Models
{
    public class Profile
    {

        public int Id { get; set; } = 0;
        public string Username { get; set; } = String.Empty;
        public string Password { get; set; } = String.Empty;
        public string Email { get; set; } = String.Empty;
        public string Bio { get; set; } = String.Empty;
        public DateTime Joined { get; set; } = DateTime.UtcNow;
        public bool Farmer { get; set; } = false; //Would I set this in a different way? So that it's unassigned?
        public bool Forager { get; set; } = false;
        public List<string> Inventory { get; set; } = new List<string>();
        //I was thinking of making this List<Produce> (and adding a Produce model) but this might be totally wrong for how you want to do this.
        //Also, is it okay if this is here but doesn't get used in the constructor and is just added to the object later?

        public Profile() { }
        public Profile(string username, string password, string email, string? bio, DateTime dt, bool farmer, bool forager) //Can be a farmer and forager, right?
        {
            this.Username = username;
            this.Password = password;
            this.Joined = dt;
            this.Email = email;
            this.Farmer = farmer;
            this.Forager = forager;
            if (bio == null) { bio = "Hi! I'm on GaiaShare :)"; }
            this.Bio = bio;
        }
    }

}
