using System.Text;

namespace Mp4H265Util;

public static class Be
{
    public static uint ReadUInt32(BinaryReader br)
    {
        var b = br.ReadBytes(4);
        return (uint)(b[0] << 24 | b[1] << 16 | b[2] << 8 | b[3]);
    }

    public static void WriteUInt32(BinaryWriter bw, uint value)
    {
        bw.Write(new byte[] {
            (byte)(value >> 24),
            (byte)(value >> 16),
            (byte)(value >> 8),
            (byte)value
        });
    }

    public static string ReadString(BinaryReader br, int length)
    {
        var s = Encoding.ASCII.GetString(br.ReadBytes(length));
        return s;
    }

    public static void WriteString(BinaryWriter bw, string value)
    {
        var bytes = Encoding.ASCII.GetBytes(value);
        bw.Write(bytes);
    }
}
