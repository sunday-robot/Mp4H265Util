namespace Mp4H265Util;

static class Mp4BoxReader
{
    public static Mp4Box? Read(BinaryReader br)
    {
        if (br.BaseStream.Position + 8 > br.BaseStream.Length)
            return null;

        uint size = Be.ReadUInt32(br);
        string type = Be.ReadString(br, 4);

        long payloadSize = size - 8;

        Mp4Box box = type switch
        {
            "ftyp" => new FtypBox(),
            "moov" => new ContainerBox(),
            "trak" => new ContainerBox(),
            "mdia" => new ContainerBox(),
            "minf" => new ContainerBox(),
            "stbl" => new ContainerBox(),
            "stsd" => new StsdBox(),
            "hvc1" => new Hvc1Box(),
            "hev1" => new Hvc1Box(),
            _ => new DefaultBox()
        };

        box.Size = size;
        box.Type = type;

        box.DeserializePayload(br, payloadSize);

        return box;
    }

}
