using System.Formats.Cbor;
using CborSerialization;

namespace Cardano.Sync.Data.Models.Datums;

[CborSerialize(typeof(PosixTimeCborConvert))]
public record PosixTime(ulong Time): IDatum;

public class PosixTimeCborConvert : ICborConvertor<PosixTime>
{
    public PosixTime Read(ref CborReader reader)
    {
        throw new NotImplementedException();
    }

    public void Write(ref CborWriter writer, PosixTime value)
    {
        throw new NotImplementedException();
    }
}