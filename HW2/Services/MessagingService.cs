using HW2.Models;

namespace HW2.Services
{

    public class MessagingService
    {
        public static int NextId = 998;
        public static List<Message> Messages = new List<Message>(); // Database of all messages
        
        public MessagingService()
        {


            //Should I have used the "test" window for these test messages instead of putting
            //them in directly through the service? ~A
            //Test Explorer is a good way to test this section w/o having to run the application. We'll look at that later. (JVP-June-2022)

            Messages = new List<Message> { };

            //How do things like base classes and subclasses work?
            //I think I understand static when it comes to classes like models, but I don't fully understand the use
            //of it here. NextId kinda makes sense, but Messages being declared staic up there doesn't make as much sense
            //in the C# definition of static. And I also looked into the C version of static, which is a lot more straightforward,
            //but is there a connection to how it works in C#? Also, is it okay that I deleted the TODO items? I was trying to limit
            //clutter but maybe you wanted them as a sort of log? ~A

            
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

        public List<Message> GetAll() => Messages;


        public Message Add(string to, string from, string body, DateTime dt)
        {
            if (dt == new DateTime()) dt = DateTime.UtcNow;
            //How does a "new" statement work with an equality operator? ~A
            var message = new Message(to, from, body, dt);
            NextId += 2;
            message.Id = NextId;


            Messages.Add(message);
            return message;
            //Why does this return the message? ~A
        }


        public List<Message> ReadMessage(string user, string other) {

            List<Message> ConvoThread;
            ConvoThread = new List<Message> { };
            int n = Messages.Count-1;

            while (n >= 0)
            {
                if ((Messages[n].To == user || Messages[n].From == user) &&
                        (Messages[n].To == other || Messages[n].From == other)
                        && (user != other))
                { ConvoThread.Add(Messages[n]); }
                n--;
            }
            //newest first ~A
            // There is a bug here (JVP-June-2022)
            //Do you mean that if user==other you see all messages sent/received by that person?
            //I fixed that, but if there's something else I don't see it.
            //Also, should we limit SendMessage so that you can't send a message to yourself? ~A

            return ConvoThread;
        }


        public List<Message> Inbox(string user) {

            List<Message> inbox;
            List<Message> ConvoThread;
            List<string> Corresponders;
            inbox = new List<Message> { };
            ConvoThread = new List<Message> { };
            Corresponders = new List<string> { };
            int n = Messages.Count-1;

            while (n >= 0) {

                if (Messages[n].From == user && !Corresponders.Contains(Messages[n].To)) {
                    Corresponders.Add(Messages[n].To);
                }
                if (Messages[n].To == user && !Corresponders.Contains(Messages[n].From))
                {
                    Corresponders.Add(Messages[n].From);
                }
                n--;

            }

            int m = Corresponders.Count;
            int c = 0;
            int co = 0;
            int x;
            int t = Messages.Count;

            while (c < m)
            {


                while (co < t)
                {
                    if ((Messages[co].To == user || Messages[co].From == user) &&
                        (Messages[co].To == Corresponders[c] || Messages[co].From == Corresponders[c]))
                            { ConvoThread.Add(Messages[co]); }
                    co++;
                    //Messages in ConvoThread are grouped by corresponder, and groups are organized
                    //by the most recent message sent/received by that corresponder. Most
                    //recent is listed first.

                }
                co = 0;
                x = ConvoThread.Count - 1;
                inbox.Add(ConvoThread[x]);
                c++;
                //Most recent message added to the list first, so it's on top

                //Only showing the most recent message from each person, bc it's an inbox.
                //I'm assuming that to see all messages from a specific person, you'd
                //just use ReadMessages, but ConvoThread does contain all of the user's messages,
                //so we could easily just give that instead. ~A
            }



            return inbox;
        }

    }
}