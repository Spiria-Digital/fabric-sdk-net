using fabricsdk.protos.common;
using fabricsdk.protos.peer;
using Google.Protobuf;
using System;

namespace fabricsdk.fabric.Deserializers
{
    internal class EnvelopeDeserializer 
    {
        public bool IsValid
        {
            get
            {
                return ValidationCode == (int)TxValidationCode.Valid;
            }
        }

        public int Type
        {
            get
            {
                if (!type.HasValue)
                {
                    type = GetPayloadDeserializer().GetHeaderDeserializer().GetChannelHeaderDeserializer().Type;
                }

                return type.Value;
            }
        }

        public int ValidationCode {get; private set;}

        private ByteString _byteString;
        private WeakReference<Envelope> _envelope;
        private WeakReference<PayloadDeserializer> _payload;
        private int? type = null;

        public EnvelopeDeserializer(ByteString byteString, byte validCode)
        {
            _byteString = byteString;
            ValidationCode = validCode;
        }

        public Envelope GetEnvelope()
        {
            Envelope ret = null;

            if (_envelope != null)
                _envelope.TryGetTarget(out ret);

            if (ret == null)
            {
                ret = Envelope.Parser.ParseFrom(_byteString);

                _envelope = new WeakReference<Envelope>(ret);
            }

            return ret;
        }

        public virtual PayloadDeserializer GetPayloadDeserializer()
        {
            PayloadDeserializer ret = null;

            if (_payload != null)
                _payload.TryGetTarget(out ret);

            if (ret == null)
            {
                ret = new PayloadDeserializer(GetEnvelope().Payload);
                _payload = new WeakReference<PayloadDeserializer>(ret);
            }
            
            return ret;
        }
    }
}