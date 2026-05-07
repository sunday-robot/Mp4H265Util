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

        var mp4 = Mp4File.Deserialize(input);
        mp4.Serialize(output);

        Console.WriteLine("Done.");
    }
}
