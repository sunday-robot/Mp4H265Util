namespace Mp4H265Util;

public sealed  class DefaultBox : Mp4Box {
    byte[]? _payload = null;

    public override void DeserializePayload(BinaryReader br, long payloadSize)
    {
        _payload = br.ReadBytes((int)payloadSize);
    }

    public override void SerializePayload(BinaryWriter bw)
    {
        bw.Write(_payload!);
    }

    public override string PayloadToString()
    {
        return $"size = {(_payload!).Length}";
    }
}
