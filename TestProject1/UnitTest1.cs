using GaiaShare.Helpers;
using HW2.Services;

namespace TestProject1
{
    public class UnitTest1
    {
        private MessagingService MsgService;

        public UnitTest1()
        {
            MsgService = new MessagingService();
        }

        [Fact]
        public void Test1()
        {
            Console.WriteLine("hello world");
            Assert.True(true, "haha I told u so");
        }

        [Fact]
        public void CheckDate()
        {
            var delta = TimeHelper.getDateTimeDelta(new DateTime(2022, 5, 1, 12, 0, 0));
            Console.WriteLine("hello world");
            Assert.True(delta.Length > 1, "Time delta text was not returned.");
        }

        [Fact]
        public void GoodMath()
            //Just an example of a passed test
        {
            //arrange
            int a = 2;
            int b = 3;
            //act
            int c = a * b;
            //assert
            Assert.True(c == 6, "quick maths");
        }

        //Just an example of a failed test
        [Fact]
        public void BadMath()
        {
            //arrange
            int a = 2;
            int b = 3;
            
            //act
            int c = a * b;
        
            //assert
            Assert.True(c == 5, "bad maths");
            //So for Assert.True or Assert.False, it checks if the asserted thing is true of the first bit,
            //and if it is it does nothing, but if it isn't then it prints the message in the second bit?
            // JVP-Jul-2022 -- Correct! Here's a crash course in Testing: https://auth0.com/blog/xunit-to-test-csharp-code/
            // Get more descriptive with your assert message (second param). And test out Assert.Equal here.  What other Assert methods can you use in this class?
        }

        //Just to see how Add would work in tests, Assert statement uses Id number
        //to see if it gets added before or after the database is built
        // NOTE: Get in the habit of adding comments for the function above the function like this. (JVP-Jul-2022)
        [Fact]
        public void CheckAdd()
        {
            //arrange
            var m = MsgService.Add("Al", "EJ", "BIRTHDAY", DateTime.UtcNow);
            //What's wrong with this? I had a similar problem with the controller when I first started
            //working on the controller, and I think I remember solving it by bringing in "_msgService" but
            //it doesn't like that here, I think because _msgService is private.
            // JVP-Jul-2022 You are on the right track. You must create an Object before you can "add" to it.
            // _msgService is kinda magic.
            // MsgService is the same concept, but I've created a constructor to instantiate the object.
            // Homework: use the global MsgService 


            //assert
            Assert.True(m.Id > 0, "Id was not greater than zero, it was: " + m.Id.ToString()); // Greater than zero is "business logic" we have stated in the imaginary specification that an id must be positive.
            //How could I include text in this as well as the variable? -- Is the above answer what you are asking? (JVP-Jul-2022)
            //TODO: Assert The ID is even. (business logic)
        }


        [Fact]
        public void CheckMessages()
            //Checks to see if the first message in the database is what I expect it to be,
            //as a way to test the database
        {
            //arrange
            int count = 10; // expected number of messages (change to fit your test case)
            var body = "Message body"; // Search a list of messages (between two people) to assert this message is in the list.
           
            var m = MessagingService.Messages[0];
            //This throws an index out of range exception, almost like Messages is empty what do I do about that?
            //Test 9 confirms that Messages is empty

            //act
            // Add message or two here. I'll give you a specific case to write:
            // 1) add a random message.
            // 2) add a message with contents "body" per above.
            // 3) add a random message.

            //assert
            Assert.True(m.To == "EJ", "The database first message is not what it should be");
            // TODO: Assert Count of messages
            // TODO: Assert "body" message is contained in the list.
        }

        [Fact]
        public void CheckRead()
            //Tests the ability to use ReadMessage by checking to see if ReadMessage
            //produces the right number of messages
        {
            //arrange
            int count = 10; // count of messages
            //TODO: fix like others
            //var m = MessagingService.ReadMessage("Al", "EJ");
            
            //act
            int b = 0; //m.Count;

            //assert
            Assert.True(b == count, "bad ConvoThread");
        }

        [Fact]
        public void DatabaseCheck1()
            //Checks if the database length is what it's supposed to be (before anything else gets added)
        {
            //arrange
            int m;

            //act
             m = MessagingService.Messages.Count;
            //splitting it up this way to make it fit A/A/A seems like cheating, is there a diffferent
            //way that I should be doing this?

            //assert
            Assert.True(m == 12000, "Messages not working- some missing");
        }

        [Fact]
        public void DatabaseCheck2()
            //Checks if the database is empty
        {
            //arrange
            int m;

            //act
            m = MessagingService.Messages.Count;
            

            //assert
            Assert.False(m == 0, "Messages reeally not working");
        }

    }
}