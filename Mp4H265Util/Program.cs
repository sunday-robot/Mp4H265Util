namespace Mp4H265Util;

class Program
{
    static void Main(string[] args)
    {
        if (args.Length != 2)
        {
            Console.WriteLine("Usage: Mp4H265Util <input.mp4> <output.mp4>");
            return;
        }

        var input = args[0];
        var output = args[1];

        var boxes = Mp4Reader.Read(input);
        Mp4Printer.Print(boxes);
        Mp4Writer.Write(output, boxes);

        Console.WriteLine("Done.");
    }
}
