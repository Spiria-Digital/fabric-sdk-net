using fabricsdk.protos.common;

namespace fabricsdk.fabric
{
    internal class BlockchainInfo
    {
        public ulong Height
        {
            get
            {
                return _blockchainInfo.Height;
            }
        }

        public byte[] CurrentBlockHash
        {
            get
            {
                return _blockchainInfo.CurrentBlockHash.ToByteArray();
            }
        }

        public byte[] PreviousBlockHash
        {
            get
            {
                return _blockchainInfo.CurrentBlockHash.ToByteArray();
            }
        }

        public fabricsdk.protos.common.BlockchainInfo _blockchainInfo;
        public BlockchainInfo(fabricsdk.protos.common.BlockchainInfo blockchainInfo)
        {
            _blockchainInfo = blockchainInfo;
        } 

        public fabricsdk.protos.common.BlockchainInfo GetBlockchainInfo()
        {
            return _blockchainInfo;
        }
    }
}