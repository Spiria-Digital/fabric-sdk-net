using fabricsdk.fabric.Deserializers;
using fabricsdk.protos.common;
using fabricsdk.protos.peer;
using fabricsdk.protos.ledger.rwset;
using Google.Protobuf;
using Google.Protobuf.WellKnownTypes;
using System;
using System.Collections;
using System.Collections.Generic;

namespace fabricsdk.fabric.Infos
{
    internal class TransactionActionInfo
    {
        public byte[] ResponseMessageBytes
        {
            get
            {
                return IsFiltered ? null :
                    _transactionActionDeserializer
                        .GetPayloadDeserializer()
                        .GetChaincodeEndorsedActionDeserializer()
                        .GetProposalResponsePayloadDeserializer()
                        .GetChaincodeActionDeserializer().ResponseMessageBytes;
            }
        }

        public string ResponseMessage
        {
            get
            {
                return IsFiltered ? null :
                    _transactionActionDeserializer
                        .GetPayloadDeserializer()
                        .GetChaincodeEndorsedActionDeserializer()
                        .GetProposalResponsePayloadDeserializer()
                        .GetChaincodeActionDeserializer()
                        .ResponseMessage;
            }
        }

        public byte[] ProposalResponseMessageBytes
        {
            get
            {
                return IsFiltered ? null :
                    _transactionActionDeserializer
                        .GetPayloadDeserializer()
                        .GetChaincodeEndorsedActionDeserializer()
                        .GetProposalResponsePayloadDeserializer()
                        .GetChaincodeActionDeserializer()
                        .ResponseMessageBytes;
            }
        }

        public int ProposalResponseStatus
        {
            get
            {
                return IsFiltered ? -1 :
                     _transactionActionDeserializer
                        .GetPayloadDeserializer()
                        .GetChaincodeEndorsedActionDeserializer()
                        .GetProposalResponsePayloadDeserializer()
                        .GetChaincodeActionDeserializer()
                        .ResponseStatus;
            }
        }

        public int ResponseStatus
        {
            get
            {
                return IsFiltered ? -1 :
                    _transactionActionDeserializer
                        .GetPayloadDeserializer()
                        .GetChaincodeEndorsedActionDeserializer()
                        .GetProposalResponsePayloadDeserializer()
                        .GetChaincodeActionDeserializer()
                        .ResponseStatus;
            }
        }

        public byte[] GetProposalResponsePayload
        {
            get
            {
                return IsFiltered ? null :
                    _transactionActionDeserializer.GetPayloadDeserializer()
                        .GetChaincodeEndorsedActionDeserializer()
                        .GetProposalResponsePayloadDeserializer()
                        .GetChaincodeActionDeserializer()
                        .Payload?.ToByteArray();
            }   
            
        }

        public fabricsdk.fabric.Events.ChaincodeEvent Event
        {
            get
            {
                if (IsFiltered)
                    return new fabricsdk.fabric.Events.ChaincodeEvent(_filteredChaincodeAction.CcEvent.ToByteString());

                return _transactionActionDeserializer
                    .GetPayloadDeserializer()
                    .GetChaincodeEndorsedActionDeserializer()
                    .GetProposalResponsePayloadDeserializer()
                    .GetChaincodeActionDeserializer()
                    .GetEvent();
            }
        }

        private int? _chaincodeInputArgsCount = null;
        public int ChaincodeInputArgsCount
        {
            get
            {
                if (IsFiltered)
                    return 0;

                if (!_chaincodeInputArgsCount.HasValue)
                {
                    _chaincodeInputArgsCount = _transactionActionDeserializer
                        .GetPayloadDeserializer()
                        .GetChaincodeProposalPayloadDeserializer()
                        .GetChaincodeInvocationSpecDeserializer()
                        .GetChaincodeInputDeserializer()
                        .GetChaincodeInput()
                        .Args.Count;
                }

                return _chaincodeInputArgsCount.Value;
            }
        }

        private int? _endorsementsCount = null;
        public int EndorsementsCount
        {
            get
            {
                if (IsFiltered)
                    return 0;

                if (_endorsementsCount.HasValue)
                {
                    _endorsementsCount = _transactionActionDeserializer
                        .GetPayloadDeserializer()
                        .GetChaincodeEndorsedActionDeserializer().EndorsmentsCount;
                }

                return _endorsementsCount.Value;
            }
        }

        public bool IsFiltered
        {
            get
            {
                return _filteredChaincodeAction != null;
            }
        }


        private TransactionActionDeserializer _transactionActionDeserializer;
        private FilteredChaincodeAction _filteredChaincodeAction;
        private List<EndorserInfo> _endorserInfos = null;

        public TransactionActionInfo(TransactionActionDeserializer transactionActionDeserializer)
        {
            _transactionActionDeserializer = transactionActionDeserializer;
        }

        public TransactionActionInfo(FilteredChaincodeAction filteredChaincodeAction)
        {
            _filteredChaincodeAction = filteredChaincodeAction;
        }

        public EndorserInfo GetEndorsementInfo(int index)
        {
            if (IsFiltered)
                return null;

            if (_endorserInfos == null)
            {
                _endorserInfos = new List<EndorserInfo>();
                var endorsements = _transactionActionDeserializer.GetPayloadDeserializer()
                    .GetChaincodeEndorsedActionDeserializer()
                    .GetChaincodeEndorsedAction()
                    .Endorsements;

                foreach (var endorsement in endorsements)
                    _endorserInfos.Add(new EndorserInfo(endorsement));
            }

            return _endorserInfos[index];
        }

        /**
            Get read write set for this transaction. Will return null on for Eventhub events.
            For eventhub events find the block by block number to get read write set if needed.
         */
        public TxReadWriteSetInfo GetTxReadWriteSet()
        {
            if (IsFiltered)
                return null;

            TxReadWriteSet txReadWriteSet = _transactionActionDeserializer
                .GetPayloadDeserializer()
                .GetChaincodeEndorsedActionDeserializer()
                .GetProposalResponsePayloadDeserializer()
                .GetChaincodeActionDeserializer()
                .GetResults();

            if (txReadWriteSet == null)
                return null;

            return new TxReadWriteSetInfo(txReadWriteSet);
        }

        public byte[] ChaincodeInputArgs(int index)
        {
            if (IsFiltered)
                return null;

            var input = _transactionActionDeserializer
                .GetPayloadDeserializer()
                .GetChaincodeProposalPayloadDeserializer()
                .GetChaincodeInvocationSpecDeserializer()
                .GetChaincodeInputDeserializer()
                .GetChaincodeInput();

            return input.Args[index].ToByteArray();
        }
    }
}