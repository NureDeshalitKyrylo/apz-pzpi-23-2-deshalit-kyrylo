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

//Create an example of Decorator pattern usage with C#. 
//You have to create base ITextComponent interface, 
//ConcreteTextComponent implementation, EncryptDecorator 
//abstract class with basic encrypt and decrypt methods,
//and its two subclasses: CaesarDecorator that uses Caesar
//cipher with shift by 3 and Reverse Decorator that reverses given string.
//Think carefully, write concise code and don't leave any redundant comments

//Now rewrite the code for a more classic approach where data only flows 
//one way through the decorator chain, remove decryption functionality

//Change static void Main so the user can see original text 
//and text after encryption in the console output
