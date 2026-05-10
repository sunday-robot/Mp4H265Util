using System.Text;

namespace Mp4H265Util;

public static class Be
{
    public static byte[] ReadBytes(BinaryReader br, uint length)
    {
        var bytes = br.ReadBytes((int)length);
        if (bytes.Length != length)
            throw new EndOfStreamException("Unexpected end of stream while reading bytes.");
        return bytes;
    }

    public static byte ReadByte(BinaryReader br)
    {
        var bytes = ReadBytes(br, 1);
        return bytes[0];
    }

    public static ushort ReadUInt16(BinaryReader br)
    {
        var b = ReadBytes(br, 2);
        return (ushort)(b[0] << 8 | b[1]);
    }

    public static uint ReadUInt32(BinaryReader br)
    {
        var b = ReadBytes(br, 4);
        return (uint)(b[0] << 24 | b[1] << 16 | b[2] << 8 | b[3]);
    }

    public static ulong ReadUInt48(BinaryReader br)
    {
        var b = ReadBytes(br, 6);
        return ((ulong)b[0] << 40) | ((ulong)b[1] << 32) | ((ulong)b[2] << 24) | ((ulong)b[3] << 16) | ((ulong)b[4] << 8) | b[5];
    }

    public static void WriteUInt16(BinaryWriter bw, ushort value)
    {
        bw.Write([(byte)(value >> 8), (byte)value]);
    }

    public static void WriteUInt32(BinaryWriter bw, uint value)
    {
        bw.Write([(byte)(value >> 24), (byte)(value >> 16), (byte)(value >> 8), (byte)value]);
    }

    public static void WriteUInt48(BinaryWriter bw, ulong value)
    {
        bw.Write([(byte)(value >> 40), (byte)(value >> 32), (byte)(value >> 24), (byte)(value >> 16), (byte)(value >> 8), (byte)value]);
    }

    public static string ReadString(BinaryReader br, uint length)
    {
        var bytes = ReadBytes(br, length);
        return Encoding.ASCII.GetString(bytes);
    }

    public static void WriteString(BinaryWriter bw, string value)
    {
        var bytes = Encoding.ASCII.GetBytes(value);
        bw.Write(bytes);
    }
}
