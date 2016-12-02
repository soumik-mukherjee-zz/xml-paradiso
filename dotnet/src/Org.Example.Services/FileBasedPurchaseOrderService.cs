/**
Copyright 2016 Soumik Mukherjee
   Licensed under the Apache License, Version 2.0 (the "License");
   you may not use this file except in compliance with the License.
   You may obtain a copy of the License at
       http://www.apache.org/licenses/LICENSE-2.0
   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
   See the License for the specific language governing permissions and
   limitations under the License.

    ## Release: v1.0
**/
using System;
using System.Xml;
using System.Text;
using System.Diagnostics;
using System.Runtime.Serialization;
using System.IO;
using Org.Example.DataContracts;
namespace Org.Example.Services {
  public class FileBasedPurchaseOrderService:IPurchaseOrderService {
    public void SendPurchaseOrderToPartner (String poId, PurchaseOrderType po){
            String fileCreationTimeStamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
            // Produce a XML file with the name format poId_yyyyMMdd_HHmmss.xml
            String xmlOutFilePath = @"PurchaseOrders\" + poId + "_" + fileCreationTimeStamp + ".xml";
            FileStream outputFileStream = new FileStream(xmlOutFilePath, FileMode.Create);
            // Wrap file stream in XML Writer, control encoding. Default is UTF8.
            XmlTextWriter xmlFileWriter = new XmlTextWriter (outputFileStream, Encoding.UTF8);
            DataContractSerializer dcSerializer = new DataContractSerializer(typeof(PurchaseOrderType));
            dcSerializer.WriteObject(xmlFileWriter, po);
            xmlFileWriter.Close();
    }
  }
}
