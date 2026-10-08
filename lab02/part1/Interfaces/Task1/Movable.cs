namespace Interfaces.Task1;

public interface IMovable
{
    void Move(int x, int y);
}

public class Point : IMovable
{
    public int X { get; private set; }
    public int Y { get; private set; }

    public Point(int x, int y)
    {
        X = x;
        Y = y;
    }

    // Сдвиг точки на (x, y)
    public void Move(int x, int y)
    {
        X += x;
        Y += y;
    }

    public override string ToString() => $"({X}, {Y})";
}
