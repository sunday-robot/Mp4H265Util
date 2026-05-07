using System.Text;

namespace Mp4H265Util;

class FtypBox : Mp4Box
{
    public string MajorBrand { get; set; } = "";
    public uint MinorVersion { get; set; }
    public List<string> CompatibleBrands { get; set; } = [];

    public override void DeserializePayload(BinaryReader br, long payloadSize)
    {
        MajorBrand = Encoding.ASCII.GetString(br.ReadBytes(4));
        MinorVersion = Be.ReadUInt32(br);

        long remaining = payloadSize - 8;

        while (remaining >= 4)
        {
            string brand = Encoding.ASCII.GetString(br.ReadBytes(4));
            CompatibleBrands.Add(brand);
            remaining -= 4;
        }
    }

    public override string PayloadToString()
    {
        return $"MajorBrand: {MajorBrand}, MinorVersion: {MinorVersion}, CompatibleBrands: [{string.Join(", ", CompatibleBrands)}]";
    }

    public override void SerializePayload(BinaryWriter bw)
    {
        bw.Write(Encoding.ASCII.GetBytes(MajorBrand));
        Be.WriteUInt32(bw, MinorVersion);

        foreach (var brand in CompatibleBrands)
            bw.Write(Encoding.ASCII.GetBytes(brand));
    }
}
