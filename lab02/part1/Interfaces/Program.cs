using Interfaces.Task1;
using Interfaces.Task2;
using Interfaces.Task3;
using Interfaces.Task4;

Console.OutputEncoding = System.Text.Encoding.UTF8;

Console.WriteLine("=== Задание 1. Базовые интерфейсы ===");
var point = new Point(2, 3);
Console.WriteLine($"Точка до перемещения: {point}");
IMovable movable = point;
movable.Move(5, -1);
Console.WriteLine($"Точка после Move(5, -1): {point}");

var drawables = new List<IDrawable> { new Interfaces.Task1.Circle(3), new Interfaces.Task1.Rectangle(4, 2) };
Canvas.DrawAll(drawables);

Console.WriteLine();
Console.WriteLine("=== Задание 2. Интерфейсы и наследование ===");
var shapes = new List<IShape> { new Interfaces.Task2.Circle(1), new Interfaces.Task2.Rectangle(3, 4), new Cube(2) };
foreach (var shape in shapes)
    ShapeInfo.PrintShapeInfo(shape);

Console.WriteLine();
Console.WriteLine("=== Задание 3. Разделение интерфейсов (ISP) ===");
IDevice oldPrinter = new OldPrinter();
oldPrinter.Print("отчёт.docx");
oldPrinter.Scan("отчёт.docx");   // ничего не делает – пустой метод
Console.WriteLine("После разделения интерфейсов:");
IPrinter printer = new Printer();
IScanner scanner = new Scanner();
var mfu = new MultifunctionDevice();
printer.Print("отчёт.docx");
scanner.Scan("паспорт.pdf");
mfu.Print("договор.pdf");
mfu.Scan("договор.pdf");
mfu.Fax("договор.pdf");

Console.WriteLine();
Console.WriteLine("=== Задание 4. Интерфейсы и полиморфизм ===");
Checkout.ProcessPayment(new CreditCard("4276123412345678"), 1500m);
Checkout.ProcessPayment(new Cash(), 320.50m);
Checkout.ProcessPayment(new Cash(), 0m);

Worker.DoWork(new ConsoleLogger());
var logPath = Path.Combine(AppContext.BaseDirectory, "work.log");
File.Delete(logPath);
Worker.DoWork(new FileLogger(logPath));
Console.WriteLine($"Содержимое файла {Path.GetFileName(logPath)}:");
Console.Write(File.ReadAllText(logPath));
