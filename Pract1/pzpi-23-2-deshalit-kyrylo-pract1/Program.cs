// See https://aka.ms/new-console-template for more information
public interface ITextComponent
{
    string GetText();
}

public class ConcreteTextComponent : ITextComponent
{
    private readonly string _text;

    public ConcreteTextComponent(string text)
    {
        _text = text;
    }

    public string GetText() => _text;
}

public abstract class TextDecorator : ITextComponent
{
    protected readonly ITextComponent _component;

    protected TextDecorator(ITextComponent component)
    {
        _component = component;
    }

    public virtual string GetText() => _component.GetText();
}

public class CaesarDecorator : TextDecorator
{
    private const int Shift = 3;

    public CaesarDecorator(ITextComponent component) : base(component) { }

    public override string GetText()
    {
        string text = base.GetText();
        char ShiftChar(char c)
        {
            if (!char.IsLetter(c)) return c;
            char offset = char.IsUpper(c) ? 'A' : 'a';
            return (char)((c - offset + Shift) % 26 + offset);
        }

        return new string(Array.ConvertAll(text.ToCharArray(), ShiftChar));
    }
}

public class ReverseDecorator : TextDecorator
{
    public ReverseDecorator(ITextComponent component) : base(component) { }

    public override string GetText()
    {
        char[] arr = base.GetText().ToCharArray();
        Array.Reverse(arr);
        return new string(arr);
    }
}

class Program
{
    static void Main()
    {
        ITextComponent text = new ConcreteTextComponent("HelloWorld");

        Console.WriteLine("Text before encyption: " + text.GetText());

        var caesarText = new CaesarDecorator(text);
        var reverseText = new ReverseDecorator(caesarText);

        Console.WriteLine("Text after encyption: " + reverseText.GetText());
    }
}