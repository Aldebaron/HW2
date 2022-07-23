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

       

    }
}