using fabricsdk.fabric.Deserializers;
using Google.Protobuf;
using Google.Protobuf.WellKnownTypes;
using System;

namespace fabricsdk.fabric
{
    internal enum EnvelopeType
    {
        TransactionEnveloppe,
        Enveloppe
    }

    internal class EnvelopeInfo
    {
        public string ChannelID
        {
            get
            {
                return _headerDeserializer.GetChannelHeaderDeserializer().ChannelID;
            }
        }

        public string TransactionID
        {
            get
            {
                return _headerDeserializer.GetChannelHeaderDeserializer().TxID;
            }
        }

        public ulong Epoch
        {
            get
            {
                return _headerDeserializer.GetChannelHeaderDeserializer().Epoch;
            }
        }

        public Timestamp Timestamp
        {
            get
            {
                return _headerDeserializer.GetChannelHeaderDeserializer().Timestamp;
            }
        }

        public bool IsValid
        {
            get
            {
                return _envelopeDeserializer.IsValid;
            }
        }

        public byte ValidationCode
        {
            get
            {
                return _envelopeDeserializer.ValidationCode;
            }
        }

        public EnvelopeType Type
        {
            get
            {
                switch (_headerDeserializer.GetChannelHeaderDeserializer().Type)
                {
                    case 3:
                        return EnvelopeType.TransactionEnveloppe;
                    default:
                        return EnvelopeType.Enveloppe;
                }
            }
        }

        private EnvelopeDeserializer _envelopeDeserializer;
        protected HeaderDeserializer _headerDeserializer;

        public EnvelopeInfo(EnvelopeDeserializer envelopeDeserializer, int blockIndex)
        {
            _envelopeDeserializer = envelopeDeserializer;
            _headerDeserializer = envelopeDeserializer.GetPayloadDeserializer().GetHeaderDeserializer();
        }
    }
}