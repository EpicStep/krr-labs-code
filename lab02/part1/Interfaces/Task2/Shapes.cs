namespace Interfaces.Task2;

public interface IShape
{
    double GetArea();
    double GetPerimeter();
}

public interface I3DShape : IShape
{
    double GetVolume();
}

public class Circle : IShape
{
    private readonly double _radius;

    public Circle(double radius) => _radius = radius;

    public double GetArea() => Math.PI * _radius * _radius;
    public double GetPerimeter() => 2 * Math.PI * _radius;
}

public class Rectangle : IShape
{
    private readonly double _width;
    private readonly double _height;

    public Rectangle(double width, double height)
    {
        _width = width;
        _height = height;
    }

    public double GetArea() => _width * _height;
    public double GetPerimeter() => 2 * (_width + _height);
}

public class Cube : I3DShape
{
    private readonly double _side;

    public Cube(double side) => _side = side;

    // Для куба «площадь» – площадь полной поверхности, «периметр» – сумма длин рёбер
    public double GetArea() => 6 * _side * _side;
    public double GetPerimeter() => 12 * _side;
    public double GetVolume() => _side * _side * _side;
}

public static class ShapeInfo
{
    public static void PrintShapeInfo(IShape shape)
    {
        Console.Write($"{shape.GetType().Name}: площадь = {shape.GetArea():F2}, периметр = {shape.GetPerimeter():F2}");
        if (shape is I3DShape solid)
            Console.Write($", объём = {solid.GetVolume():F2}");
        Console.WriteLine();
    }
}
