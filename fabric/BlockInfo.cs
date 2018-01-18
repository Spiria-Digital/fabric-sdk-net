using fabricsdk.fabric.Deserializers;
using fabricsdk.protos.common;
using fabricsdk.protos.peer;
using Google.Protobuf;
using System;

namespace fabricsdk.fabric
{
    /**
        Information related to a Block.
     */
    internal class BlockInfo
    {
        public Block Block
        {
            get
            {
                return _blockDeserializer.Block;
            }
        }

        public byte[] PreviousHash
        {
            get
            {
                return _blockDeserializer.PreviousHash.ToByteArray();
            }
        }

        public byte[] DataHash
        {
            get
            {
                return _blockDeserializer.DataHash.ToByteArray();
            }
        }

        public byte[] TransactionsMetaData
        {
            get
            {
                return _blockDeserializer.TransactionsMetaData;
            }
        }

        public ulong Number
        {
            get
            {
                return _blockDeserializer.Number;
            }
        }

        public int EnvelopeCount
        {
            get
            {
                return _blockDeserializer.Data.Data.Count;
            }
        }

        // public string ChannelID() 
        // {
        //     return 
        // }

        private BlockDeserializer _blockDeserializer;

        public BlockInfo(Block block)
        {
            _blockDeserializer = new BlockDeserializer(block);
        }

        public EnvelopeInfo GetEnvelopeInfo(int envelopeIndex)
        {
            EnvelopeInfo ret = null;

            var ed = EnvelopeDeserializerFactory.Create(_blockDeserializer.Block.Data.Data[envelopeIndex], _blockDeserializer.TransactionsMetaData[envelopeIndex]);
            
            switch (ed.Type)
            {
                case 3 :
                    ret = new TransactionEnvelopeInfo((EndorserTransactionEnvDeserializer)ed, envelopeIndex);
                    break;
                default :
                    ret = new EnvelopeInfo(ed, envelopeIndex);
                    break;
            }

            return ret;
        }
    }
}