using System.Formats.Cbor;
using CborSerialization;

namespace Cardano.Sync.Data.Models.Datums;

/*
pub type PoolDatum {
  /// the unique identifier of the pool. Produced by hashing one of the input UTXOs used to produce the pool
  /// to ensure uniqueness.
  identifier: Ident,
  /// The two asset IDs that this pool can exchange, in alphabetical order
  /// Used to validate that the assets being swapped are indeed the intended assets
  assets: (AssetClass, AssetClass),
  /// The total number of LP tokens in circulation
  /// Maintains the following two invariants on each deposit or withdrawal:
  /// - circulating_lp is always equal to the number of LP tokens that have been minted and are in circulation
  /// - A users LP tokens (or burned LP tokens), as a percentage of the circulating LP tokens, represent the percentage of assets they just deposited or withdrew.
  circulating_lp: Int,
  /// The basis points to charge on each trade for bid (A -> B) and ask (B -> A) orders
  /// For example, a 1% fee would be represented as 100 (out of 10,000), and a 0.3% fee would be represented as 30
  bid_fees_per_10_thousand: Int,
  ask_fees_per_10_thousand: Int,
  // An optional multisig condition under which the protocol fees can be updated
  fee_manager: Option<multisig.MultisigScript>,
  /// The UNIX millisecond timestamp at which trading against the pool should be allowed
  /// TODO: deposits and arguably withdrawals should be processed before the market open
  market_open: PosixTime,
  /// The amount of ADA on the UTXO that is set aside by collecting protocol fees
  /// This should be increased on each scoop to represent collecting fees; deducted from the reserve amount (if one of the tokens in the pair is ADA)
  /// to calculate the swap amounts, and decreased when some amount is withdrawn.
  /// Note that this also allows us to conveniently sidestep minUTXO woes, because the creator of the pool can set the initial protocol fees to whatever minUTXO is needed
  /// and withdrawals never have to be for the full amount.
  /// TODO: should we add a field to the settings object to set a minimum initial protocol_fees on pool mint?
  protocol_fees: Int,
}

121_0([_
    h'64f35d26b237ad58e099041bc14c687ea7fdc58969d7d5b66e2540ef',
    [_
        [_ h'', h''],
        [_
            h'c48cbb3d5e57ed56e276bc45f99ab39abe94e6cd7ac39fb402da47ad',
            h'0014df105553444d',
        ],
    ]),
    2461579028107_3,
    100_0,
    100_0,
    121_0([_
        121_0([_
            h'a55f28e5ea668602c69f0975a5b51d68be87763360f63277ffe6dd7d',
        ]),
    ]),
    0,
    351704004_2,
])
*/

[CborSerialize(typeof(SundaeSwapLiquidityPoolCborConvert))]
public record SundaeSwapLiquidityPool(
    byte[] Identifier,
    SundaeSwapAssets Assets,
    ulong CirculatingLP,
    ulong BidFeesPer10Thousand,
    ulong AskFeesPer10Thousand,
    MultisigScript? FeeManager,
    PosixTime MarketOpen,
    ulong ProtocolFees
) : IDatum;

public class SundaeSwapLiquidityPoolCborConvert : ICborConvertor<SundaeSwapLiquidityPool>
{
    public SundaeSwapLiquidityPool Read(ref CborReader reader)
    {
        var tag = reader.ReadTag();
        if ((int)tag != 121)
        {
            throw new Exception("Invalid tag");
        }
        reader.ReadStartArray();
        var identifier = reader.ReadByteString();
        var assets = new SundaeSwapAssetsCborConvert().Read(ref reader);
        var circulatingLp = reader.ReadUInt64();
        var bidFeesPer10Thousand = reader.ReadUInt64();
        var askFeesPer10Thousand = reader.ReadUInt64();
        var feeManager = new MultisigScriptCborConvert().Read(ref reader);
        var marketOpen = new PosixTimeCborConvert().Read(ref reader);
        var protocolFees = reader.ReadUInt64();
        reader.ReadEndArray();

        return new SundaeSwapLiquidityPool
        (
            identifier, 
            assets, 
            circulatingLp, 
            bidFeesPer10Thousand, 
            askFeesPer10Thousand,
            feeManager,
            marketOpen,
            protocolFees
        );
    }

    public void Write(ref CborWriter writer, SundaeSwapLiquidityPool value)
    {
        writer.WriteTag((CborTag)121);
        writer.WriteStartArray(null);
        writer.WriteByteString(value.Identifier);
        new SundaeSwapAssetsCborConvert().Write(ref writer, value.Assets);
        writer.WriteUInt64(value.CirculatingLP);
        writer.WriteUInt64(value.BidFeesPer10Thousand);
        writer.WriteUInt64(value.AskFeesPer10Thousand);
        new MultisigScriptCborConvert().Write(ref writer, value.FeeManager);
        new PosixTimeCborConvert().Write(ref writer, value.MarketOpen);
        writer.WriteUInt64(value.ProtocolFees);
        writer.WriteEndArray();
    }
}