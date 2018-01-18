using fabricsdk.fabric.Deserializers;
using fabricsdk.protos.common;
using fabricsdk.protos.ledger.rwset;
using fabricsdk.protos.peer;
using Google.Protobuf;
using System;

namespace fabricsdk.fabric
{
    internal class TxReadWriteSetInfo
    {
        public int NsRwsetCount
        {
            get
            {
                return _txReadWriteSetInfo.NsRwset.Count;
            }
        }

        private TxReadWriteSet _txReadWriteSetInfo;

        public TxReadWriteSetInfo(TxReadWriteSet txReadWriteSet)
        {
            _txReadWriteSetInfo = txReadWriteSet;
        }

        public NsRwsetInfo GetNsRwsetInfo(int index)
        {
            return new NsRwsetInfo(_txReadWriteSetInfo.NsRwset[index]);
        }
    }
}