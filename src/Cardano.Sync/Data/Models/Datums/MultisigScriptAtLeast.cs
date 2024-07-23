using System.Formats.Cbor;
using CborSerialization;

namespace Cardano.Sync.Data.Models.Datums;

[CborSerialize(typeof(MultisigScriptAtLeastCborConvert))]
public record MultisigScriptAtLeast
(
    ulong Required,
    MultisigScripts Scripts
): IDatum;

public class MultisigScriptAtLeastCborConvert : ICborConvertor<MultisigScriptAtLeast>
{
    public MultisigScriptAtLeast Read(ref CborReader reader)
    {
        throw new NotImplementedException();
    }

    public void Write(ref CborWriter writer, MultisigScriptAtLeast value)
    {
        throw new NotImplementedException();
    }
}