using fabricsdk.protos.common;
using fabricsdk.protos.peer;
using Google.Protobuf;
using System;

namespace fabricsdk.fabric.Deserializers
{
    internal class ChaincodeActionPayloadDeserializer
    {
        private ByteString _byteString;
        private WeakReference<ChaincodeActionPayload> _chainCodeActionPayload;
        private WeakReference<ChaincodeEndorsedActionDeserializer> _chainCodeEndorsedActionDeserializer;
        private WeakReference<ChaincodeProposalPayloadDeserializer> _chainCodeProposalPayloadDeserializer;

        public ChaincodeActionPayloadDeserializer(ByteString byteString)
        {
            _byteString = byteString;
        }

        public ChaincodeActionPayload GetChaincodeActionPayload()
        {
            ChaincodeActionPayload ret = null;

            if (_chainCodeActionPayload != null)
                _chainCodeActionPayload.TryGetTarget(out ret);

            if (ret == null)
            {
                ret = ChaincodeActionPayload.Parser.ParseFrom(_byteString);
                _chainCodeActionPayload = new WeakReference<ChaincodeActionPayload>(ret);
            }

            return ret;
        }

        public ChaincodeEndorsedActionDeserializer GetChaincodeEndorsedActionDeserializer()
        {
            ChaincodeEndorsedActionDeserializer ret = null;

            if(_chainCodeEndorsedActionDeserializer != null)
                _chainCodeEndorsedActionDeserializer.TryGetTarget(out ret);

            if (ret == null)
            {
                ret = new ChaincodeEndorsedActionDeserializer(GetChaincodeActionPayload().Action);
                _chainCodeEndorsedActionDeserializer = new WeakReference<ChaincodeEndorsedActionDeserializer>(ret);
            }

            return ret;
        }

        public ChaincodeProposalPayloadDeserializer GetChaincodeProposalPayloadDeserializer()
        {
            ChaincodeProposalPayloadDeserializer ret = null;

            if (_chainCodeProposalPayloadDeserializer != null)
                _chainCodeProposalPayloadDeserializer.TryGetTarget(out ret);

            if (ret == null)
            {
                ret = new ChaincodeProposalPayloadDeserializer(GetChaincodeActionPayload().ChaincodeProposalPayload);
                _chainCodeProposalPayloadDeserializer = new WeakReference<ChaincodeProposalPayloadDeserializer>(ret);
            }

            return null;
        }
    }
}