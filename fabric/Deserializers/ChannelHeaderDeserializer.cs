using fabricsdk.protos.common;
using Google.Protobuf;
using Google.Protobuf.WellKnownTypes;
using System;

namespace fabricsdk.fabric.Deserializers
{
    internal class ChannelHeaderDeserializer
    {
        public string ChannelID
        {
            get
            {
                return GetChannelHeader().ChannelId;
            }
        }

        public ulong Epoch
        {
            get
            {
                return GetChannelHeader().Epoch;
            }
        }

        public Timestamp Timestamp
        {
            get
            {
                return GetChannelHeader().Timestamp;
            }
        }

        public string TxID
        {
            get
            {
                return GetChannelHeader().TxId;
            }
        }

        public int Type
        {
            get
            {
                return GetChannelHeader().Type;
            }
        }

        public int Version
        {
            get
            {
                return GetChannelHeader().Version;
            }
        }

        private ByteString _byteString;
        private WeakReference<ChannelHeader> _channelHeader;

        public ChannelHeaderDeserializer(ByteString byteString)
        {
            _byteString = byteString;
        }

        public ChannelHeader GetChannelHeader()
        {
            ChannelHeader ret = null;

            if (_channelHeader != null)
                _channelHeader.TryGetTarget(out ret);

            if (ret == null)
            {
                ret = ChannelHeader.Parser.ParseFrom(_byteString);
                _channelHeader = new WeakReference<ChannelHeader>(ret);
            }

            return ret;
        }
    }
}