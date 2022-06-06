
namespace HW2.Models
{
    public class Rectangle
    {


        public int Id { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public double area = 0; 


        public Rectangle()
        {
            // nothing to do
            area = Width * Height;
        }

        public void CalculateArea()
        {
            area = Width * Height;
        }

        /// <summary>
        /// With ID
        /// </summary>
        public Rectangle(int id, int x, int Y, int H, int W)
        {
            Id = id; // special sauce
            X = x;
            this.Y = Y;
            //Rectangle();
        }

        /// <summary>
        /// Without ID
        /// </summary>
        public Rectangle(int x, int Y, int H, int W)
        {
            Id = X + Y; // special sauce
            X = x;
            this.Y = Y;
        }



    }
    
}