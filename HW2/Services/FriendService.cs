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
        private static List<Record> Records = new List<Record>();

        public FriendService()
        {

            Friends = new List<Friendship> { };
            Profiles = new List<Profile> { };
            Records = new List<Record> { };

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

            NewRecord("John", "Tomatoes", 3, new DateTime(2022, 2, 3));
            NewRecord("John", "Cilantro", 3, new DateTime(2022, 2, 2));
            NewRecord("John", "Eggs", 3, new DateTime(2022, 2, 1));
            NewRecord("John", "Oranges", 3, new DateTime(2022, 1, 30));
            NewRecord("John", "Potatoes", 3, new DateTime(2022, 1, 2));
            NewRecord("John", "Milk", 3, new DateTime(2022, 1, 2));
            NewRecord("John", "Manure", 3, new DateTime(2022, 1, 1));
            NewRecord("Steve", "Seedlings", 3, new DateTime(2022, 2, 3));
            NewRecord("Steve", "Rutabagas", 3, new DateTime(2022, 2, 1));
            NewRecord("Steve", "Jicama", 3, new DateTime(2022, 1, 30));
            NewRecord("Steve", "Zucchini", 3, new DateTime(2022, 1, 10));
            NewRecord("Steve", "Chilis", 3, new DateTime(2022, 1, 9));
            NewRecord("Steve", "Squash", 3, new DateTime(2022, 1, 8));
            NewRecord("Steve", "Apples", 3, new DateTime(2022, 1, 2));
            NewRecord("Steve", "Pears", 3, new DateTime(2022, 1, 2));
            NewRecord("Steve", "Strawberries", 3, new DateTime(2022, 1, 1));

        }
        public List<Friendship> GetAllFriendships() => Friends;

        public List<Profile> GetAllProfiles() => Profiles;

        public List<Record> GetAllRecords() => Records;

        public string Befriend(string user, string other, DateTime dt)
        {
            if (dt == new DateTime()) dt = DateTime.UtcNow;


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
            if (password != password2) { return "Your reentered password does not match"; }
            if (FindUser(username) != -1) { return "Please select a different username, that one is already in use."; }
            if (forager == false && farmer == false) { return "You must be either a forager or a farmer. Otherwise, what are you doing here?"; }

            //Should we check validity of email address?

            var profile = new Profile(username, password, email, bio, dt, forager, farmer);
            ProfileId += 2;
            profile.Id = ProfileId;
            Profiles.Add(profile);
            return profile.ToString();

        }

        public Profile ViewProfile(string username) {
            int i = FindUser(username);
            var profile = Profiles[i];
            return profile;

        }
        //provides the index of a user's profile within Profiles, return -1 if not found
        public int FindUser(string user) {
            
            for (int i = 0; i < Profiles.Count; i++)
            {
                if (Profiles[i].Username == user) { return i; }
            }
            return -1;
        }

        //Allows a user to update theis inventory
        public string UpdateInventory(string username, string produce)
        {
            int j = FindUser(username);
            if (j == -1) { return "User not found"; }

            if (Profiles[j].Farmer == false) { return "You must be a farmer to have an inventory!"; }
            Profiles[j].Inventory.Add(produce);
            //I assume that the end goal is for produce in the inventory to be more than just string, but I don't know what it will be in the long run so I'm using string as a placeholder.
            return "Updated";

        }

        //Allows a user to update their bio
        public string UpdateBio(string user, string bio) {

            int i = FindUser(user);
            if (i != -1) { Profiles[i].Bio = bio; return "Updated"; }
            return "User not found"; //The Authenticate function should prevent this from ever happening

        }

        //Checks if a user has already friended someone
        public bool CheckFriendship(string user, string other)
        {
            bool fstatus = false;
            for (int i = 0; i < Friends.Count; i++)
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

            for (int i = 0; i < Friends.Count; i++)
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

        //Remove a friendship
        public string Unfriend(string user, string other)
        {
            for (int i = 0; i < Friends.Count; i++)
            {
                if (Friends[i].User == user && Friends[i].Other == other)
                {
                    Friends.Remove(Friends[i]);
                    return "Unfriended.";
                }

            }

            return "No friendship found";
        }

        //create a new record
        public Record NewRecord(string user, string item, int quantity, DateTime dt) {

            if (dt == new DateTime()) dt = DateTime.UtcNow;
            var record = new Record(user, item, quantity, dt);

            Records.Add(record);
            return record;
        }

        //get all of a user's record
        public List<Record> GetRecords(string user)
        {
            var r = new List<Record> { };

            for (int i = 0; i < Records.Count; i++)
            {
                if (Records[i].User == user) { r.Add(Records[i]); }
            }


            return r;
        }


        //Display a portion of a user's records depending on the number indicated and the length of time between each record
        public List<Record> DisplayRecords(string user, int number) {
            var r = new List<Record> { };
            var allr = GetRecords(user);
            Record t;

            //organize all of a user's records by date
            for (int i = 0; i < allr.Count; i++)
            {
                if (allr[i].CreatedAt < allr[i + 1].CreatedAt)
                {
                    t = allr[i];
                    allr[i] = allr[i + 1];
                    allr[i + 1] = t;
                    if (i > 0) { i -= 2; };
                }
            }

            //return all of a user's record if the total is less that twice the number of record requested
            if (allr.Count < (number * 2)) { return allr; }

            //collect the number of records requested
            for (int i = 0; i < number; i++)
            {
                r.Add(allr[i]);
            }

            //collect extra records if they were created within less than three days of the last record collected in the above loop
            for (int i = number - 1; i < allr.Count - 1; i++)
            {
                if (allr[i + 1].CreatedAt < allr[i].CreatedAt.AddDays(3))
                { r.Add(allr[i + 1]); }
                else { break; }
            }


            return r;
        }

    }

}
