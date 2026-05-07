using System.Text;

namespace Mp4H265Util;

public static class Mp4BoxWriter
{
    public static void WriteBox(BinaryWriter bw, Mp4Box box)
    {
        Be.WriteUInt32(bw, box.Size);
        Be.WriteString(bw, box.Type);
        box.SerializePayload(bw);
    }
}
