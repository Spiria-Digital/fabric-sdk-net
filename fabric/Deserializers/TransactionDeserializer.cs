using fabricsdk.protos.peer;
using Google.Protobuf;
using System;
using System.Collections.Concurrent;

namespace fabricsdk.fabric.Deserializers
{
    internal class TransactionDeserializer 
    {
        public int ActionsCount
        {
            get
            {
                return GetTransaction().Actions.Count;
            }
        }

        private ByteString _byteString;
        private WeakReference<Transaction> _transaction;
        private ConcurrentDictionary<int, WeakReference<TransactionActionDeserializer>> _transactionActions = 
            new ConcurrentDictionary<int, WeakReference<TransactionActionDeserializer>>();

        public TransactionDeserializer(ByteString byteString)
        {
            _byteString = byteString;
        }

        public Transaction GetTransaction()
        {
            Transaction ret = null;

            if (_transaction != null)
                _transaction.TryGetTarget(out ret);
            
            if (ret == null)
            {
                ret = Transaction.Parser.ParseFrom(_byteString);
                _transaction = new WeakReference<Transaction>(ret);
            }
            
            return ret;
        }

        public TransactionActionDeserializer GetTransactionActionDeserializer(int index)
        {
            Transaction transaction = GetTransaction();

            if (index >= ActionsCount)
                return null;

            WeakReference<TransactionActionDeserializer> transactionActionDeserializerWeakRef;
            _transactionActions.TryGetValue(index, out transactionActionDeserializerWeakRef);

            if (transactionActionDeserializerWeakRef != null)
            {
                TransactionActionDeserializer ret = null;
                transactionActionDeserializerWeakRef.TryGetTarget(out ret);
                if (ret != null)
                    return ret;
            }

            TransactionActionDeserializer transactionActionDeserializer = new TransactionActionDeserializer(transaction.Actions[index]);
            _transactionActions[index] = new WeakReference<TransactionActionDeserializer>(transactionActionDeserializer);

            return transactionActionDeserializer;
        }

        //TODO getTransactionActions(). Depends on how it's used further down. (Iterator VS IEnumerable)
    }
}