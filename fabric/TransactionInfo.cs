using fabricsdk.fabric.Deserializers;
using fabricsdk.protos.common;
using fabricsdk.protos.peer;
using Google.Protobuf;

namespace fabricsdk.fabric
{
    internal class TransactionInfo
    {
        public string TxID {get; private set;}

        public Envelope Envelope
        {
            get
            {
                return ProcessedTransaction.TransactionEnvelope;
            }
        }

        public ProcessedTransaction ProcessedTransaction {get; private set;}

        public TxValidationCode ValidationCode
        {
            get
            {
                return (TxValidationCode)ProcessedTransaction.ValidationCode;
            }
        }

        public TransactionInfo(string txID, ProcessedTransaction processedTransaction)
        {
            TxID = txID;
            ProcessedTransaction = processedTransaction;
        }
    }
}