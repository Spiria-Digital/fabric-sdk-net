using System;
using Google.Protobuf;
using fabricsdk.protos.peer;

namespace fabricsdk.fabric.Events
{
    public class ChaincodeEvent
    {
        public string EventName
        {
            get
            {
                return GetChaincodeEvent().EventName;
            }
        }

        public string ChaincodeID
        {
            get
            {
                return GetChaincodeEvent().ChaincodeId;
            }
        }

        public string TxID
        {
            get
            {
                return GetChaincodeEvent().TxId;
            }
        }

        public byte[] Payload
        {
            get
            {
                return GetChaincodeEvent().Payload?.ToByteArray();
            }
        }

        

        private ByteString _byteString;
        private WeakReference<fabricsdk.protos.peer.ChaincodeEvent> _chaincodeEvent;

        public ChaincodeEvent(ByteString byteString)
        {
            _byteString = byteString;
        }

        private fabricsdk.protos.peer.ChaincodeEvent GetChaincodeEvent()
        {
            fabricsdk.protos.peer.ChaincodeEvent ret = null;

            if (_chaincodeEvent != null)
                _chaincodeEvent.TryGetTarget(out ret);

            if (ret == null)
            {
                ret = fabricsdk.protos.peer.ChaincodeEvent.Parser.ParseFrom(_byteString);
                _chaincodeEvent = new WeakReference<protos.peer.ChaincodeEvent>(ret);
            }

            return ret;
        }

        
    }
}