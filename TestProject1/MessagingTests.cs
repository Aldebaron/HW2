using HW2.Services;

namespace TestProject1
{
    public class MessagingTests
    {
        /// <summary>
        /// Ensure that the messaging service has a specific number of messages in the database between Al and Joe
        /// </summary>
        [Fact]
        public void ThreadAlJoe()
        {
            // Arrange
            var m = new MessagingService();
            int count = 3000;

            // Act
            var thread = m.ReadMessage("Al", "Joe");
            Console.WriteLine("Messages in thread: " + thread.Count);

            // Assert
            Assert.True(thread.Count > 0, "messages not found.");
            Assert.True(count == thread.Count, "The number of messages between Al and Joe are not as expected: " + count);
        }
    }
}