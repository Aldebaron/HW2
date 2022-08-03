using HW2.Models;
using HW2.Services;
using Microsoft.AspNetCore.Mvc;

namespace HW2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FriendController : ControllerBase
    {


        private readonly FriendService _frndService;

        public FriendController (FriendService fs)
        {
            _frndService = fs;
        }

       
        [HttpGet]
        public ActionResult<List<Friendship>> GetAllFriendships() =>
            _frndService.GetAllFriendships();

        [HttpGet]
        public ActionResult<List<Record>> GetAllRecords() =>
            _frndService.GetAllRecords();

        [HttpGet]
        public ActionResult<List<Profile>> GetAllProfiles() =>
            _frndService.GetAllProfiles();


        /// <summary>
        /// How to 'add' a friendship.
        /// This should be a HTTP POST method.
        /// </summary>
        /// <param name="user">User ID doing the friending</param>
        /// <param name="other">User ID being friended</param>
        [HttpPost("Befriend")]
        public ActionResult Befriend(string user, string other)
        {
            if (Authenticate(user) == false) { return NoContent(); }
            var friend = _frndService.Befriend(user, other, DateTime.UtcNow);
            return Accepted(friend);
        }


        /// <summary>
        /// How to 'add' a profile.
        /// </summary>
        /// <param name="user">User ID doing the friending</param>
        /// <param name="other">User ID being friended</param>
        [HttpPost("NewProfile")]
        public string NewProfile(string username, string password, string reenteredpassword, string email, string? bio, bool forager, bool farmer)
        {
            
            var profilemsg = _frndService.NewProfile(username, password, reenteredpassword, email, bio, DateTime.UtcNow, forager, farmer);
            return profilemsg;
        }

        /// <summary>
        /// How to update bio info.
        /// </summary>
        /// <param name="user">User that's changing their bio</param>
        /// <param name="bio">New bio</param>
        [HttpPut("UpdateBio")]
        public ActionResult UpdateBio(string user, string bio)
        {
            if (Authenticate(user) == false) { return NoContent(); }
            var newbio = _frndService.UpdateBio(user, bio);
            return Accepted(newbio);
        }


        /// <summary>
        /// How to add inventory info.
        /// </summary>
        /// <param name="user">User that's changing their inventory</param>
        /// <param name="produce">New inventory item</param>
        [HttpPut("UpdateInventory")]
        public ActionResult UpdateInventory(string user, string produce)
        {
            if (Authenticate(user) == false) { return NoContent(); }
            var newproduce = _frndService.UpdateBio(user, produce);
            return Accepted(newproduce);
        }

        /// <summary>
        /// Check to see if a user has already established a friendship with another user.
        /// </summary>
        /// <param name="user">User ID who would have done the friending</param>
        /// <param name="other">User ID who would have been friended</param>
        /// <returns>Message about friendship</returns>
        [HttpGet("Profile/{user}")]
        public ActionResult<Profile> ViewProfile(string user)
        {
            var profile = _frndService.ViewProfile(user);
            return profile;
        }

        /// <summary>
        /// Check to see if a user has already established a friendship with another user.
        /// </summary>
        /// <param name="user">User ID who would have done the friending</param>
        /// <param name="other">User ID who would have been friended</param>
        /// <returns>Message about friendship</returns>
        [HttpGet("Friendship/{user}/{other}")]
        public ActionResult<string> CheckFriendship(string user, string other)
        {
            if (Authenticate(user) == false) { return NoContent(); }
            bool fstatus;
            fstatus = _frndService.CheckFriendship(user, other);
            string msg = "";
            if (fstatus == false) { msg = "You are not friends with this user."; }
            else if (fstatus == true) { msg = "You are already friends with this user."; }
            return msg;
        }

        /// <summary>
        /// Lists all of a user's friends. Should anyone be able to see who someone has friended?
        /// </summary>
        /// <param name="user">User ID who would have done the friending</param>
        /// <returns>list of friends</returns>
        [HttpGet("List/{user}")]
        public ActionResult<List<Friendship>> AllFriends(string user)
        {
            if (Authenticate(user) == false) { return NoContent(); }
            var list = new List<Friendship>();
            list = _frndService.AllFriends(user);
            return list;
        }

        /// <summary>
        /// How to 'add' a record.
        /// This should be a HTTP POST method.
        /// </summary>
        /// <param name="user">Username of the person submitting the record</param>
        /// <param name="item">Produce/item being taken</param>
        /// <param name="quantity">Number of items taken</param>
        /// <param name="dt">DateTime of the item getting taken- should be able to be set by user</param>
        [HttpPost("NewRecord")]
        public ActionResult NewRecord(string user, string item, int quantity, DateTime dt)
        {
            if (Authenticate(user) == false) { return NoContent(); }
            var record = _frndService.NewRecord(user, item, quantity, dt);
            return Accepted(record);
        }

        /// <summary>
        /// Display slection of user's records
        /// </summary>
        /// <param name="user">User who created the records</param>
        /// <param name="number">Desired quantity of records</param>
        /// <returns>list of records</returns>
        [HttpGet("DisplayRecords/{user}")]
        public ActionResult<List<Record>> DisplayRecords(string user, int number)
        {
            if (Authenticate(user) == false) { return NoContent(); }
            var list = new List<Record>();
            list = _frndService.DisplayRecords(user, number);
            return list;
        }

        /// <summary>
        /// This function should be called as a way to authenticate a user is who they say.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        private bool Authenticate(string userId)
        {
           if (1 == 0)
                return false; // for debugging
            return true;
        }

        //I wanted to use the profile info to make a simple Authenticate function, but it would require every controller function to include a "password" paramenter.
    }
}
