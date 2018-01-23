using fabricsdk.protos.peer;
using System;
using System.Threading;
using System.Collections.Concurrent;

namespace fabricsdk.fabric
{
    [Serializable]
    public class Peer
    {
        public string Name {get; private set;}
        public string Url {get; private set;}

        //public Channel Channel {get; private set;}
        public string ChannelName {get; private set;}

        // public ConcurrentQueue ExecutorService
        // {
        //     get
        //     {
        //         return Channel.ExecutorService;
        //     }
        // }

        public Peer(string name, string grcpUrl)
        {
            //TODO util checkGrpcUrl()
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Invalid name for peer.");

            Name = name;
            Url = grcpUrl;
        }

        public void UnsetChannel()
        {
            ChannelName = null;
            //Channel = null;
        }
    }
}