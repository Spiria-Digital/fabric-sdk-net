using fabricsdk.protos.peer;
using Google.Protobuf;
using System;

namespace fabricsdk.fabric.Deserializers
{
    internal class TransactionActionDeserializer 
    {
        private ByteString _byteString;
        private WeakReference<TransactionAction> _transactionAction;
        private WeakReference<ChaincodeActionPayloadDeserializer> _chaincodeActionPayloadDeserializer;

        public TransactionActionDeserializer(ByteString byteString)
        {
            _byteString = byteString;
        }

        public TransactionActionDeserializer(TransactionAction transactionAction)
        {
            _byteString = transactionAction.ToByteString();
            _transactionAction = new WeakReference<TransactionAction>(transactionAction);
        }

        public TransactionAction GetTransactionAction()
        {
            TransactionAction ret = null;

            if (_transactionAction != null)
                _transactionAction.TryGetTarget(out ret);

            if (ret == null)
            {
                ret = TransactionAction.Parser.ParseFrom(_byteString);
                _transactionAction = new WeakReference<TransactionAction>(ret);
            }

            return ret;
        }

        public ChaincodeActionPayloadDeserializer GetPayloadDeserializer()
        {
            ChaincodeActionPayloadDeserializer ret = null;

            if (_chaincodeActionPayloadDeserializer != null)
                _chaincodeActionPayloadDeserializer.TryGetTarget(out ret);

            if (ret == null)
            {
                ret = new ChaincodeActionPayloadDeserializer(GetTransactionAction().Payload);
                _chaincodeActionPayloadDeserializer = new WeakReference<ChaincodeActionPayloadDeserializer>(ret);
            }

            return ret;
        }
    }
}
