namespace Mp4H265Util;

static class Mp4BoxReader
{
    public static Mp4Box? Read(BinaryReader br)
    {
        if (br.BaseStream.Position + 8 > br.BaseStream.Length)
            return null;

        uint size = Be.ReadUInt32(br);
        string type = new(br.ReadChars(4));

        long payloadSize = size - 8;

        Mp4Box box = type switch
        {
            "ftyp" => new FtypBox(),
            _ => new DefaultBox()
        };

        box.Size = size;
        box.Type = type;

        box.DeserializePayload(br, payloadSize);

        return box;
    }

}
