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

        //Just to see how Add would work in tests, Assert statement uses Id number
        //to see if it gets added before or after the database is built
        [Fact]
        public void CheckAdd()
        {
            //arrange
            var pickanewname = new MessagingService();
            var m = pickanewname.Add("Al", "EJ", "BIRTHDAY", DateTime.UtcNow);
            int i;

            //act
            i = m.Id % 2;

            //assert
            //Assert that Id is even
            Assert.True(i == 0, "Id was not even, it was: " + m.Id.ToString()); // use Asert.Equal
        }

        //Checks to see if the first message in the database is what I expect it to be,
        //as a way to test the database
        [Fact]
        public void CheckMessages()
        {
            //arrange
            int count = 1; // expected number of messages
            var body = "Hi, let's test special chars?&$%^ at the same time. This always needs consideration when viewing in an HTML page or URL (get)."; // specified body
            MsgService = new MessagingService();
            MsgService.Add("Cat", "Other Cat", "meow", DateTime.UtcNow);
            MsgService.Add("Jim", "Steve", body, DateTime.UtcNow);
            MsgService.Add("Al", "Joe", "Random messages are oddly hard to write", DateTime.UtcNow);
            int m; // remove
            string msgFound = "no"; // change to boolean. You are using yes/no as a string -- why not use the precise data type: boolean
            bool test; // what is the default status of a bool if you don't specify?

            //act
           var thread = MsgService.ReadMessage("Jim", "Steve");
            m = thread.Count; // You are only using m once, for consisenstiy's sake remove m, or use m everwhere thread.Count is used. In this case I prefer removing thread.count.
            //But this only works if I know who the sender and receiver are, which is pretty limited.
            // For Unit Tests you ALWAYS have a contrived scenario. In arrange you setup a specific use case/business logic. 
            // here we have arranged one message is sent. Use the variable "body" in line 57 where you have a string representation. This eliminates errors when the message is changed.


            //WAIT- what if I added a function in MessagingServices that searches all of a users messages for
            //ones containing a keyphrase? This wouldn't really solve the above problem, you'd still need to know
            //at least one user involved, but it would add some cool functionality to the messaging service
            // That's called big brother. Complete view. However this might be an admin function we add down the road for monitoring user's activity. 
            // Still big brother, however if users become unruly, we might have to ban/block their account.


            //Checks Messages for a message with a specified body
            //Was there a shorter way to do this? I couldn't think of one.
            for (int i =0; i < thread.Count; i++) {
                if (thread[i].Body == body) { msgFound = "yes"; }
            }

            //assert
            //Asserts that Jim/Steve thread contains expected number of messages
            //Asserts that there is a message with the specified body
            Assert.True(count == m, "The database message count is not what it should be"); // This is better as Assert.Equal()
            Assert.True(msgFound == "yes", "There is no message with the body " + body); // Assert.Ture will work best here for type: bool
        }

        //Tests the ability to use ReadMessage by checking to see if ReadMessage
        //produces the right number of messages
        // Get descritive with your test function names. this is part of the documentatin, don't be shy on char length.
        [Fact]
        public void CheckReadCountAlEJ()
        {
            //arrange
            //MsgService = new MessagingService();
            int count = 2000; // expected count of messages
                           
            //act
            var m = MsgService.ReadMessage("Al", "EJ"); // NOTE: this is act (not arrange)
            int b = m.Count; 

            //assert
            //asserts that count of thread equals expectation (count) // NOTE: Good documentation, can you write this into an Assert.Equal statement so the code line has all the documentation included?
            //IE: logic of the first parameter should be very clear what you are testing. 
            // second parameter should tell exactly what went wrong if the test failed.
            // the above two items should tell a documentation story of what's going on here (positive, or failed case).
            // Let's see how much ingenuity you can pack into one line of code :)
            Assert.True(b == count, "bad ConvoThread");
        }
        //This fails every time, but none of the others do! And b is a different number every time, ~3900. I have no idea why.
        // NOTE: YES!! You've found what I wanted you to see here. Continue to use the global variable. 

        //With a local MsgService, this works! I like the local better than global for these tests, it's much easier to keep track of. 
    }
    //2nd test
}