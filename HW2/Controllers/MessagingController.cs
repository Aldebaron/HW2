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
        [HttpGet]
        public ActionResult<string> Index()
        {
            return "Messaging System Online.";
        }

        /// <summary>
        /// How to 'add' a message.
        /// This should be a HTTP POST method.
        /// </summary>
        /// <param name="to">User ID the message will be send to.</param>
        /// <param name="from">User ID who sent the message</param>
        /// <param name="message">Body of the message. Note: To keep it simple, we will not use subject.</param>
        /// <returns>boolean</returns>
        [HttpPost("Send/{to}/{from}/{message}")]
        public ActionResult<bool> SendMessage(string to, string from, string message)
        {
            return false;
        }

        /// <summary>
        /// To read messages from another users. This will get a 'thread' -- both to and from
        /// messages ordered by date, newest on top.
        /// </summary>
        /// <todo>
        /// Decide the input parameters here.
        /// </todo>
        /// <returns>List of Messages ordered descending</returns>
        [HttpGet("Read/{from}")]
        public ActionResult<List<Message>> ReadMessage(string from)
        {
            var messages = new List<Message>();
            return messages;
        }

        /// <summary>
        /// Inbox view, display a list of users who you are conversing with. 
        /// </summary>
        /// <returns></returns>
        [HttpGet("List/{from}")]
        public ActionResult<List<Message>> ListMessages()
        {
            var list = new List<Message>();
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
            return true;
        }
    }
}