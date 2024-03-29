﻿using HW2.Models;
using HW2.Services;
using Microsoft.AspNetCore.Mvc;

namespace HW2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MessagingController : ControllerBase
    {
        //TODO:
        // Decide if this interface will be suficient for "recievers" and "senders".
        // How will it look if the user is sending messages or recieving messages?

        private readonly MessagingService _msgService;

        public MessagingController (MessagingService ms)
        {
            _msgService = ms;
        }

        /// <summary>
        /// Default, just to ensure all is working.
        /// </summary>
        /// <returns>Basic Message</returns>
        /// 
        //[HttpGet]
        //public ActionResult<string> Index()
        //{
        //    return "Messaging";
        //}

        //Changed to GetAll for testing ~A
        [HttpGet]
        public ActionResult<List<Message>> GetAll() =>
            _msgService.GetAll();


        /// <summary>
        /// How to 'add' a message.
        /// This should be a HTTP POST method.
        /// </summary>
        /// <param name="to">User ID the message will be send to.</param>
        /// <param name="from">User ID who sent the message</param>
        /// <param name="message">Body of the message. Note: To keep it simple, we will not use subject.</param>
        [HttpPost("Send")]
        public ActionResult SendMessage(Message msg)
        {
            if (Authenticate(msg.From) == false) { return NoContent(); }
            var mesg = _msgService.Add(msg.To, msg.From, msg.Body, msg.CreatedAt);
            return Accepted(mesg);
            //This returns the message info, but it's buggy.
            //It allows you to set an id, which is a problem, though it does overwrite it when it adds to Messages,
            //which you can see in GetAll, but the Accepted message shows the overwritten wrong id.
            //If you change the paramaters from "Message msg" to "string to, string from, string body, DateTime dt"
            //and use that instead of msg, it solves both problems, but I imagine you changed it from that for a reason. ~A
        }

        /// <summary>
        /// To read messages from another users. This will get a 'thread' -- both to and from
        /// messages ordered by date, newest on top.
        /// </summary>
        /// <todo>
        /// Decide the input parameters here.
        /// </todo>
        /// <returns>List of Messages ordered descending</returns>
        [HttpGet("Read/{user}/{other}")]
        public ActionResult<List<Message>> ReadMessage(string user, string other)
        {
            if (Authenticate(user) == false) { return NoContent(); }
            var messages = new List<Message>();
            messages = _msgService.ReadMessage(user, other);
            return messages;
        }

        /// <summary>
        /// Inbox view, display a list of users who you are conversing with. 
        /// </summary>
        /// <returns></returns>
        [HttpGet("List/{user}")]
        public ActionResult<List<Message>> ListMessages(string user)
        {
            if (Authenticate(user) == false) { return NoContent(); }
            var list = new List<Message>();
            list = _msgService.Inbox(user);
            return list;
        }

        [HttpGet("SearchConvo/{user}/{other}/{search}")]
        public ActionResult<List<Message>> SearchConvo(string user, string other, string search)
        {
            if (Authenticate(user) == false) { return NoContent(); }
            // look for contents aka 'search'
            var messages = _msgService.SearchConvo(user, other, search);
            return messages;
        }

        //Needs Unit Tests
        [HttpGet("SearchAll/{user}/{search}")]
        public ActionResult<List<Message>> SearchAll(string user, string search)
        {
            if (Authenticate(user) == false) { return NoContent(); }
            var messages = _msgService.SearchAll(user, search);
            return messages;
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
