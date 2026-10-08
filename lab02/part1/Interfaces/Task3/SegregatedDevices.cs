namespace Interfaces.Task3;

public interface IPrinter
{
    void Print(string document);
}

public interface IScanner
{
    void Scan(string document);
}

public interface IFax
{
    void Fax(string document);
}

public class Printer : IPrinter
{
    public void Print(string document) => Console.WriteLine($"[Printer] Печать: {document}");
}

public class Scanner : IScanner
{
    public void Scan(string document) => Console.WriteLine($"[Scanner] Сканирование: {document}");
}

public class MultifunctionDevice : IPrinter, IScanner, IFax
{
    public void Print(string document) => Console.WriteLine($"[МФУ] Печать: {document}");
    public void Scan(string document) => Console.WriteLine($"[МФУ] Сканирование: {document}");
    public void Fax(string document) => Console.WriteLine($"[МФУ] Отправка факса: {document}");
}
