using HW2.Models;
using System.Diagnostics;

namespace HW2.Services
{

    public class FriendService
    {
        public static int FriendshipId = 998;
        public static int ProfileId = 998;
        private static List<Friendship> Friends = new List<Friendship>();
        private static List<Profile> Profiles = new List<Profile>();

        public FriendService()
        {

            Friends = new List<Friendship> { };
            Profiles = new List<Profile> { };


            Befriend("EJ", "Al", new DateTime(2023, 5, 1, 8, 30, 52));
            Befriend("EJ", "Cat", new DateTime(2020, 5, 1, 8, 30, 52));
            Befriend("Cat", "Al", new DateTime(2019, 5, 1, 8, 30, 52));
            Befriend("Jim", "Al", DateTime.UtcNow);
            Befriend("John", "Joe", DateTime.UtcNow);

            NewProfile("Al", "1", "1", "al@aol.com", null, DateTime.UtcNow, true, false);
            NewProfile("Joe", "2", "2", "joe@aol.com", null, DateTime.UtcNow, true, true);
            NewProfile("John", "3", "3", "john@aol.com", null, DateTime.UtcNow, true, false);
            NewProfile("Jim", "4", "4", "jim@aol.com", null, DateTime.UtcNow, false, true);
            NewProfile("EJ", "5", "5", "ej@aol.com", null, DateTime.UtcNow, true, true);

        }
        public List<Friendship> GetAllFriendships() => Friends;

        public List<Profile> GetAllProfiles() => Profiles;

        //My understanding of what you mean by "friending" someone is closer to "following" them in that if Person A follows Person B, it's not automatically
        //reciprocal, and Person B must then follow Person A. The alternative would be that A sends B a friend request, and once it's approved they're mutually friends.
        //If we went with the second version, it would also make sense to limit Messages so that they can only be sent to friends. However, this may not be super practical
        //for foragers wanting to ask farmers quick questions, etc.

        public string Befriend(string user, string other, DateTime dt)
        {
            if (dt == new DateTime()) dt = DateTime.UtcNow;
            var test = new DateTime(); // See what the value is. 


            bool fstatus = CheckFriendship(user, other);
            if (fstatus == true) { return "You are already friends!"; }
            var friend = new Friendship(user, other, dt);
            FriendshipId += 2;
            friend.Id = FriendshipId;


            Friends.Add(friend);
            return friend.ToString();
            
        }

        public string NewProfile(string username, string password, string password2, string email, string? bio, DateTime dt, bool forager, bool farmer)
        {
            if (dt == new DateTime()) dt = DateTime.UtcNow;
            var test = new DateTime(); // See what the value is.
            if (password != password2) { return "Your reentered password does not match"; }
            if (FindUser(username) != 0) { return "Please select a different username, that one is already in use."; }
            if (forager == false && farmer == false) { return "You must be either a forager or a farmer. Otherwise, what are you doing here?"; }

            //Should we check validity of email address?

            var profile = new Profile(username, password, email, bio, dt, forager, farmer);
            ProfileId += 2;
            profile.Id = ProfileId;
            Profiles.Add(profile);
            return profile.ToString();

        }

        public int FindUser(string user) {
            
            for (int i = 0; i < Profiles.Count - 1; i++)
            {
                if (Profiles[i].Username == user) { return i; break; }
            }
            return 0;
        }

        //Allows a user to update theis inventory
        public string UpdateInventory(string username, string produce)
        {
            int j = FindUser(username);
            if (j == 0) { return "User not found"; }

            if (Profiles[j].Farmer == false) { return "You must be a farmer to have an inventory!"; }
            Profiles[j].Inventory.Add(produce);
            //I assume that the end goal is for produce in the inventory to be more than just string, but I don't know what it will be in the long run so I'm using string as a placeholder.
            return "Updated";

        }

        //Allows a user to update their bio
        public string UpdateBio(string user, string bio) {

            int i = FindUser(user);
            if (i != 0) { Profiles[i].Bio = bio; return "Updated"; }
            return "User not found"; //The Authenticate function should prevent this from ever happening

        }

        //Checks if a user has already friended someone
        public bool CheckFriendship(string user, string other)
        {
            bool fstatus = false;
            for (int i = 0; i < Friends.Count - 1; i++)
            {
                if ((Friends[i].User == user || Friends[i].User == other) && (Friends[i].Other == user || Friends[i].Other == other))
                {
                    fstatus = true;
                    return fstatus;
                    //if it hit this return, the function would end, right?
                }

            }

            return fstatus;
        }


        public List<Friendship> AllFriends(string user) {
            
            var friendships = new List<Friendship>();
           

            var t = new Friendship();
            //temporary container to switch order of list around

            for (int i = 0; i < Friends.Count - 1; i++)
            {
                if (Friends[i].User == user) { friendships.Add(Friends[i]); }


            }
            //Collects all of a user's friendships


            for (int i = 0; i < (friendships.Count - 1); i++)
            {

                if (friendships[i].CreatedAt < friendships[i + 1].CreatedAt)
                {
                    t = friendships[i];
                    friendships[i] = friendships[i + 1];
                    friendships[i + 1] = t;
                    if (i > 0) { i -= 2; };
                }

            }
            //organizes friendships by most recent

            return friendships;
        }

        //Removes a friendship
        public string Unfriend(string user, string other)
        {
            for (int i = 0; i < Friends.Count - 1; i++)
            {
                if (Friends[i].User == user && Friends[i].Other == other)
                {
                    Friends.Remove(Friends[i]);
                    return "Unfriended.";
                }

            }

            return "No friendship found";
        }

    }

}
