namespace Mp4H265Util;

public abstract class Mp4Box
{
    public uint Size { get; set; }
    public string Type { get; set; } = "";
    public List<Mp4Box> Children { get; set; } = [];

    public abstract void ReadProperties(BinaryReader br);

    public void Write(BinaryWriter bw)
    {
        Be.WriteUInt32(bw, Size);
        Be.WriteString(bw, Type);
        WriteProperties(bw);
        foreach (var child in Children)
            child.Write(bw);
    }

    protected abstract void WriteProperties(BinaryWriter bw);

    public void Print(string indent, int index)
    {
        Console.WriteLine($"{indent}[{index}]{Type}({Size} bytes)");
        PrintProperties(indent);
        for (int i = 0; i < Children.Count; i++)
        {
            Children[i].Print(indent + "  ", i);
        }
    }

    protected abstract void PrintProperties(string indent);

    protected static void PrintProperty(string indent, string name, string value)
    {
        Console.WriteLine($"{indent}    {name}: {value}");
    }
}
