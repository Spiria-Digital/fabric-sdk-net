using fabricsdk.protos.peer;
using Google.Protobuf;
using System;

namespace fabricsdk.fabric.Deserializers
{
    internal class ChaincodeProposalPayloadDeserializer
    {
        private ByteString _byteString;
        private WeakReference<ChaincodeProposalPayload> _chaincodeProposalPayload;
        private WeakReference<ChaincodeInvocationSpecDeserializer> _chaincodeInvocationSpecDeserializer;

        public ChaincodeProposalPayloadDeserializer(ByteString byteString)
        {
            _byteString = byteString;
        }

        public ChaincodeProposalPayload GetChaincodeProposalPayload()
        {
            ChaincodeProposalPayload ret = null;

            if (_chaincodeProposalPayload != null)
                _chaincodeProposalPayload.TryGetTarget(out ret);

            if (ret == null)
            {
                ret = ChaincodeProposalPayload.Parser.ParseFrom(_byteString);
                _chaincodeProposalPayload = new WeakReference<ChaincodeProposalPayload>(ret);
            }

            return ret;
        }

        public ChaincodeInvocationSpecDeserializer GetChaincodeInvocationSpecDeserializer()
        {
            ChaincodeInvocationSpecDeserializer ret = null;

            if (_chaincodeInvocationSpecDeserializer != null)
                _chaincodeInvocationSpecDeserializer.TryGetTarget(out ret);

            if (ret == null)
            {
                ret = new ChaincodeInvocationSpecDeserializer(GetChaincodeProposalPayload().Input);
                _chaincodeInvocationSpecDeserializer = new WeakReference<ChaincodeInvocationSpecDeserializer>(ret);
            }

            return ret;
        }
    }
}