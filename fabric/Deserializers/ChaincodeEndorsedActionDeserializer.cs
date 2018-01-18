using fabricsdk.protos.peer;
using Google.Protobuf;
using System;

namespace fabricsdk.fabric.Deserializers
{
    internal class ChaincodeEndorsedActionDeserializer
    {
        public int EndorsmentsCount
        {
            get
            {
                return GetChaincodeEndorsedAction().Endorsements.Count;
            }
        }

        private ByteString _byteString;
        private WeakReference<ChaincodeEndorsedAction> _chaincodeEndorsedAction;
        private WeakReference<ProposalResponsePayloadDeserializer> _proposalResponsePayloadDeserializer;

        public ChaincodeEndorsedActionDeserializer(ChaincodeEndorsedAction chaincodeEndorsedAction)
        {
            _byteString = chaincodeEndorsedAction.ToByteString();
            _chaincodeEndorsedAction = new WeakReference<ChaincodeEndorsedAction>(chaincodeEndorsedAction);
        }

        public ChaincodeEndorsedAction GetChaincodeEndorsedAction()
        {
            ChaincodeEndorsedAction ret = null;

            if (_chaincodeEndorsedAction != null)
                _chaincodeEndorsedAction.TryGetTarget(out ret);

            if (ret == null)
            {
                ret = ChaincodeEndorsedAction.Parser.ParseFrom(_byteString);
                _chaincodeEndorsedAction = new WeakReference<ChaincodeEndorsedAction>(ret);
            }

            return ret;
        }

        public byte[] GetEndorsmentSignature(int index)
        {
            return GetChaincodeEndorsedAction().Endorsements[index].Signature.ToByteArray();
        }

        public ProposalResponsePayloadDeserializer GetProposalResponsePayloadDeserializer()
        {
            ProposalResponsePayloadDeserializer ret = null;

            if (_proposalResponsePayloadDeserializer != null)
                _proposalResponsePayloadDeserializer.TryGetTarget(out ret);

            if (ret == null)
            {
                ret = new ProposalResponsePayloadDeserializer(GetChaincodeEndorsedAction().ProposalResponsePayload);
                _proposalResponsePayloadDeserializer = new WeakReference<ProposalResponsePayloadDeserializer>(ret);
            }

            return ret;
        }
    }
}