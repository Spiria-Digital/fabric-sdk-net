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
                return IsFiltered() ? null : _blockDeserializer.Block;
            }
        }

        public FilteredBlock FilteredBlock 
        {
            get {
                return !IsFiltered() ? null : _filteredBlock;
            }
        }

        public byte[] PreviousHash
        {
            get
            {
                return IsFiltered() ? null : _blockDeserializer.PreviousHash.ToByteArray();
            }
        }

        public byte[] DataHash
        {
            get
            {
                return IsFiltered() ? null : _blockDeserializer.DataHash.ToByteArray();
            }
        }

        public byte[] TransactionsMetaData
        {
            get
            {
                return IsFiltered() ? null : _blockDeserializer.TransactionsMetaData;
            }
        }

        public ulong Number
        {
            get
            {
                return IsFiltered() ? _filteredBlock.Number : _blockDeserializer.Number;
            }
        }

        public int EnvelopeCount
        {
            get
            {
                return IsFiltered() ? _filteredBlock.FilteredTx.Count : _blockDeserializer.Data.Data.Count;
            }
        }

        public string ChannelID
        {
            get {
                return IsFiltered() ? _filteredBlock.ChannelId : GetEnvelopeInfo(0).ChannelID;
            } 
        }

        private BlockDeserializer _blockDeserializer;
        private FilteredBlock _filteredBlock;
        
        public BlockInfo(Block block)
        {
            _blockDeserializer = new BlockDeserializer(block);
        }

        public BlockInfo(DeliverResponse resp) {
            var type = resp.TypeCase;

            if (type == DeliverResponse.TypeOneofCase.Block) {
                var respBlock = resp.Block;
                _filteredBlock = null;
                if (respBlock == null) {
                    throw new ArgumentException("DeliverResponse type block but block is null");
                }

                _blockDeserializer = new BlockDeserializer(respBlock);
            } else if (type == DeliverResponse.TypeOneofCase.FilteredBlock) {
                _filteredBlock = resp.FilteredBlock;
                _blockDeserializer = null;
                if (_filteredBlock == null) {
                    throw new ArgumentException("DeliverResponse type filter block but filter block is null");
                }
            } else {
                throw new ArgumentException($"DeliverResponse type has unexpected type: {type}, {(int)type}");
            }
        }

        public bool IsFiltered() {
            if (_filteredBlock == null && _blockDeserializer == null)
                throw new Exception("Both block and filter are null.");

            if (_filteredBlock != null && _blockDeserializer != null)
                throw new Exception("Both block and filter are set.");

            return _filteredBlock != null;
        }

        public EnvelopeInfo GetEnvelopeInfo(int envelopeIndex)
        {
            EnvelopeInfo ret = null;

            if (IsFiltered())
            {
                switch ((int)_filteredBlock.FilteredTx[envelopeIndex].Type)
                {
                    case (int)HeaderType.EndorserTransaction:
                        ret = new TransactionEnvelopeInfo(_filteredBlock.FilteredTx[envelopeIndex], envelopeIndex);
                        break;
                    default: 
                        ret = new EnvelopeInfo(_filteredBlock.FilteredTx[envelopeIndex]);
                        break;
                }
            } 
            else 
            {
                var ed = EnvelopeDeserializerFactory.Create(_blockDeserializer.Block.Data.Data[envelopeIndex], _blockDeserializer.TransactionsMetaData[envelopeIndex]);
                
                switch (ed.Type)
                {
                    case (int)HeaderType.EndorserTransaction :
                        ret = new TransactionEnvelopeInfo((EndorserTransactionEnvDeserializer)ed, envelopeIndex);
                        break;
                    default :
                        ret = new EnvelopeInfo(ed);
                        break;
                }
            }

            return ret;
        }
    }
}