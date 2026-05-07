namespace Mp4H265Util;

public sealed class ContainerBox : AbstractContainerBox
{
    protected override void ReadHeader(BinaryReader br) { }

    protected override void WriteHeader(BinaryWriter bw) { }

    protected override string HeaderToString() => "";
}
