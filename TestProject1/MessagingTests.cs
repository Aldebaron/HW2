using HW2.Services;

namespace TestProject1
{
    public class MessagingTests
    {
        private MessagingService MsgService; // for the local version, should I cut this as well? And just add "var" to each local assignment?
        // I added new here, so you don't have to do it in each function. This will also showcase the issue I intended to bring up. See ChechReadCountALEJ().
        // For the local version, pick a different name. Pick only one different name and be consistent with the local variable, since it's local, each function will create a new one and not use a prior version. 

        public MessagingTests()
        {
            MsgService = new MessagingService();
        }
        /// <summary>
        ///Tests that Id is at least even, like it's supposed to be
        /// <summary>
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
        /// <summary>
        ///Checks for expected count of messages and for a message with specified body.
        /// <summary>
        [Fact]
        public void CheckMessages()
        {
            //arrange
            int expectedCount = 1; // expected number of messages
            var body = "Hi, let's test special chars?&$%^ at the same time. This always needs consideration when viewing in an HTML page or URL (get)."; // specified body
            var localService = new MessagingService();
            localService.Add("Cat", "Other Cat", "meow", DateTime.UtcNow);
            localService.Add("Jim", "Steve", body, DateTime.UtcNow);
            localService.Add("Al", "Joe", "Random messages are oddly hard to write", DateTime.UtcNow);
            bool msgFound = false;
            
            //act
            var thread = localService.ReadMessage("Jim", "Steve");
            int m = thread.Count;

            for (int i =0; i < m; i++) {
                if (thread[i].Body == body) { msgFound = true; }
            }

            //assert
            //Asserts that Jim/Steve thread contains expected number of messages
            //Asserts that there is a message with the specified body
            Assert.Equal(expectedCount, m);
            Assert.True(msgFound, "There is no message with the body " + body);
        }
        /// <summary>
        ///Checks to see if ReadMessage produces the right number of messages
        /// <summary>
        [Fact]
        public void CheckReadCountAlEJ()
        {
            //arrange
            //var localService = new MessagingService();
            int expectedCount = 2000; // expected count of messages
                           
            //act
            var m = MsgService.ReadMessage("Al", "EJ"); 
            int b = m.Count; // actual count of messages

            //assert
            Assert.Equal(expectedCount, b);
        }
        //This fails every time, but none of the others do! And b is a different number every time, ~3900. I have no idea why.
        // NOTE: YES!! You've found what I wanted you to see here. Continue to use the global variable.
        //So this one keeps the global variable and the ones that you put pickanewname in use a local?
        // Let's talk about this in August.

        //Checks SearchConvo
        [Fact]
        public void CheckSearchConvoAlJoeGoodbye()
        {
            //arrange
            var localService = MsgService;
            //var localService = new MessagingService();
            int expectedCount = 1000; // expected number of messages

            //act
            var c = localService.SearchConvo("Joe", "Al", "Goodbye!");
            var d = localService.SearchConvo("Al", "Joe", "Goodbye!");

            //assert
            Assert.Equal(expectedCount, c.Count);
            Assert.Equal(c, d);
        }

        /// <summary>
        /// Searches for text that is not in the two parties conversation. 
        /// </summary>
        [Fact]
        public void CheckSearchConvoAlJoeNotFound()
        {
            //arrange
            var localService = MsgService;
            //var localService = new MessagingService();
            int expectedCount = 0; // expected number of messages

            //act
            var c = localService.SearchConvo("Joe", "Al", "See ya");
            var d = localService.SearchConvo("Al", "Joe", "See ya");

            //assert
            Assert.Equal(expectedCount, c.Count);
            Assert.Equal(c, d);
        }

        /// <summary>
        /// Testing the SearchConvo() endpoint for one word in a body of a message.
        /// </summary>
        [Fact]
        public void CheckSearchConvoAlJoeUniverse()
        {
            //arrange
            var localService = MsgService;
            //var localService = new MessagingService();
            var body = "This is a test messages in a test universe. "; // expected count of messages
            int expectedCount = 1; // expected number of messages

            //act
            var a = localService.Add("Joe", "Al", body, new DateTime());
            var c = localService.SearchConvo("Joe", "Al", "universe");
            var d = localService.SearchConvo("Al", "Joe", "universe");

            //assert
            Assert.Equal(body, a.Body);
            Assert.Equal(expectedCount, c.Count);
            Assert.Equal(c, d);
        }

        /// <summary>
        /// Ensure that the messaging service has a specific number of messages in the database between Al and Joe
        /// </summary>
        [Fact]
        public void ThreadAlJoe()
        {
            // Arrange
            int expectedCount = 3001;

            // Act
            var msg = MsgService.Add("Al", "Joe", "Three thousand and one.", DateTime.UtcNow);
            var thread = MsgService.ReadMessage("Al", "Joe");
            Console.WriteLine("Messages in thread: " + thread.Count);

            // Assert
            Assert.True(thread.Count > 0, "no messages found.");
            Assert.True(expectedCount == thread.Count, "The number of messages between Al and Joe are not as expected: " + expectedCount);
        }

        /// <summary>
        /// Ensure that the messaging service has a specific number of messages in the database between Al and Joe
        /// </summary>
        [Fact]
        public void ThreadAlJoe2()
        {
            // Arrange
            int expectedCount = 3000; // ThreadAlJoe must run before this test.

            // Act
            var thread = MsgService.ReadMessage("Al", "Joe");
            Console.WriteLine("Messages in thread: " + thread.Count);

            // Assert
            Assert.True(thread.Count > 0, "no messages found.");
            Assert.True(expectedCount == thread.Count, "The number of messages between Al and Joe are not as expected: " + expectedCount + ". Found: " +thread.Count);
        }
        /// <summary>
        /// Search for something that doesn't exist (will fix name later- drawing a blank rn)
        /// </summary>
        [Fact]
        public void JimSearchNoResult() //UnicornHunt()
        {
            //Arrange
            string body = "Unicorns";
            string user = "Jim";
            var localService = new MessagingService();
            //Act
            var list = localService.SearchAll(user,body);
            //Assert
            Assert.Null(list);
        }
        /// <summary>
        /// Search for a body that belongs to a different user
        /// </summary>
        [Fact]
        public void SearchForOtherUserMessageBody() //SteveTheBodySnatcher()
        {
            //Arrange
            string body = "Latest Test";
            string user = "Steve";
            var localService = new MessagingService();
            //Act
            var list = localService.SearchAll(user, body);
            //Assert
            Assert.Null(list);
        }
        /// <summary>
        /// Search for something no body, and checks to see if a string that is "" is technically found in all messages or none
        /// </summary>
        [Fact]
        public void SearchEmptyBody() //ParanormalInvestigator()
        {
            //Arrange
            string body = "";
            string body2 = null;
            //I was wondering if "" == null
            string user = "Steve";
            var localService = new MessagingService();
            //Act
            var list = localService.SearchAll(user, body);
            var list2 = localService.SearchAll(user, body2);

            //Assert
            //Assert that Steve does not have any messages with no body
            Assert.Null(list);
            Assert.Null(list2);
        }
        /// <summary>
        /// Checks search functionality on special characters- hunts for special characters
        /// </summary>
        [Fact]
        public void JimSteveSpecialCharacterSearch()//ElmerFudd()
        {
            //Arrange
            string body = "$@%& you!";
            string user = "Jim";
            string other = "Steve";
            var localService = new MessagingService();
            //Act
            localService.Add(user, other, body, DateTime.UtcNow);
            var list = localService.SearchAll(user, body);
            //Assert
            Assert.NotNull(list);
        }
        /// <summary>
        /// Checks search functionality for a space instead of a character- searches for space with no character
        /// </summary>
        [Fact]
        public void EmptySpaceSearchJim()//MorallyBankruptAstronautWhosReallyShittyAtHisJob()
        {
            //Arrange
            string search = " ";
            string body = "Tell me where it is or this puppy is gonna get it";
            string user = "Jim";
            string other = "Al";
            var localService = new MessagingService();
            //Act
            localService.Add(user, other, body, DateTime.UtcNow);
            var list = localService.SearchAll(user, search);
            //Assert
            Assert.NotNull(list);
            Assert.True(list.Count > 1);
        }
        /// <summary>
        /// Checks search functionality for a space instead of a character- searches for space with character
        /// </summary>
        [Fact]
        public void CharacterSpaceSearchJim()//IneptAstronautWhosReallyJustTryingHisBest()
        {
            //Arrange
            string search = "se, co";
            string body = "Please, could you help me? I got lost, and I was supposed to be in orbit 15 minutes ago...";
            string user = "Jim";
            string other = "Al";
            int expected = 1;
            var localService = new MessagingService();
            //Act
            localService.Add(user, other, body, DateTime.UtcNow);
            var list = localService.SearchAll(user, search);
            //Assert
            Assert.NotNull(list);
            Assert.Equal(expected, list.Count);
        }

    }
}