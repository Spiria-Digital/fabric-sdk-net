using fabricsdk.protos.ledger.rwset;
using fabricsdk.protos.peer;
using Google.Protobuf;
using System;

namespace fabricsdk.fabric.Deserializers
{
    internal class ChaincodeActionDeserializer
    {
        public String ResponseMessage
        {
            get
            {
                return GetChaincodeAction().Response.Message;
            }
        }

        public byte[] ResponseMessageBytes
        {
            get
            {
                return GetChaincodeAction().Response.ToByteArray();
            }
        }

        public int ResponseStatus
        {
            get
            {
                return GetChaincodeAction().Response.Status;
            }
        }

        public ByteString Payload
        {
            get
            {
                return GetChaincodeAction().Response.Payload;
            }
        }

        private ByteString _byteString;
        private WeakReference<ChaincodeAction> _chaincodeAction;

        public ChaincodeActionDeserializer(ByteString byteString)
        {
            _byteString = byteString;
        }

        public ChaincodeAction GetChaincodeAction()
        {
            ChaincodeAction ret = null;

            if (_chaincodeAction != null)
                _chaincodeAction.TryGetTarget(out ret);

            if (ret == null)
            {
                ret = ChaincodeAction.Parser.ParseFrom(_byteString);
                _chaincodeAction = new WeakReference<ChaincodeAction>(ret);
            }

            return ret;
        }

        public fabricsdk.fabric.Events.ChaincodeEvent GetEvent()
        {
            ChaincodeAction ca = GetChaincodeAction();
            ByteString eventsByte = ca.Events;

            if (eventsByte == null || eventsByte.IsEmpty)
                return null;

            return new fabricsdk.fabric.Events.ChaincodeEvent(eventsByte);
        }

        public TxReadWriteSet GetResults()
        {
            return TxReadWriteSet.Parser.ParseFrom(GetChaincodeAction().Results);
        }
    }
}