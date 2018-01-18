using fabricsdk.protos.peer;
using Google.Protobuf;
using System;

namespace fabricsdk.fabric.Deserializers
{
    internal class ChaincodeInvocationSpecDeserializer
    {
        private ByteString _byteString;
        private WeakReference<ChaincodeInvocationSpec> _chaincodeInvocationSpec;
        private WeakReference<ChaincodeInputDeserializer> _chaincodeInputDeserializer;
        
        public ChaincodeInvocationSpecDeserializer(ByteString byteString)
        {
            _byteString = byteString;
        }

        public ChaincodeInvocationSpec GetChaincodeInvocationSpec()
        {
            ChaincodeInvocationSpec ret = null;

            if (_chaincodeInvocationSpec != null)
                _chaincodeInvocationSpec.TryGetTarget(out ret);

            if (ret == null)
            {
                ret = ChaincodeInvocationSpec.Parser.ParseFrom(_byteString);
                _chaincodeInvocationSpec = new WeakReference<ChaincodeInvocationSpec>(ret);
            }

            return ret;
        }

        public ChaincodeInputDeserializer GetChaincodeInputDeserializer()
        {
            ChaincodeInputDeserializer ret = null;

            if (_chaincodeInputDeserializer != null)
                _chaincodeInputDeserializer.TryGetTarget(out ret);

            if (ret == null)
            {
                ret = new ChaincodeInputDeserializer(GetChaincodeInvocationSpec().ChaincodeSpec.Input);
                _chaincodeInputDeserializer = new WeakReference<ChaincodeInputDeserializer>(ret);
            }

            return ret;
        }
    }
}