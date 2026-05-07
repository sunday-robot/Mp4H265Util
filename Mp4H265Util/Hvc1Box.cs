namespace Mp4H265Util;

public sealed class Hvc1Box : AbstractContainerBox
{
    public byte[] Header { get; set; } = [];

    protected override void ReadHeader(BinaryReader br)
    {
        Header = br.ReadBytes(78);
    }

    protected override void WriteHeader(BinaryWriter bw)
    {
        bw.Write(Header);
    }

    protected override string HeaderToString()
    {
        return $"Header={BitConverter.ToString(Header)}";
    }
}
