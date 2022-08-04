using HW2.Models;
using System.Diagnostics;

namespace HW2.Services
{

    public class MessagingService
    {
        public static int NextId = 998;
        private static List<Message> Messages = new List<Message>(); // Database of all messages

        public MessagingService()
        {


            //Should I have used the "test" window for these test messages instead of putting
            //them in directly through the service? ~A
            //Test Explorer is a good way to test this section w/o having to run the application. We'll look at that later. (JVP-June-2022)

            Messages = new List<Message> { };

            //How do things like base classes and subclasses work?
            //I think I understand static when it comes to classes like models, but I don't fully understand the use
            //of it here. NextId kinda makes sense, but Messages being declared static up there doesn't make as much sense
            //in the C# definition of static. And I also looked into the C version of static, which is a lot more straightforward,
            //but is there a connection to how it works in C#? Also, is it okay that I deleted the TODO items? I was trying to limit
            //clutter but maybe you wanted them as a sort of log? ~A
            // Yep.
            // Static will ensure there is only one version of the variable if you create this object again and again.
            // Lucky for us, this won't happen because the way the Messaging Service is created, however if something does get messed up 
            // and there are two instances of this class, we won't have ID collisions. One object will not have message id 100, and the second object have message id 100 too!

            //start timing

            for (int i = 0; i < 1000; i++)
            {
                Add("EJ", "Al", "New", new DateTime(2023, 5, 1, 8, 30, 52));
                Add("EJ", "Al", "Old", new DateTime(2020, 5, 1, 8, 30, 52));
                Add("Cat", "Al", "Old", new DateTime(2019, 5, 1, 8, 30, 52));
                Add("Jim", "Al", "Message 1", DateTime.UtcNow);
                Add("John", "Joe", "Test 2", DateTime.UtcNow);
                Add("Steve", "Al", "Hi", DateTime.UtcNow);
                Add("Al", "Steve", "Hello", DateTime.UtcNow);
                Add("Jim", "Al", "Hi!", DateTime.UtcNow);
                Add("Al", "Joe", "Goodbye", DateTime.UtcNow);
                Add("Joe", "Al", "Goodbye!", DateTime.UtcNow);
                Add("Steve", "Al", "See ya", DateTime.UtcNow);
                Add("Joe", "Al", "Latest Test", DateTime.UtcNow);
            }
            //Takes around 300 milliseconds for 1000 loop iterations, 3/10 millisecondfor one iteration.
            
        }
        public List<Message> GetAll() => Messages;


        public Message Add(string to, string from, string body, DateTime dt)
        {
            if (dt == new DateTime()) dt = DateTime.UtcNow;
            //How does a "new" statement work with an equality operator? ~A
            var test = new DateTime(); // See what the value is. 
			// Null is not an option with this type.
			// We are checking to ensure Date set to something. If we get "new" then we want to set to current d/t.

            var message = new Message(to, from, body, dt);
            NextId += 2;
            message.Id = NextId;


            Messages.Add(message);
            return message;
            //Why does this return the message? ~A
            // Obviously they know the to/from/message already.
            // but we add the ID of the message and createDate.
        }


        public List<Message> ReadMessage(string user, string other) {
            var ConvoThread = new List<Message> { };

            if (user == other)
            {

                for (int i = Messages.Count - 1; i >= 0; i--)
                {
                    if (Messages[i].To == user && Messages[i].From == user)
                    {
                        ConvoThread.Add(Messages[i]);
                    }
                    i--;
                }
            }
            //Fail fast! Special case (messages sent to self) handled before usual situation

            else
            {
                for (int i = Messages.Count - 1; i >= 0; i--)
                {
                    if ((Messages[i].To == user || Messages[i].From == user) &&
                        (Messages[i].To == other || Messages[i].From == other))
                    { ConvoThread.Add(Messages[i]); }

                }
            }
            //if a message in the database is sent/received by user/corresponder, add it to ConvoThread


            var t = new Message();
            //temporary message container for switching message order
   
            //organizes ConvoThread by newest to oldest
            for (int i=0; i < (ConvoThread.Count - 1); i++) {


                if (ConvoThread[i].CreatedAt < ConvoThread[i + 1].CreatedAt)
                {
                    t = ConvoThread[i];
                    ConvoThread[i] = ConvoThread[i + 1];
                    ConvoThread[i + 1] = t;
                    if (i > 0) { i-=2; };
                }

                }

           
            return ConvoThread;
        }


        public List<Message> SearchConvo(string user, string other, string search)
        {
            //if message in convothread has search term in body, add to searchthread.
            //limitation- if you searched for someone's name within a convothread of that person, you might expect it to return
            //all messages sent by that person. This code won't do that, it will only return messages with that person's name in the body,
            //and I think that has more practical merit, but I could also expand it if that's preferred.
            // Good idea! Small details like this may not always be apparent at the initil development. Let's see how it goes. (JVP-Jul-2022)
            var ConvoThread = ReadMessage(user, other);
            var SearchThread = new List<Message>();

            for (int i = 0; i < (ConvoThread.Count - 1); i++)
            {
                if (ConvoThread[i].Body.Contains(search)) { SearchThread.Add(ConvoThread[i]); };
            }

            return SearchThread;
        }

        public List<Message> Inbox(string user) {
            
            var inbox = new List<Message>();
            var ConvoThread = new List<Message>();
            var Corresponders = new List<string>();

            var t = new Message();
            //temporary container to switch order of list around


            //new inbox Average = 550 milliseconds - difference = 73
            for (int i = 0; i < Messages.Count; i++)
            {
                if (Messages[i].To == user || Messages[i].From == user)
                { ConvoThread.Add(Messages[i]); }

            }
            //Collect all messages sent/received by user

            for (int i = 0; i < (ConvoThread.Count - 1); i++)
            {

                if (ConvoThread[i].CreatedAt > ConvoThread[i + 1].CreatedAt)
                {
                    t = ConvoThread[i];
                    ConvoThread[i] = ConvoThread[i + 1];
                    ConvoThread[i + 1] = t;
                    if (i > 0) { i -= 2; };
                }

            }
            //organize user's messages by date

            string j;
            //placeholder for corresponder
            for (int i = (ConvoThread.Count - 1); i >= 0; i--)
            {
                if (ConvoThread[i].To == user) { j = ConvoThread[i].From; }
                else { j = ConvoThread[i].To; }
                if (!Corresponders.Contains(j)) { inbox.Add(ConvoThread[i]); Corresponders.Add(j); }
            }
            //starting with most recent, collect message if a message involving that corresponder hasn't already been
            //added to the list

            
                return inbox;


        }

        public List<Message> SearchAll(string user, string search)
        {
            //In order to use a call to Inbox for this instead, I'd need to make it so that inbox returns ConvoThread, not just inbox, because SearchAll
            //searches all of a user's messages, not just the most recent from each person. I could add a "decider" type thing like I did in alt for that, but
            //would that be the best way of doing that?

            var inbox = new List<Message>();
            var ConvoThread = new List<Message>();
            var Corresponders = new List<string>();
            var SearchThread = new List<Message>();

            var t = new Message();
            //temporary container to switch order of list around


            //new inbox Average = 550 milliseconds - difference = 73
            for (int i = 0; i < Messages.Count; i++)
            {
                if (Messages[i].To == user || Messages[i].From == user)
                { ConvoThread.Add(Messages[i]); }

            }
            //Collect all messages sent/received by user

            for (int i = 0; i < (ConvoThread.Count - 1); i++)
            {

                if (ConvoThread[i].CreatedAt > ConvoThread[i + 1].CreatedAt)
                {
                    t = ConvoThread[i];
                    ConvoThread[i] = ConvoThread[i + 1];
                    ConvoThread[i + 1] = t;
                    if (i > 0) { i -= 2; };
                }

            }
            //organize user's messages by date

            for (int i = (ConvoThread.Count - 1); i >= 0; i--) {
                if (ConvoThread[i].Body.Contains(search)) { SearchThread.Add(ConvoThread[i]); }
                else if (ConvoThread[i].To.Contains(search)) { SearchThread.Add(ConvoThread[i]); }
                else if (ConvoThread[i].From.Contains(search)) { SearchThread.Add(ConvoThread[i]); }

            }
            //if message in convothread contains search term, add it to searchthread.
            //I believe that some messaging service search things repeat messages if they contain multiple instances of the
            //search term. However, I think that's stupid. Unless you want me to do that, in which case it's not stupid.

            return SearchThread;
        }


    }

}
