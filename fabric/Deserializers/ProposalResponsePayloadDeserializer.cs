using fabricsdk.protos.peer;
using Google.Protobuf;
using System;

namespace fabricsdk.fabric.Deserializers
{
    internal class ProposalResponsePayloadDeserializer
    {
        private ByteString _byteString;
        private WeakReference<ProposalResponsePayload> _proposalResponsePayload;
        private WeakReference<ChaincodeActionDeserializer> _chaincodeActionDeserializer;

        public ProposalResponsePayloadDeserializer(ByteString byteString)
        {
            _byteString = byteString;
        }

        public ProposalResponsePayload GetProposalResponsePayload()
        {
            ProposalResponsePayload ret = null;

            if (_proposalResponsePayload != null)
                _proposalResponsePayload.TryGetTarget(out ret);

            if (ret == null)
            {
                ret = ProposalResponsePayload.Parser.ParseFrom(_byteString);
                _proposalResponsePayload = new WeakReference<ProposalResponsePayload>(ret);
            }

            return ret;
        }

        public ChaincodeActionDeserializer GetChaincodeActionDeserializer()
        {
            ChaincodeActionDeserializer ret = null;

            if (_chaincodeActionDeserializer != null)
                _chaincodeActionDeserializer.TryGetTarget(out ret);

            if (ret == null)
            {
                ret = new ChaincodeActionDeserializer(GetProposalResponsePayload().Extension);
                _chaincodeActionDeserializer = new WeakReference<ChaincodeActionDeserializer>(ret);
            }

            return ret;
        }
    }
}