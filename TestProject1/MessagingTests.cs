using HW2.Services;

namespace TestProject1
{
    public class MessagingTests
    {
        private MessagingService MsgService = new MessagingService(); // for the local version, should I cut this as well? And just add "var" to each local assignment?
        // I added new here, so you don't have to do it in each function. This will also showcase the issue I intended to bring up. See ChechReadCountALEJ().
        // For the local version, pick a different name. Pick only one different name and be consistent with the local variable, since it's local, each function will create a new one and not use a prior version. 

        /// <summary>
        /// Ensure that the messaging service has a specific number of messages in the database between Al and Joe
        /// </summary>
        [Fact]
        public void ThreadAlJoe()
        {
            
            // Arrange
            int count = 3000;

            // Act
            var thread = MsgService.ReadMessage("Al", "Joe");
            Console.WriteLine("Messages in thread: " + thread.Count);

            // Assert
            Assert.True(thread.Count > 0, "messages not found.");
            Assert.True(count == thread.Count, "The number of messages between Al and Joe are not as expected: " + count);
        }

        //Tests that Id is at least even, like it's supposed to be
        [Fact]
        public void CheckAdd()
        {
            //arrange
            var localService = new MessagingService();
            var m = localService.Add("Al", "EJ", "BIRTHDAY", DateTime.UtcNow);
            int i;

            //act
            i = m.Id % 2;

            //assert
            //Assert that Id is even
            Assert.Equal(0, i); 
        }

        //Checks for expected count of messages and for a message with specified body.
        [Fact]
        public void CheckMessages()
        {
            //arrange
            int count = 1; // expected number of messages
            var body = "Hi, let's test special chars?&$%^ at the same time. This always needs consideration when viewing in an HTML page or URL (get)."; // specified body
            var localService = new MessagingService();
            localService.Add("Cat", "Other Cat", "meow", DateTime.UtcNow);
            localService.Add("Jim", "Steve", body, DateTime.UtcNow);
            localService.Add("Al", "Joe", "Random messages are oddly hard to write", DateTime.UtcNow);
            bool msgFound = false;
            bool test; // what is the default status of a bool if you don't specify?
            //I wrote an assert statement for it but it won't accept an unnassigned variable even tho it's boolean

            //act
           var thread = localService.ReadMessage("Jim", "Steve");
           int m = thread.Count;

            //WAIT- what if I added a function in MessagingServices that searches all of a users messages for
            //ones containing a keyphrase? This wouldn't really solve the above problem, you'd still need to know
            //at least one user involved, but it would add some cool functionality to the messaging service
            // That's called big brother. Complete view. However this might be an admin function we add down the road for monitoring user's activity. 
            // Still big brother, however if users become unruly, we might have to ban/block their account.
            //No, sorry, I mean like how Messages on the iphone offers a search *your own* messages function. And when it's actually in use,
            //bc you'd be searching your own messages, the user would always be known.

            for (int i =0; i < m; i++) {
                if (thread[i].Body == body) { msgFound = true; }
            }

            //assert
            //Asserts that Jim/Steve thread contains expected number of messages
            //Asserts that there is a message with the specified body
            Assert.Equal(count, m); // It wouldn't allow me to include a failure message? It got very angry and very confusing.
            Assert.True(msgFound, "There is no message with the body " + body);
            //Assert.False(test, "The default status of an unspecified bool is true");
        }

        //Checks to see if ReadMessage produces the right number of messages
        [Fact]
        public void CheckReadCountAlEJ()
        {
            //arrange
            //var localService = new MessagingService();
            int count = 2000; // expected count of messages
                           
            //act
            var m = MsgService.ReadMessage("Al", "EJ"); 
            int b = m.Count; // actual count of messages

            //assert
            Assert.Equal(count, b);
        }
        //This fails every time, but none of the others do! And b is a different number every time, ~3900. I have no idea why.
        // NOTE: YES!! You've found what I wanted you to see here. Continue to use the global variable.
        //So this one keeps the global variable and the ones that you put pickanewname in use a local?
    }
}