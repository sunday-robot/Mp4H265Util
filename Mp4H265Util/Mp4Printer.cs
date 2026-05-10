namespace Mp4H265Util;

public static class Mp4Printer
{
    public static void Print(List<Mp4Box> boxes)
    {
        for (int i = 0; i < boxes.Count; i++)
            boxes[i].Print("", i);
    }
}
