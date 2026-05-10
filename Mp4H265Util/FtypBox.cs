using System.Text;

namespace Mp4H265Util;

class FtypBox : Mp4Box
{
    public string MajorBrand { get; set; } = "";
    public uint MinorVersion { get; set; }
    public List<string> CompatibleBrands { get; set; } = [];

    public override void ReadProperties(BinaryReader br)
    {
        MajorBrand = Be.ReadString(br, 4);
        MinorVersion = Be.ReadUInt32(br);
        var remaining = Size - 16;
        if (remaining % 4 != 0)
            throw new Exception();
        for (; remaining > 0; remaining -= 4)
        {
            var brand = Be.ReadString(br, 4);
            CompatibleBrands.Add(brand);
        }
    }

    protected override void WriteProperties(BinaryWriter bw)
    {
        Be.WriteString(bw, MajorBrand);
        Be.WriteUInt32(bw, MinorVersion);
        foreach (var brand in CompatibleBrands)
            Be.WriteString(bw, brand);
    }

    protected override void PrintProperties(string indent)
    {
        PrintProperty(indent, "MajorBrand", MajorBrand);
        PrintProperty(indent, "MinorVersion", MinorVersion.ToString());
        for (int i = 0; i < CompatibleBrands.Count; i++)
            PrintProperty(indent, $"CompatibleBrand[{i}]", CompatibleBrands[i]);
    }
}
