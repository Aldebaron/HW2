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
        public ActionResult<List<Friendship>> GetAll() =>
            _frndService.ListFriends();


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
        /// This function should be called as a way to authenticate a user is who they say.
        /// For example, any one can call the endpoint SendMessage(). If a nefarious user
        ///  tries to impersonate another user, this function should catch it. 
        ///  We want to utilize this function in any area where a message is created or read,
        ///  to ensure the proper person is accessing the message.
        /// For example 2, if 'Joe' is sending a message, be sure he cannot send a message
        ///  as someone else 'Jim' to a user 'Sue'. This example it doesn't really matter who 
        ///  the 'to' is, but it's extremely important that the 'From' is validated.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        private bool Authenticate(string userId)
        {
            // Will check login information to see if this user is who they say.
            // For now authentication details are masked. We will assume good actors.
            // Terminology 'good actor' or 'bad actor' is a generic way to reference nefarious situations.
            if (1 == 0)
                return false; // for debugging
            return true;
        }
    }
}
