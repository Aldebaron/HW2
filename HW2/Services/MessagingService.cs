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
            //of it here. NextId kinda makes sense, but Messages being declared static up there doesn't make as much sense
            //in the C# definition of static. And I also looked into the C version of static, which is a lot more straightforward,
            //but is there a connection to how it works in C#? Also, is it okay that I deleted the TODO items? I was trying to limit
            //clutter but maybe you wanted them as a sort of log? ~A
            // Yep.
            // Static will ensure there is only one version of the variable if you create this object again and again.
            // Lucky for us, this won't happen because the way the Messaging Service is created, however if something does get messed up 
            // and there are two instances of this class, we won't have ID collisions. One object will not have message id 100, and the second object have message id 100 too!
            var date1 = new DateTime(2023, 5, 1, 8, 30, 52);
            var date2 = new DateTime(2020, 5, 1, 8, 30, 52);
            var date3 = new DateTime(2019, 5, 1, 8, 30, 52);
            Add("EJ", "Al", "New", date1);
            Add("EJ", "Al", "Old", date2);
            //Test ReadMessage w 2 EJ messages
            Add("Cat", "Al", "Old", date3);
            //Test Inbox w first EJ message and Cat Message
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

            List<Message> ConvoThread;
            ConvoThread = new List<Message> { };
            int n = Messages.Count - 1;

            while (n >= 0)
            {
                if ((Messages[n].To == user || Messages[n].From == user) &&
                    (Messages[n].To == other || Messages[n].From == other) &&
                    (user != other))
                { ConvoThread.Add(Messages[n]); }
                n--;
            }

            n = Messages.Count - 1;

            if (user == other) {

                while (n >= 0)
                {
                    if (Messages[n].To == user && Messages[n].From == user) {
                        ConvoThread.Add(Messages[n]);
                    }
                    n--;

                }

            }

            n = ConvoThread.Count-1;
            int c = 0;
            Message t1 = new Message();
            Message t2 = new Message();

            while (c < n) {
                if (ConvoThread[c].CreatedAt.Ticks > ConvoThread[c + 1].CreatedAt.Ticks) { c++; }
                else if (ConvoThread[c].CreatedAt.Ticks < ConvoThread[c + 1].CreatedAt.Ticks)
                {
                    t1 = ConvoThread[c];
                    t2 = ConvoThread[c + 1];
                    ConvoThread[c] = t2;
                    ConvoThread[c + 1] = t1;
                    if (c > 0) { c = c + 1; };
                }

                else if (ConvoThread[c].CreatedAt.Ticks == ConvoThread[c + 1].CreatedAt.Ticks) { c++; }

                }





            //newest first ~A
            // There is a bug here (JVP-June-2022)
            //Do you mean that if user==other you see all messages sent/received by that person?
            //I fixed that, but if there's something else I don't see it.
            // The bug is in the sorting mechanism. If you change your Add's at the top with dates in different order, then they will also be out of sequence later.
            // We can never assume that messages get stored in order by ID, we must sort on date. (JVP-Jun-2022)
            //Also, should we limit SendMessage so that you can't send a message to yourself? ~A -- No (JVP-Jun-2022)
            //Fixed. (Right?) ~A

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

            
            int tc = 0;
            Message t1 = new Message();
            Message t2 = new Message();

            while (tc < n)
            {
                if (Messages[tc].CreatedAt.Ticks < Messages[tc + 1].CreatedAt.Ticks) { tc++; }
                else if (Messages[tc].CreatedAt.Ticks > Messages[tc + 1].CreatedAt.Ticks)
                {
                    t1 = Messages[tc];
                    t2 = Messages[tc + 1];
                    Messages[tc] = t2;
                    Messages[tc + 1] = t1;
                    if (tc > 0) { tc = tc + 1; };
                }

                else if (Messages[tc].CreatedAt.Ticks == Messages[tc + 1].CreatedAt.Ticks) { tc++; }

            }
            // Now for this function, Messages is definitely ordered by date. Would it be worth it
            //to put this somewhere else so that Messages is always ordered by date? ~A

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