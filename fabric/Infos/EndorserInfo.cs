using fabricsdk.fabric.Deserializers;
using fabricsdk.protos.common;
using fabricsdk.protos.peer;
using Google.Protobuf;
using Google.Protobuf.WellKnownTypes;
using System;

namespace fabricsdk.fabric.Infos
{
    internal class EndorserInfo
    {
        public byte[] Signature
        {
            get
            {
                return _endorsement.Signature.ToByteArray();
            }
        }

        public byte[] Endorser
        {
            get
            {
                return _endorsement.Endorser.ToByteArray();
            }
        }

        private Endorsement _endorsement;

        public EndorserInfo(Endorsement endorsement)
        {
            _endorsement = endorsement;
        }
    }
}