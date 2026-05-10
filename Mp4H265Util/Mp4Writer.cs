namespace Mp4H265Util;

public static  class Mp4Writer
{
    public static void Write(string path, List<Mp4Box> boxes)
    {
        using var fs = File.Create(path);
        using var bw = new BinaryWriter(fs);
        foreach (var box in boxes)
            box.Write(bw);
    }
}
