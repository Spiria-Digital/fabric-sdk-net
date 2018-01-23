using fabricsdk.fabric.Deserializers;
using fabricsdk.protos.peer;
using Google.Protobuf;
using Google.Protobuf.WellKnownTypes;
using fabricsdk.fabric.Transaction;
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
                return IsFiltered ? _filteredTx.Txid : _headerDeserializer.GetChannelHeaderDeserializer().TxID;
            }
        }

        public ulong Epoch
        {
            get
            {
                return IsFiltered ? Convert.ToUInt64(-1) : _headerDeserializer.GetChannelHeaderDeserializer().Epoch;
            }
        }

        public DateTime Timestamp
        {
            get
            {
                return IsFiltered ? DateTime.MinValue : _headerDeserializer.GetChannelHeaderDeserializer().Timestamp.ToDateTime();
            }
        }

        public bool IsFiltered
        {
            get {
                return _filteredTx != null;
            }
        }

        public bool IsValid
        {
            get
            {
                return IsFiltered ? _filteredTx.TxValidationCode == TxValidationCode.Valid : _envelopeDeserializer.IsValid;
            }
        }

        public byte ValidationCode
        {
            get
            {
                if (IsFiltered)
                    return (byte) _filteredTx.TxValidationCode;

                return _envelopeDeserializer.ValidationCode;
            }
        }

        public EnvelopeType Type
        {
            get
            {
                var type = IsFiltered ? (int)_filteredTx.Type : _headerDeserializer.GetChannelHeaderDeserializer().Type;

                switch (type)
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
        protected FilteredTransaction _filteredTx;

        public EnvelopeInfo(EnvelopeDeserializer envelopeDeserializer)
        {
            _envelopeDeserializer = envelopeDeserializer;
            _headerDeserializer = envelopeDeserializer.GetPayloadDeserializer().GetHeaderDeserializer();
        }

        public EnvelopeInfo(FilteredTransaction filteredTx) {
            _filteredTx = filteredTx;
        }
    }
}