using fabricsdk.protos.peer;
using Google.Protobuf;
using System;

namespace fabricsdk.fabric.Deserializers
{
    internal class ChaincodeInputDeserializer
    {
        private ByteString _byteString;
        private WeakReference<ChaincodeInput> _chaincodeInput;

        public ChaincodeInputDeserializer(ChaincodeInput chaincodeInput)
        {
            _byteString = chaincodeInput.ToByteString();
            _chaincodeInput = new WeakReference<ChaincodeInput>(chaincodeInput);
        }

        public ChaincodeInput GetChaincodeInput()
        {
            ChaincodeInput ret = null;

            if (_chaincodeInput != null)
                _chaincodeInput.TryGetTarget(out ret);

            if (ret == null)
            {
                ret = ChaincodeInput.Parser.ParseFrom(_byteString);
                _chaincodeInput = new WeakReference<ChaincodeInput>(ret);
            }

            return ret;
        }
    }
}