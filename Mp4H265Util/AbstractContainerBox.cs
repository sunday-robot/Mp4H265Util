namespace Mp4H265Util;

public abstract class AbstractContainerBox : Mp4Box
{
    public override void DeserializePayload(BinaryReader br, long payloadSize)
    {
        long start = br.BaseStream.Position;

        ReadHeader(br);

        long consumed = br.BaseStream.Position - start;
        long end = start + payloadSize;

        while (br.BaseStream.Position + 8 <= end)
        {
            var child = Mp4BoxReader.Read(br);
            if (child == null) break;
            Children.Add(child);
        }

        if (br.BaseStream.Position < end)
            br.BaseStream.Position = end;
    }

    public override void SerializePayload(BinaryWriter bw)
    {
        WriteHeader(bw);

        foreach (var child in Children)
            Mp4BoxWriter.WriteBox(bw, child);
    }

    protected abstract void ReadHeader(BinaryReader br);

    protected abstract void WriteHeader(BinaryWriter bw);

    protected abstract string HeaderToString();

    public override string PayloadToString()
    {
        var h = HeaderToString();
        if (h.Length == 0)
            return $"Children={Children.Count}";
        else
            return $"{h}, Children={Children.Count}";
    }
}
