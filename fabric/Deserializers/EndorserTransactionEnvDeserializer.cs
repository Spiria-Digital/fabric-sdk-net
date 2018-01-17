using Google.Protobuf;

namespace fabricsdk.fabric.Deserializers
{
    internal class EndorserTransactionEnvDeserializer : EnvelopeDeserializer
    {
        public EndorserTransactionEnvDeserializer(ByteString byteString, byte validCode)
            : base(byteString, validCode) { }

        public override PayloadDeserializer GetPayloadDeserializer()
        {
            return new TransactionPayloadDeserializer(GetEnvelope().Payload);
        }
    }
}