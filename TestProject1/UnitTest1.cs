using GaiaShare.Helpers;

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
        public void Test3()
        { 
            //arrange
            int a = 2;
            int b = 3;
            //act
            int c = a * b;
            //assert
            Assert.True(c == 6, "quick maths") ;
        }

        //[Fact]
        //public void Test4()
        //{
        //    //arrange?
            
            

        //    //act?
        //    var m = HW2.Services.MessagingService.Add("Al", "EJ", "BIRTHDAY", DateTime.UtcNow);
        //    //What's wrong with this? I had a similar problem with the controller when I first started
        //    //working on the controller, and I think I solved it by bringing in "_msgService" but
        //    //it doesn't like that here.
          
        //    var ID = (m.Id - 1000) / 2;
            
        //    //assert
        //    Assert.True(m.To == "EJ", ID.ToString());
        //    //How could I include text in this as well as the variable?
        //}
        [Fact]
        public void Test5()
        {
            //arrange?



            //act?
            var m = HW2.Services.MessagingService.Messages[0];
            //This throws an out of range exception, almost like Messages is empty and hasn't been
            //initialized, what do I do about that?


            //assert
            Assert.True(m.To == "EJ", "The database first message is not what it should be");
           
        }
    }
}