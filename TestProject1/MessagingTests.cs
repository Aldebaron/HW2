using HW2.Services;

namespace TestProject1
{
    public class MessagingTests
    {
        private MessagingService MsgService; // for the local version, should I cut this as well? And just add "var" to each local assignment?

        /// <summary>
        /// Ensure that the messaging service has a specific number of messages in the database between Al and Joe
        /// </summary>
        [Fact]
        public void ThreadAlJoe()
        {

            // Arrange
            MsgService = new MessagingService();
            var m = MsgService;
            int count = 3000;

            // Act
            var thread = m.ReadMessage("Al", "Joe");
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
            MsgService = new MessagingService();
            var m = MsgService.Add("Al", "EJ", "BIRTHDAY", DateTime.UtcNow);
            int i;

            //Act
            i = m.Id % 2;

            //assert
            //Assert that Id is even
            Assert.True(i == 0, "Id was not even, it was: " + m.Id.ToString()); 
        }

        //Checks to see if the first message in the database is what I expect it to be,
        //as a way to test the database
        [Fact]
        public void CheckMessages()
        {
            //arrange
            MsgService = new MessagingService();
            int count = 1; // expected number of messages
            var body = "Message body"; // specified body
            int m;
            string msg = "no";
            
            //act
            MsgService.Add("Cat", "Other Cat", "meow", DateTime.UtcNow);
            MsgService.Add("Jim", "Steve", "Message body", DateTime.UtcNow);
            MsgService.Add("Al", "Joe", "Random messages are oddly hard to write", DateTime.UtcNow);
            var thread = MsgService.ReadMessage("Jim", "Steve");
            m = thread.Count;
            //But this only works if I know who the sender and receiver are, which is pretty limited.
            //WAIT- what if I added a function in MessagingServices that searches all of a users messages for
            //ones containing a keyphrase? This wouldn't really solve the above problem, you'd still need to know
            //at least one user involved, but it would add some cool functionality to the messaging service

            //Checks Messages for a message with a specified body
            //Was there a shorter way to do this? I couldn't think of one.
            for (int i =0; i < thread.Count; i++) {
                if (thread[i].Body == body) { msg = "yes"; }
                }

            //assert
            //Asserts that Jim/Steve thread contains expected number of messages
            //Asserts that there is a message with the specified body
            Assert.True(count == m, "The database message count is not what it should be");
            Assert.True(msg == "yes", "There is no message with the body " + body);
        }

        //Tests the ability to use ReadMessage by checking to see if ReadMessage
        //produces the right number of messages
        [Fact]
        public void CheckRead()
        {
            //arrange
            MsgService = new MessagingService();
            int count = 2000; // expected count of messages
            var m = MsgService.ReadMessage("Al", "EJ");               
                           
            //act
            int b = m.Count; 

            //assert
            //asserts that count of thread equals expectation (count)
            Assert.True(b == count, "bad ConvoThread");
        }
        //This fails every time, but none of the others do! And b is a different number every time, ~3900. I have no idea why.
        //With a local MsgService, this works! I like the local better than global for these tests, it's much easier to keep track of. 
    }
}