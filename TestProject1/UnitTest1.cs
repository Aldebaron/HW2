using GaiaShare.Helpers;
using HW2.Services;

namespace TestProject1
{
    public class UnitTest1
    {
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
            Assert.True(delta.Length < 10, "text was not returned.");
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

        [Fact]
        public void BadMath()
            //Just an example of a failed test
        {
            //arrange
            int a = 2;
            int b = 3;
            //act
            int c = a * b;
            //assert
            Assert.True(c == 5, "bad maths");
        }
        //So for Assert.True or Assert.False, it checks if the asserted thing is true of the first bit,
        //and if it is it does nothing, but if it isn't then it prints the message in the second bit?

        [Fact]
        public void CheckAdd()
            //Just to see how Add would work in tests, Assert statement uses Id number
            //to see if it gets added before or after the database is built
        {
            //arrange?


            var m = MessagingService.Add("Al", "EJ", "BIRTHDAY", DateTime.UtcNow);
            //What's wrong with this? I had a similar problem with the controller when I first started
            //working on the controller, and I think I remember solving it by bringing in "_msgService" but
            //it doesn't like that here, I think because _msgService is private.


            //act?
            

            //assert
            Assert.True(m.Id != null, m.Id.ToString());
            //How could I include text in this as well as the variable?
        }


        [Fact]
        public void CheckMessages()
            //Checks to see if the first message in the database is what I expect it to be,
            //as a way to test the database
        {
            //arrange?
           
            var m = MessagingService.Messages[0];
            //This throws an index out of range exception, almost like Messages is empty what do I do about that?
            //Test 9 confirms that Messages is empty

            //act?

            //assert
            Assert.True(m.To == "EJ", "The database first message is not what it should be");
           
        }

        [Fact]
        public void CheckRead()
            //Tests the ability to use ReadMessage by checking to see if ReadMessage
            //produces the right number of messages
        {
            //arrange
            var m = MessagingService.ReadMessage("Al", "EJ");
            //same problem as Test 5, so the issue isn't just with Add()
            //Would changing [Fact] to [TestMethod] do anything?

            //act
            int b = m.Count;

            //assert
            Assert.True(b == 2000, "bad ConvoThread");
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