namespace Levitator_Slackness;

public class Block
{
    public Block(string partName, string colorName, int quantity)
    {
        PartName = partName;
        ColorName = colorName;
        Quantity = quantity;
    }

    public string PartName 
    { 
        get; 
        set; 
    }
    
    public string ColorName 
    { 
        get; 
        set; 
    }

    public int Quantity
    {
        get;
        set;
    }

    public string PartColor => $"({ColorName})".PadRight(16, ' ') + $"{PartName}" ;
    
    public override string ToString() => $"{PartName}\n" + $"{ColorName}\n";
}