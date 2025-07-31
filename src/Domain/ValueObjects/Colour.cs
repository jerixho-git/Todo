using System.Xml.Linq;

namespace Todo_App.Domain.ValueObjects;

public class Colour : ValueObject
{
    static Colour()
    {
    }

    private Colour()
    {
    }

    public string Name { get; }
    public string Code { get; private set; } = "#000000";

    private Colour(string name, string code)
    {
        Name = name;
        Code = code;
    }

    public static Colour From(string code)
    {
        var colour = new Colour { Code = code };

        if (!SupportedColours.Contains(colour))
        {
            throw new UnsupportedColourException(code);
        }

        return colour;
    }

   public static Colour White => new(nameof(White), "#FFFFFF");
    public static Colour Red => new(nameof(Red), "#FF5733");
    public static Colour Orange => new(nameof(Orange), "#FFC300");
    public static Colour Yellow => new(nameof(Yellow), "#FFFF66");
    public static Colour Green => new(nameof(Green), "#CCFF99");
    public static Colour Blue => new(nameof(Blue), "#6666FF");
    public static Colour Purple => new(nameof(Purple), "#9966CC");
    public static Colour Grey => new(nameof(Grey), "#999999");


    public static implicit operator string(Colour colour)
    {
        return colour.ToString();
    }

    public static explicit operator Colour(string code)
    {
        return From(code);
    }

    public override string ToString()
    {
        return Code;
    }

    protected static IEnumerable<Colour> SupportedColours
    {
        get
        {
            yield return White;
            yield return Red;
            yield return Orange;
            yield return Yellow;
            yield return Green;
            yield return Blue;
            yield return Purple;
            yield return Grey;
        }
    }
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Code;
    }

    public static IEnumerable<Colour> GetSupportedColours()
    {
        return SupportedColours;
    }
    public static string GetNameFromCode(string code) =>
        SupportedColours.FirstOrDefault(c => c.Code == code)?.Name ?? "Unknown";

}
