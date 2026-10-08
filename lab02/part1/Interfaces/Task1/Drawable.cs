namespace Interfaces.Task1;

public interface IDrawable
{
    void Draw();
}

public class Circle : IDrawable
{
    public double Radius { get; }

    public Circle(double radius) => Radius = radius;

    public void Draw() => Console.WriteLine($"Рисую круг радиусом {Radius}");
}

public class Rectangle : IDrawable
{
    public double Width { get; }
    public double Height { get; }

    public Rectangle(double width, double height)
    {
        Width = width;
        Height = height;
    }

    public void Draw() => Console.WriteLine($"Рисую прямоугольник {Width}x{Height}");
}

public static class Canvas
{
    public static void DrawAll(List<IDrawable> shapes)
    {
        foreach (var shape in shapes)
            shape.Draw();
    }
}
