namespace Mp4H265Util;

public sealed class HvcCBox : Mp4Box
{
    public sealed class NalArray
    {
        public sealed class Nalu
        {
            // nalUnitLength 0, 2byte
            public byte[] NalUnits { get; set; } = []; // 2

            public static Nalu Read(BinaryReader br)
            {
                var nalu = new Nalu();
                var nalUnitLength = Be.ReadUInt16(br);           // 0, 2byte
                nalu.NalUnits = Be.ReadBytes(br, nalUnitLength);    // 2
                return nalu;
            }

            public void Write(BinaryWriter bw)
            {
                Be.WriteUInt16(bw, (ushort)NalUnits.Length);    // 0, 2byte
                bw.Write(NalUnits);                             // 2
            }

            public void Print(string indent)
            {
                PrintProperty(indent, "nalUnitLength", NalUnits.Length.ToString());
                PrintProperty(indent, "nalUnits", BitConverter.ToString(NalUnits));
            }
        }

        byte _b0;
        public byte ArrayCompleteness   // 0:X___-____
        {
            get { return (byte)((_b0 >> 7) & 0x01); }
            set { _b0 = (byte)((_b0 & 0x7F) | ((value & 0x01) << 7)); }
        }
        // reserved                        0:_X__-____
        public byte NalUnitType         // 0:__XX-XXXX
        {
            get { return (byte)(_b0 & 0x3F); }
            set { _b0 = (byte)((_b0 & 0xC0) | (value & 0x3F)); }
        }
        // numNalus                        1, 2byte
        public List<Nalu> Nalus { get; } = []; // 2

        public static NalArray Read(BinaryReader br)
        {
            var nalArray = new NalArray();
            nalArray._b0 = Be.ReadByte(br);     // 0
            var numNalus = Be.ReadUInt16(br);   // 1, 2byte
            for (int i = 0; i < numNalus; i++)
                nalArray.Nalus.Add(Nalu.Read(br));
            return nalArray;
        }

        public void Write(BinaryWriter bw)
        {
            bw.Write(_b0);                              // 0
            Be.WriteUInt16(bw, (ushort)Nalus.Count);    // 1, 2byte
            foreach (var nalu in Nalus)
                nalu.Write(bw);
        }

        public void Print(string indent)
        {
            PrintProperty(indent, "arrayCompleteness", ArrayCompleteness.ToString());
            PrintProperty(indent, "nalUnitType", NalUnitType.ToString());
            PrintProperty(indent, "numNalus", Nalus.Count.ToString());
            for (int i = 0; i < Nalus.Count; i++)
            {
                PrintProperty(indent, $"nalUs[{i}]", ":");
                Nalus[i].Print(indent + "    ");
            }
        }
    }

    public byte ConfigurationVersion { get; set; }  // 0
    byte _b1;
    public byte GeneralProfileSpace // 1:XX__-____
    {
        get { return (byte)((_b1 >> 6) & 0x03); }
        set { _b1 = (byte)((_b1 & 0x3F) | ((value & 0x03) << 6)); }
    }
    public byte GeneralTierFlag     // 1:__X_-____
    {
        get { return (byte)((_b1 >> 5) & 0x01); }
        set { _b1 = (byte)((_b1 & 0xDF) | ((value & 0x01) << 5)); }
    }
    public byte GeneralProfileIdc   // 1:___X-XXXX
    {
        get { return (byte)(_b1 & 0x1F); }
        set { _b1 = (byte)((_b1 & 0xE0) | (value & 0x1F)); }
    }
    public uint GeneralProfileCompatibilityFlags { get; set; }  // 2, 4byte
    public ulong GeneralConstraintIndicatorFlags { get; set; }  // 6, 6byte
    public byte GeneralLevelIdc { get; set; }   // 12
    ushort _w13;
    // reserved                                13:XXXX-____-____-____
    public ushort MinSpatialSegmentationIdc // 13:____-XXXX-XXXX-XXXX
    {
        get { return (ushort)(_w13 & 0x0FFF); }
        set { _w13 = (ushort)((_w13 & 0xF000) | (value & 0x0FFF)); }
    }
    byte _b15;
    // reserved                    15:XXXX-XX__
    public byte ParallelismType // 15:____-__XX
    {
        get { return (byte)(_b15 & 0x03); }
        set { _b15 = (byte)((_b15 & 0xFC) | (value & 0x03)); }
    }   // 15
    byte _b16;
    // reserved                    16:XXXX-XX__
    public byte ChromaFormat    // 16:____-__XX
    {
        get { return (byte)(_b16 & 0x03); }
        set { _b16 = (byte)((_b16 & 0xFC) | (value & 0x03)); }
    }
    byte _b17;
    // reserved                        17:XXXX-X___
    public byte BitDepthLumaMinus8  // 17:____-_XXX
    {
        get { return (byte)(_b17 & 0x07); }
        set { _b17 = (byte)((_b17 & 0xF8) | (value & 0x07)); }
    }
    byte _b18;
    // reserved                            18:XXXX-X___
    public byte BitDepthChromaMinus8    // 18:____-_XXX
    {
        get { return (byte)(_b18 & 0x07); }
        set { _b18 = (byte)((_b18 & 0xF8) | (value & 0x07)); }
    }
    public ushort AvgFrameRate { get; set; }        // 19, 2byte
    byte _b21;
    public byte ConstantFrameRate   // 21:XX__-____
    {
        get { return (byte)((_b21 >> 6) & 0x03); }
        set { _b21 = (byte)((_b21 & 0x3F) | ((value & 0x03) << 6)); }
    }
    public byte NumTemporalLayers   // 21:__XX-X___
    {
        get { return (byte)((_b21 >> 3) & 0x07); }
        set { _b21 = (byte)((_b21 & 0xC7) | ((value & 0x07) << 3)); }
    }
    public byte TemporalIdNested    // 21:____-_X__
    {
        get { return (byte)((_b21 >> 2) & 0x01); }
        set { _b21 = (byte)((_b21 & 0xFB) | ((value & 0x01) << 2)); }
    }
    public byte LengthSizeMinusOne  // 21:____-__XX
    {
        get { return (byte)(_b21 & 0x03); }
        set { _b21 = (byte)((_b21 & 0xFC) | (value & 0x03)); }
    }
    // numOfArrays                                 22
    public List<NalArray> Arrays { get; } = []; // 23

    public override void ReadProperties(BinaryReader br)
    {
        ConfigurationVersion = Be.ReadByte(br);                 // 0
        _b1 = br.ReadByte();                                    // 1
        GeneralProfileCompatibilityFlags = Be.ReadUInt32(br);   // 2, 4byte
        GeneralConstraintIndicatorFlags = Be.ReadUInt48(br);    // 6, 6byte
        GeneralLevelIdc = Be.ReadByte(br);                      // 12
        _w13 = Be.ReadUInt16(br);                               // 13, 2byte
        _b15 = Be.ReadByte(br);                                 // 15
        _b16 = Be.ReadByte(br);                                 // 16
        _b17 = Be.ReadByte(br);                                 // 17
        _b18 = Be.ReadByte(br);                                 // 18
        AvgFrameRate = Be.ReadUInt16(br);                       // 19, 2byte
        _b21 = Be.ReadByte(br);                                 // 21
        byte numArrays = Be.ReadByte(br);                       // 22
        for (int i = 0; i < numArrays; i++)
            Arrays.Add(NalArray.Read(br));
    }

    protected override void WriteProperties(BinaryWriter bw)
    {
        bw.Write(ConfigurationVersion);                         // 0
        bw.Write(_b1);                                          // 1
        Be.WriteUInt32(bw, GeneralProfileCompatibilityFlags);   // 2, 4byte
        Be.WriteUInt48(bw, GeneralConstraintIndicatorFlags);    // 6, 6byte
        bw.Write(GeneralLevelIdc);                              // 12
        Be.WriteUInt16(bw, _w13);                               // 13, 2byte
        bw.Write(_b15);                                         // 15
        bw.Write(_b16);                                         // 16
        bw.Write(_b17);                                         // 17
        bw.Write(_b18);                                         // 18
        Be.WriteUInt16(bw, AvgFrameRate);                       // 19, 2byte
        bw.Write(_b21);                                         // 21
        bw.Write((byte)Arrays.Count);                           // 22
        foreach (var array in Arrays)
            array.Write(bw);
    }

    protected override void PrintProperties(string indent)
    {
        PrintProperty(indent, "configurationVersion", $"0x{ConfigurationVersion:X2}");
        PrintProperty(indent, "generalProfileSpace", GeneralProfileSpace.ToString());
        PrintProperty(indent, "generalTierFlag", GeneralTierFlag.ToString());
        PrintProperty(indent, "generalProfileIdc", GeneralProfileIdc.ToString());
        PrintProperty(indent, "generalProfileCompatibilityFlags", $"0x{GeneralProfileCompatibilityFlags:X8}");
        PrintProperty(indent, "generalConstraintIndicatorFlags", $"0x{GeneralConstraintIndicatorFlags:X12}");
        PrintProperty(indent, "generalLevelIdc", GeneralLevelIdc.ToString());
        PrintProperty(indent, "minSpatialSegmentationIdc", MinSpatialSegmentationIdc.ToString());
        PrintProperty(indent, "parallelismType", ParallelismType.ToString());
        PrintProperty(indent, "chromaFormat", ChromaFormat.ToString());
        PrintProperty(indent, "bitDepthLumaMinus8", BitDepthLumaMinus8.ToString());
        PrintProperty(indent, "bitDepthChromaMinus8", BitDepthChromaMinus8.ToString());
        PrintProperty(indent, "avgFrameRate", AvgFrameRate.ToString());
        PrintProperty(indent, "constantFrameRate", ConstantFrameRate.ToString());
        PrintProperty(indent, "numTemporalLayers", NumTemporalLayers.ToString());
        PrintProperty(indent, "temporalIdNested", TemporalIdNested.ToString());
        PrintProperty(indent, "lengthSizeMinusOne", LengthSizeMinusOne.ToString());
        PrintProperty(indent, "numArrays", Arrays.Count.ToString());
        for (int i = 0; i < Arrays.Count; i++)
        {
            PrintProperty(indent, $"arrays[{i}]", ":");
            Arrays[i].Print(indent + "    ");
        }
    }
}
