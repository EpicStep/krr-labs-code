namespace Interfaces.Task3;

// «Толстый» интерфейс: классы вынуждены реализовывать методы, которые им не нужны
public interface IDevice
{
    void Print(string document);
    void Scan(string document);
    void Fax(string document);
}

public class OldPrinter : IDevice
{
    public void Print(string document) => Console.WriteLine($"[OldPrinter] Печать: {document}");
    public void Scan(string document) { }
    public void Fax(string document) { }
}

public class OldScanner : IDevice
{
    public void Print(string document) { }
    public void Scan(string document) => Console.WriteLine($"[OldScanner] Сканирование: {document}");
    public void Fax(string document) { }
}
