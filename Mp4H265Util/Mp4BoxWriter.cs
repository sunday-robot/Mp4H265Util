using System.Text;

namespace Mp4H265Util;

public static class Mp4BoxWriter
{
    public static void WriteBox(BinaryWriter bw, Mp4Box box)
    {
        // Write box size and type
        Be.WriteUInt32(bw, box.Size);
        bw.Write(Encoding.ASCII.GetBytes(box.Type));
        // Write box payload
        box.SerializePayload(bw);
    }
}
