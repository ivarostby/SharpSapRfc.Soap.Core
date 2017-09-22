﻿using SharpSapRfc.Metadata;
using SharpSapRfc.Soap.Configuration;

namespace SharpSapRfc.Soap.Core
{
    public class SoapSapRfcConnection : SapRfcConnection
    {
        public SapSoapRfcDestination Destination { get; private set; }
        private SoapRfcWebClient _webClient;
        private SoapRfcMetadataCache metadataCache;

        private SoapRfcStructureMapper structureMapper;

        public SoapSapRfcConnection(SapSoapRfcDestination destination)
        {
            this.Destination = destination;
            this._webClient = new SoapRfcWebClient(this.Destination);
            this.metadataCache = new SoapRfcMetadataCache(this._webClient);
            this.structureMapper = new SoapRfcStructureMapper(new SoapRfcValueMapper());
        }

        public override RfcPreparedFunction PrepareFunction(string functionName)
        {
            FunctionMetadata metadata = this.metadataCache.GetFunctionMetadata(functionName);
            return new SoapRfcPreparedFunction(metadata, this.structureMapper, this._webClient);
        }

        public override void Dispose()
        {

        }

        public override RfcStructureMapper GetStructureMapper()
        {
            return this.structureMapper;
        }

        public override void SetTimeout(int timeout)
        {
            this.Destination.Timeout = timeout;
        }
    }
}
