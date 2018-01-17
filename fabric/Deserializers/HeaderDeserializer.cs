using fabricsdk.protos.common;
using System;

namespace fabricsdk.fabric.Deserializers
{
    internal class HeaderDeserializer
    {
        public Header Header {get; private set;}
        private WeakReference<ChannelHeaderDeserializer> _channelHeader;

        public HeaderDeserializer(Header header)
        {
            Header = header;
        }

        public ChannelHeaderDeserializer GetChannelHeaderDeserializer()
        {
            ChannelHeaderDeserializer ret = null;

            if (_channelHeader != null)
                _channelHeader.TryGetTarget(out ret);

            if (ret == null)
            {
                ret = new ChannelHeaderDeserializer(Header.ChannelHeader);
                _channelHeader = new WeakReference<ChannelHeaderDeserializer>(ret);
            }

            return ret;
        }
    }
}