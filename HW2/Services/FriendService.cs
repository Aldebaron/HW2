using HW2.Models;
using System.Diagnostics;

namespace HW2.Services
{

    public class FriendService
    {
        public static int FriendshipId = 998;
        private static List<Friendship> Friends = new List<Friendship>(); // Database of all messages

        public FriendService()
        {

            Friends = new List<Friendship> { };

            
        Befriend("EJ", "Al", new DateTime(2023, 5, 1, 8, 30, 52));
        Befriend("EJ", "Cat", new DateTime(2020, 5, 1, 8, 30, 52));
        Befriend("Cat", "Al", new DateTime(2019, 5, 1, 8, 30, 52));
        Befriend("Jim", "Al", DateTime.UtcNow);
        Befriend("John", "Joe", DateTime.UtcNow);
           
        }
        public List<Friendship> GetAll() => Friends;

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
        public List<Friendship> ListFriends()=> Friends;
       

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
