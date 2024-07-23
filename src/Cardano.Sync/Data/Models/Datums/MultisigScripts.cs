using System.Formats.Cbor;
using CborSerialization;

namespace Cardano.Sync.Data.Models.Datums;

[CborSerialize(typeof(MultisigScriptsCborConvert))]
public record MultisigScripts(List<MultisigScript> Scripts): IDatum;

public class MultisigScriptsCborConvert : ICborConvertor<MultisigScripts>
{
    public MultisigScripts Read(ref CborReader reader)
    {
        throw new NotImplementedException();
    }

    public void Write(ref CborWriter writer, MultisigScripts value)
    {
        throw new NotImplementedException();
    }
}