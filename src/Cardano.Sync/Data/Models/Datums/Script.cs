using System.Formats.Cbor;
using CborSerialization;

namespace Cardano.Sync.Data.Models.Datums;

[CborSerialize(typeof(ScriptCborConvert))]
public record Script(byte[] ScriptHash): IDatum;

public class ScriptCborConvert : ICborConvertor<Script>
{
    public Script Read(ref CborReader reader)
    {
        throw new NotImplementedException();
    }

    public void Write(ref CborWriter writer, Script value)
    {
        throw new NotImplementedException();
    }
}