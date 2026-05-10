namespace Mp4H265Util;

public static class Mp4BoxReader
{
    public static Mp4Box Read(BinaryReader br)
    {
        uint size = Be.ReadUInt32(br);
        string type = Be.ReadString(br, 4);
        var end = br.BaseStream.Position + size - 8;
        //Console.WriteLine($"{indent}{type}({size})");

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
            "hvcC" => new HvcCBox(),
            _ => new DefaultBox()
        };
        box.Size = size;
        box.Type = type;
        box.ReadProperties(br);
        while (br.BaseStream.Position < end)
        {
            var child = Mp4BoxReader.Read(br)!;
            box.Children.Add(child);
        }
        if (br.BaseStream.Position != end)
            throw new InvalidDataException($"Box {type} has invalid size.");

        return box;
    }
}
