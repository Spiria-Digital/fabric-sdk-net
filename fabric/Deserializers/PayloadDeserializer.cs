using fabricsdk.protos.common;
using Google.Protobuf;
using System;

namespace fabricsdk.fabric.Deserializers
{
    internal class PayloadDeserializer
    {
        private ByteString _byteString;
        private WeakReference<Payload> _payload;

        public PayloadDeserializer(ByteString byteString)
        {
            _byteString = byteString;
        }

        public Payload GetPayload()
        {
            Payload ret = null;

            if (_payload != null)
                _payload.TryGetTarget(out ret);

            if (ret == null)
            {
                ret = Payload.Parser.ParseFrom(_byteString);

                _payload = new WeakReference<Payload>(ret);
            }

            return ret;
        }

        public HeaderDeserializer GetHeaderDeserializer()
        {
            return new HeaderDeserializer(GetPayload().Header);
        }
    }
}