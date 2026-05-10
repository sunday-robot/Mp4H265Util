namespace Mp4H265Util;

public static class Mp4Reader
{
    public static List<Mp4Box> Read(string path)
    {
        using var fs = File.OpenRead(path);
        using var br = new BinaryReader(fs);
        var boxes = new List<Mp4Box>();
        while (br.BaseStream.Position < br.BaseStream.Length)
        {
            var box = Mp4BoxReader.Read(br);
            boxes.Add(box);
        }
        return boxes;
    }
}
