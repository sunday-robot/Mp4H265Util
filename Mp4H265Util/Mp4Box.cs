namespace Mp4H265Util;

public abstract class Mp4Box
{
    public uint Size { get; set; }
    public string Type { get; set; } = "";
    public List<Mp4Box> Children { get; set; } = [];

    public abstract void DeserializePayload(BinaryReader br, long payloadSize);

    public abstract void SerializePayload(BinaryWriter bw);

    public void Pp(string indent = "")
    {
        Console.WriteLine($"{indent}Box: {Type}, Size: {Size}, Payload: {PayloadToString()}");
        foreach (var child in Children)
            child.Pp(indent + "  ");
    }

    public abstract string PayloadToString();
}
