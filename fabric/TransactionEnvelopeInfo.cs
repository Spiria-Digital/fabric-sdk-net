using fabricsdk.fabric.Deserializers;
using Google.Protobuf;
using Google.Protobuf.WellKnownTypes;
using System;

namespace fabricsdk.fabric
{
    internal class TransactionEnvelopeInfo : EnvelopeInfo
    {
        public EndorserTransactionEnvDeserializer EndorserTransactionEnvDeserializer {get; private set;}
        public int TransactionActionInfoCount
        {
            get
            {
                return (EndorserTransactionEnvDeserializer.GetPayloadDeserializer() as TransactionPayloadDeserializer).GetTransactionDeserializer().ActionsCount;
            }
        }


        public TransactionEnvelopeInfo(EndorserTransactionEnvDeserializer endorserTransactionEnvDeserializer, int blockIndex)
            :base(endorserTransactionEnvDeserializer, blockIndex)
        {
            EndorserTransactionEnvDeserializer = endorserTransactionEnvDeserializer;
            _headerDeserializer = EndorserTransactionEnvDeserializer.GetPayloadDeserializer().GetHeaderDeserializer();
        }

        //TODO getTransactionActionInfoCount

        public TransactionActionInfo GetTransactionActionInfo(int index)
        {
            return new TransactionActionInfo(
                (EndorserTransactionEnvDeserializer.GetPayloadDeserializer() as TransactionPayloadDeserializer)
                    .GetTransactionDeserializer().GetTransactionActionDeserializer(index)
            );
        }
    }
}