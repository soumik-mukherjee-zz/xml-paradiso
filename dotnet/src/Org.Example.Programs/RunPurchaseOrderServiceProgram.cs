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
// Uncomment to enable debug trace
//#define DEBUG
using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using Org.Example.Services;
using Org.Example.DataContracts;
using Org.Example.SampleData;
namespace Org.Example.Programs{
    public class RunPurchaseOrderServiceProgram{
        private PurchaseOrderSampleData sampleData;
        private IPurchaseOrderService poService;
        private RunPurchaseOrderServiceProgram (){
          sampleData = PurchaseOrderSampleData.GetInstance();
          poService = (IPurchaseOrderService) new FileBasedPurchaseOrderService();
        }

        public static void Main(string[] args){
          Debug.Listeners.Add(new TextWriterTraceListener(Console.Out));
          Debug.AutoFlush = true;
          string USAGE = String.Format("Usage: RunDemo <PurchaseOrderId> where <PurchaseOrderId> one of {0} or {1}",PurchaseOrderSampleData.SAMPLE_PO_ID1,PurchaseOrderSampleData.SAMPLE_PO_ID2);
          try{
              if (null == args|| 0 == args.Length){
                throw new ArgumentException(String.Format("Purchase Order Id cannot be null.\n{0}",USAGE));
              }
              String selectedPoId = args[0];
              if (null == selectedPoId){
                throw new ArgumentException(String.Format("Purchase Order Id cannot be null.\n{0}",USAGE));
              }
              else if (PurchaseOrderSampleData.SAMPLE_PO_ID1 != selectedPoId && PurchaseOrderSampleData.SAMPLE_PO_ID2 != selectedPoId){
                throw new ArgumentException(String.Format("Unknown Purchase Order Id: {0}.\n{1}",selectedPoId,USAGE));
              }

              RunPurchaseOrderServiceProgram prg = new RunPurchaseOrderServiceProgram();

              // Simulating a back-end call to get  purchase order data by poId
              PurchaseOrderType po = prg.sampleData.GetPurchaseOrderById(selectedPoId);
              /*if (null == po){
                throw new ArgumentException(String.Format("No purchase order data for Id: {0}.\n{1}",selectedPoId, USAGE));
              }*/

              // Now send the data to partner as XML file
              prg.poService.SendPurchaseOrderToPartner(selectedPoId, po);
              Console.WriteLine(String.Format("Successfully generated PO file for PO Id: {0}.\nPress any key to exit ...",selectedPoId));
              String userKey = Console.ReadLine();
          }
          catch (Exception e){
            Console.WriteLine(e.Message);
            Debug.WriteLine(e.ToString());
            Console.WriteLine("Errors occured, quiting.\nPress any key to exit ...");
            String userKey = Console.ReadLine();
          }

        }
    }
}
