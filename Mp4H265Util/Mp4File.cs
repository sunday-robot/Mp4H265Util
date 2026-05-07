namespace Mp4H265Util;

class Mp4File
{
    public List<Mp4Box> Boxes { get; set; } = [];

    public static Mp4File Deserialize(string path)
    {
        using var fs = File.OpenRead(path);
        using var br = new BinaryReader(fs);

        var file = new Mp4File();

        while (fs.Position < fs.Length)
        {
            var box = Mp4BoxReader.Read(br);
            if (box == null) break;

            file.Boxes.Add(box);

            box.Pp();
        }

        return file;
    }

    public void Serialize(string path)
    {
        using var fs = File.Create(path);
        using var bw = new BinaryWriter(fs);

        foreach (var box in Boxes)
        {
            Mp4BoxWriter.WriteBox(bw, box);
        }
    }
}
