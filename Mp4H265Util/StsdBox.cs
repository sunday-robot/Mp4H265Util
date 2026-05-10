namespace Mp4H265Util;

public sealed class StsdBox : Mp4Box
{
    public byte Version { get; set; }
    public byte[] Flags { get; set; } = [];
    public uint EntryCount { get; set; }

    public override void ReadProperties(BinaryReader br)
    {
        Version = Be.ReadByte(br);
        Flags = Be.ReadBytes(br, 3);
        EntryCount = Be.ReadUInt32(br);
    }

    protected override void WriteProperties(BinaryWriter bw)
    {
        bw.Write(Version);
        bw.Write(Flags);
        Be.WriteUInt32(bw, EntryCount);
    }

    protected override void PrintProperties(string indent)
    {
        PrintProperty(indent, "Version", Version.ToString());
        PrintProperty(indent, "Flags", BitConverter.ToString(Flags));
        PrintProperty(indent, "EntryCount", EntryCount.ToString());
    }
}
