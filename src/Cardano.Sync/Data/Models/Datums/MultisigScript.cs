using System.Formats.Cbor;
using CborSerialization;

namespace Cardano.Sync.Data.Models.Datums;

/*
121_0([_
        121_0([_
            h'a55f28e5ea668602c69f0975a5b51d68be87763360f63277ffe6dd7d',
        ]),
    ]),
*/
[CborSerialize(typeof(MultisigScriptCborConvert))]
public record MultisigScript
(
    Signature Signature,
    MultisigScripts AllOf,
    MultisigScripts AnyOf,
    MultisigScriptAtLeast AtLeast,
    PosixTime Before,
    PosixTime After,
    Script Script
): IDatum;

public class MultisigScriptCborConvert : ICborConvertor<MultisigScript>
{
    public MultisigScript Read(ref CborReader reader)
    {
        var tag = reader.ReadTag();
        if ((int)tag != 121)
        {
            throw new Exception("Invalid tag");
        }
        reader.ReadStartArray();
        var signature = new SignatureCborConvert().Read(ref reader);
        var allOf = new MultisigScriptsCborConvert().Read(ref reader);
        var anyOf = new MultisigScriptsCborConvert().Read(ref reader);
        var atLeast = new MultisigScriptAtLeastCborConvert().Read(ref reader);
        var before = new PosixTimeCborConvert().Read(ref reader);
        var after = new PosixTimeCborConvert().Read(ref reader);
        var script = new ScriptCborConvert().Read(ref reader);
        reader.ReadEndArray();

        return new MultisigScript(signature, allOf, anyOf, atLeast, before, after, script);
    }

    public void Write(ref CborWriter writer, MultisigScript? value)
    {
        if (value is null)
        {
            throw new Exception("MultisigScript is null");
        }
        writer.WriteTag((CborTag)121);
        writer.WriteStartArray(null);
        new SignatureCborConvert().Write(ref writer, value.Signature);
        new MultisigScriptsCborConvert().Write(ref writer, value.AllOf);
        new MultisigScriptsCborConvert().Write(ref writer, value.AnyOf);
        new MultisigScriptAtLeastCborConvert().Write(ref writer, value.AtLeast);
        new PosixTimeCborConvert().Write(ref writer, value.Before);
        new PosixTimeCborConvert().Write(ref writer, value.After);
        new ScriptCborConvert().Write(ref writer, value.Script);
        writer.WriteEndArray();
    }
}