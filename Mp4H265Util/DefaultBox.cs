namespace Mp4H265Util;

/// <summary>
/// とりあえずのMp4Boxクラス。プロパティ群、子Boxについては、まとめてpayloadというバイト配列とする。
/// </summary>
public sealed class DefaultBox : Mp4Box
{
    byte[]? _payload;

    public override void ReadProperties(BinaryReader br)
    {
        _payload = Be.ReadBytes(br, Size - 8);
    }

    protected override void WriteProperties(BinaryWriter bw)
    {
        bw.Write(_payload!);
    }

    protected override void PrintProperties(string indent)
    {
        PrintProperty(indent, "(not implemented)", "");
    }
}
