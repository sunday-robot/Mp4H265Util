namespace Mp4H265Util;

public sealed class StsdBox : AbstractContainerBox
{
    public byte Version { get; set; }
    public byte[] Flags { get; } = new byte[3];
    public uint EntryCount { get; set; }

    protected override void ReadHeader(BinaryReader br)
    {
        Version = br.ReadByte();
        br.Read(Flags, 0, 3);
        EntryCount = Be.ReadUInt32(br);
    }

    protected override void WriteHeader(BinaryWriter bw)
    {
        bw.Write(Version);
        bw.Write(Flags);
        Be.WriteUInt32(bw, EntryCount);
    }

    protected override string HeaderToString()
    {
        return $"Version={Version}, Flags={BitConverter.ToString(Flags)}, EntryCount={EntryCount}";
    }
}
