using GaiaShare.Helpers;
using HW2.Services;

namespace TestProject1
{
    public class UnitTest1
    {
        // Tests the "facebook" style of date -- how many minutes,days,weeks since the posting.
        [Fact]
        public void CheckDate()
        {
            //arrange / act
            var delta = TimeHelper.getDateTimeDelta(new DateTime(2022, 5, 1, 12, 0, 0));
            Console.WriteLine("hello world");

            //assert
            Assert.True(delta.Length > 1, "Time delta text was not returned.");
        }

        [Fact]
        public void GoodMath()
            //Just an example of a passed test
        {
            //arrange
            int a = 2;
            int b = 3;
            object x = null;
            //act
            int c = a * b;
            //asserts that the result of multiplication is what is expected
            //asserts that the null object is null
            Assert.Equal(6, c);
            Assert.Null(x);
        }


        [Fact]
        public void BadMath()
        {
            //arrange
            int a = 2;
            int b = 3;
            int ans = 6;
            int wrong = 5;

            //act
            int c = a * b;
            
            //asserts that the result of multiplication is what is expected
            //asserts that the result of multiplication isn't what it shouldn't be
            Assert.True(c == ans, "bad maths");
            Assert.NotEqual(wrong, c);
            // JVP-Jul-2022 -- Correct! Here's a crash course in Testing: https://auth0.com/blog/xunit-to-test-csharp-code/
            // Get more descriptive with your assert message (second param). And test out Assert.Equal here.  What other Assert methods can you use in this class?

            // Ultimately we want all of our tests to pass. Fix up the GoodMath to match the best practices we've used up until now.
            // Review the different types of Assertions with XUnit. 
            // https://textbooks.cs.ksu.edu/cis400/1-object-orientation/04-testing/05-xunit-assertions/
            // Pick one new assertion and implement it in one of our test classes.
        }



    }
   
}