using Google.Protobuf;
using fabricsdk.protos.common;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace fabricsdk.fabric.Deserializers
{
    internal class BlockDeserializer
    {
        public Block Block {get; private set;}
        private ConcurrentDictionary<int, WeakReference<EnvelopeDeserializer>> _envelopes = new ConcurrentDictionary<int, WeakReference<EnvelopeDeserializer>>();

        public BlockDeserializer(Block block)
        {
            Block = block;
        }

        public ByteString PreviousHash 
        {
            get 
            {
                return Block.Header.PreviousHash;
            }
        }

        public ByteString DataHash 
        {
            get 
            {
                return Block.Header.DataHash;
            }
        }

        public ulong Number 
        {
            get 
            {
                return Block.Header.Number;
            }
        }

        public BlockData Data 
        {
            get 
            {
                return Block.Data;
            }
        }

        public byte[] TransactionsMetaData 
        {
            get 
            {
                return Block.Metadata.Metadata[(int)BlockMetadataIndex.TransactionsFilter].ToByteArray();
            }
        }

        public EnvelopeDeserializer GetData(int index)
         {
            if (index >= Data.Data.Count) 
                return null;

            WeakReference<EnvelopeDeserializer> envelopeWeakReference;
            _envelopes.TryGetValue(index, out envelopeWeakReference);

            if (envelopeWeakReference != null)
            {
                EnvelopeDeserializer ret;
                envelopeWeakReference.TryGetTarget(out ret);

                if (ret != null)
                    return ret;
            }

            var envelopeDeserializer = EnvelopeDeserializerFactory.Create(Data.Data[index], TransactionsMetaData[index]);
            _envelopes[index] = new WeakReference<EnvelopeDeserializer>(envelopeDeserializer);

            return envelopeDeserializer;
        }
    }
}