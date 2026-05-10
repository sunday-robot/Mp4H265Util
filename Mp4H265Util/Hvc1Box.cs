namespace Mp4H265Util;

public sealed class Hvc1Box : Mp4Box
{
    public byte[] Properties { get; set; } = [];

    public override void ReadProperties(BinaryReader br)
    {
        Properties = Be.ReadBytes(br, 78);
    }

    protected override void WriteProperties(BinaryWriter bw)
    {
        bw.Write(Properties);
    }

    protected override void PrintProperties(string indent)
    {
        PrintProperty(indent, "Properties", BitConverter.ToString(Properties));
    }
}
