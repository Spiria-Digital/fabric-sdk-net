using fabricsdk.protos.ledger.rwset;
using fabricsdk.protos.ledger.rwset.kvrwset;
using Google.Protobuf;

namespace fabricsdk.fabric.Infos
{
    internal class NsRwsetInfo
    {
        public string Namespace
        {
            get
            {
                return _nsReadWriteSet.Namespace;
            }
        }

        public KVRWSet Rwset
        {
            get
            {
                return KVRWSet.Parser.ParseFrom(_nsReadWriteSet.Rwset);
            }
        } 

        private NsReadWriteSet _nsReadWriteSet;

        public NsRwsetInfo(NsReadWriteSet nsReadWriteSet)
        {
            _nsReadWriteSet = nsReadWriteSet;
        }
    }
}