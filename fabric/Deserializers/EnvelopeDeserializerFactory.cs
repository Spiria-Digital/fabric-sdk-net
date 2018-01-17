using fabricsdk.protos.common;
using Google.Protobuf;

namespace fabricsdk.fabric.Deserializers
{
    internal static class EnvelopeDeserializerFactory
    {
        public static EnvelopeDeserializer Create(ByteString byteString, byte b)
        {
            int type = ChannelHeader.Parser.ParseFrom(Payload.Parser.ParseFrom(Envelope.Parser.ParseFrom(byteString).Payload).Header.ChannelHeader).Type;

            /*
            MESSAGE = 0;                   // Used for messages which are signed but opaque
            CONFIG = 1;                    // Used for messages which express the channel config
            CONFIG_UPDATE = 2;             // Used for transactions which update the channel config
            ENDORSER_TRANSACTION = 3;      // Used by the SDK to submit endorser based transactions
            ORDERER_TRANSACTION = 4;       // Used internally by the orderer for management
            DELIVER_SEEK_INFO = 5;         // Used as the type for Envelope messages submitted to instruct the Deliver API to seek
            CHAINCODE_PACKAGE = 6;         // Used for packaging chaincode artifacts for install
            */

            switch(type)
            {
                case 3:
                    return new EndorserTransactionEnvDeserializer(byteString, b);
                default:
                    return new EnvelopeDeserializer(byteString, b);
            }
        }
    }
}