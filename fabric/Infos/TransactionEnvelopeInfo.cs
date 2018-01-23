using fabricsdk.fabric.Deserializers;
using fabricsdk.protos.peer;
using Google.Protobuf;
using Google.Protobuf.WellKnownTypes;
using System;

namespace fabricsdk.fabric.Infos
{
    internal class TransactionEnvelopeInfo : EnvelopeInfo
    {
        public EndorserTransactionEnvDeserializer EndorserTransactionEnvDeserializer {get; private set;}
        public int TransactionActionInfoCount
        {
            get
            {
                return IsFiltered ? _filteredTx.TransactionActions.ChaincodeActions.Count :
                    (EndorserTransactionEnvDeserializer.GetPayloadDeserializer() as TransactionPayloadDeserializer).GetTransactionDeserializer().ActionsCount;
            }
        }

        public TransactionEnvelopeInfo(FilteredTransaction filteredTx)
            : base(filteredTx) { }

        public TransactionEnvelopeInfo(EndorserTransactionEnvDeserializer endorserTransactionEnvDeserializer)
            :base(endorserTransactionEnvDeserializer)
        {
            EndorserTransactionEnvDeserializer = endorserTransactionEnvDeserializer;
            _headerDeserializer = EndorserTransactionEnvDeserializer.GetPayloadDeserializer().GetHeaderDeserializer();
        }

        public TransactionActionInfo GetTransactionActionInfo(int index)
        {
            if (IsFiltered)
                return new TransactionActionInfo(
                    _filteredTx.TransactionActions.ChaincodeActions[index]
                );

            return new TransactionActionInfo(
                (EndorserTransactionEnvDeserializer.GetPayloadDeserializer() as TransactionPayloadDeserializer)
                    .GetTransactionDeserializer().GetTransactionActionDeserializer(index)
            );
        }
    }
}