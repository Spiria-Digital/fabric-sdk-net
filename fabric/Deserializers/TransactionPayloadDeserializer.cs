using fabricsdk.protos.peer;
using Google.Protobuf;
using System;

namespace fabricsdk.fabric.Deserializers
{
    internal class TransactionPayloadDeserializer : PayloadDeserializer
    {
        private WeakReference<TransactionDeserializer> _transactionDeserializer;
        
        public TransactionPayloadDeserializer(ByteString byteString)
            : base(byteString) { }

        public TransactionDeserializer GetTransactionDeserializer()
        {
            TransactionDeserializer ret  = null;

            if (_transactionDeserializer != null)
                _transactionDeserializer.TryGetTarget(out ret);

            if (ret == null)
            {
                ret = new TransactionDeserializer(GetPayload().Data);
                _transactionDeserializer = new WeakReference<TransactionDeserializer>(ret);
            }

            return ret;
        }
    }
}