using HW2.Models;

namespace HW2.Services
{

    public class MessagingService
    {
        public static int NextId = 100;
        public List<Message> Messages; // Database of all messages
        public List<Message> inbox;
        public List<Message> ConvoThread;
        public List<string> Corresponders;
        
        public MessagingService()
        {
            //The manual creation using the curly braces was giving me some problem so I
            //did it this way and it didn't seem to impair functionality ~A
            var a = new Message("Jim", "Al", "Message 1");
            a.Id = NextId++;
            var b = new Message("John", "Joe", "Test 2");
            b.Id = NextId++;
            var c = new Message("Steve", "Al", "Hi");
            c.Id = NextId++;
            var d = new Message("Al", "Steve", "Hello");
            d.Id = NextId++;
            var e = new Message("EJ", "Al", "Hi!");
            e.Id = NextId++;
            var f = new Message("Al", "Joe", "Goodbye");
            f.Id = NextId++;
            var g = new Message("EJ", "Al", "Goodbye!");
            g.Id = NextId++;
            var h = new Message("Steve", "Al", "See ya");
            h.Id = NextId++;
            var j = Add("Joe", "Al", "constructor");

            //Should I have used the "test" window for these test messages instead of putting
            //them in directly through the service? ~A

            Messages = new List<Message>
            {a, b, c,d,e,f,g,h};
        }

        public List<Message> GetAll() => Messages;


        public Message Add(string to, string from, string body)
        {
            var message = new Message(to, from, body);
            message.Id = NextId++;

            Messages.Add(message);
            return message;
        }


        public List<Message> ReadMessage(string user, string other) {

            ConvoThread = new List<Message> { };
            int n = Messages.Count-1;

            while (n >= 0)
            {
                if ((Messages[n].To == user || Messages[n].From == user) &&
                        (Messages[n].To == other || Messages[n].From == other))
                { ConvoThread.Add(Messages[n]); }
                n--;

            }
           //newest first ~A
            return ConvoThread;
        }




        public List<Message> Inbox(string user) {

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