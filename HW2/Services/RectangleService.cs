﻿using HW2.Models;

public static class RectangleService
{
    static List<Rectangle> Rectangles { get; }
    static int nextId = 3;
    static RectangleService()
    {
        var idis1 = new Rectangle(1, 2, 3, 4, 5);
        var idno = new Rectangle(2, 3, 4, 5);

        Rectangles = new List<Rectangle>
        {
            new Rectangle { Id = 1, X = 20, Y = 20, Height = 25, Width = 30},
            new Rectangle { Id = 2, X = 15, Y = 20, Height = 30, Width = 30 },
        };  
    }

    public static List<Rectangle> GetAll() => Rectangles;

    public static Rectangle? Get(int id) => Rectangles.FirstOrDefault(p => p.Id == id);

    public static void Add(Rectangle rectangle)
    {
        rectangle.Id = nextId++;
        rectangle.CalculateArea();

        Rectangles.Add(rectangle);
    }

    public static void Delete(int id)
    {
        var rectangle = Get(id);
        if (rectangle is null)
            return;

        Rectangles.Remove(rectangle);
    }
    
    public static void Update(Rectangle rectangle)
    {
        var index = Rectangles.FindIndex(p => p.Id == rectangle.Id);
        if (index == -1)
            return;

       Rectangles[index] = rectangle;
    }
    
}