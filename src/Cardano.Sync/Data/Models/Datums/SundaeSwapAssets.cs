using System.Formats.Cbor;
using CborSerialization;

namespace Cardano.Sync.Data.Models.Datums;

/*
    [_
        [_ h'', h''],
        [_
            h'c48cbb3d5e57ed56e276bc45f99ab39abe94e6cd7ac39fb402da47ad',
            h'0014df105553444d',
        ],
    ],
*/

[CborSerialize(typeof(SundaeSwapAssetsCborConvert))]
public record SundaeSwapAssets(AssetClass AssetX, AssetClass AssetY): IDatum;

public class SundaeSwapAssetsCborConvert : ICborConvertor<SundaeSwapAssets>
{
    public SundaeSwapAssets Read(ref CborReader reader)
    {
        reader.ReadStartArray();

        reader.ReadStartArray();
        var policyIdX = reader.ReadByteString();
        var assetNameX = reader.ReadByteString();
        reader.ReadEndArray();

        reader.ReadStartArray();
        var policyIdY = reader.ReadByteString();
        var assetNameY = reader.ReadByteString();
        reader.ReadEndArray();

        reader.ReadEndArray();

        return new SundaeSwapAssets(new AssetClass(policyIdX, assetNameX), new AssetClass(policyIdY, assetNameY));
    }

    public void Write(ref CborWriter writer, SundaeSwapAssets value)
    {
        writer.WriteStartArray(null);

        writer.WriteStartArray(null);
        writer.WriteByteString(value.AssetX.PolicyId);
        writer.WriteByteString(value.AssetX.AssetName);
        writer.WriteEndArray();

        writer.WriteStartArray(null);
        writer.WriteByteString(value.AssetY.PolicyId);
        writer.WriteByteString(value.AssetY.AssetName);
        writer.WriteEndArray();

        writer.WriteEndArray();
    }
}