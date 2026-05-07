namespace Mp4H265Util;

public sealed class ContainerBox : Mp4Box
{
    public override void DeserializePayload(BinaryReader br, long payloadSize)
    {
        long end = br.BaseStream.Position + payloadSize;

        while (br.BaseStream.Position + 8 <= end)
        {
            var child = Mp4BoxReader.Read(br);
            if (child == null) break;
            Children.Add(child);
        }

        // 万一読み残しがあればスキップ
        if (br.BaseStream.Position < end)
            br.BaseStream.Position = end;
    }

    public override void SerializePayload(BinaryWriter bw)
    {
        foreach (var child in Children)
            Mp4BoxWriter.WriteBox(bw, child);
    }

    public override string PayloadToString()
    {
        return $"Children: {Children.Count}";
    }
}
