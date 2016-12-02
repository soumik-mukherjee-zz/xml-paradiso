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
using System.Collections.Generic;
using System.Text;
using Org.Example.DataContracts;
namespace Org.Example.SampleData{
  public class PurchaseOrderSampleData{
    private static PurchaseOrderSampleData singletonObj;
    private Dictionary<String,USAddress> shipToAddresses;
    private Dictionary<String,USAddress> billToAddresses;
    private Dictionary<String,Items> purchaseOrderItems;
    private Dictionary<String, PurchaseOrderType> purchaseOrders;
    public const string SAMPLE_PO_ID1 = "DummyPO1";
    public const string SAMPLE_PO_ID2 = "DummyPO2";
    private PurchaseOrderSampleData(){
      InitPurchaseOrderData();
    }

    public static PurchaseOrderSampleData GetInstance(){
      if (null == singletonObj){
        singletonObj = new PurchaseOrderSampleData();
      }
      return singletonObj;
    }

    public PurchaseOrderType GetPurchaseOrderById(String poId){
      return purchaseOrders [poId];
    }

    private void InitPurchaseOrderData (){
      InitShipToAddress();
      InitBillToAddress();
      InitItems();
      purchaseOrders = new Dictionary<String, PurchaseOrderType>();
      PurchaseOrderType po1 = new PurchaseOrderType();
      po1.shipTo = shipToAddresses[SAMPLE_PO_ID1];
      po1.billTo = billToAddresses[SAMPLE_PO_ID1];
      po1.items = purchaseOrderItems[SAMPLE_PO_ID1];
      // Ordered on 1st November 2016
      po1.orderDate = new DateTime(2016,11,1);
      // Confirmed on 3rd November 2016
      po1.confirmDate = new DateTime(2016,11,3);
      // Comment can be set to null as per XSD
      po1.comment = null;
      purchaseOrders.Add(SAMPLE_PO_ID1,po1);

      PurchaseOrderType po2 = new PurchaseOrderType();
      po2.shipTo = shipToAddresses [SAMPLE_PO_ID2];
      po2.billTo = billToAddresses [SAMPLE_PO_ID2];
      po2.items = purchaseOrderItems [SAMPLE_PO_ID2];
      // Ordered on 21st November 2016
      po2.orderDate = new DateTime(2016,11,21);
      // Confirmed on 23rd November 2016
      po2.confirmDate = new DateTime(2016,11,23);
      po2.comment = String.Format("PO Id:{0}",SAMPLE_PO_ID2);
      purchaseOrders.Add(SAMPLE_PO_ID2,po2);
    }

    // Populate some fake US address data
    private void InitShipToAddress (){
       shipToAddresses = new Dictionary<String,USAddress>();
      // Add a ShipTo for PO1
      USAddress shipToPO1 = new USAddress();
      shipToPO1.name = "Brian C Mercado";
      shipToPO1.street = "3190 Vesta Drive";
      shipToPO1.city = "Hickory Hills";
      shipToPO1.state = "Illinois";
      shipToPO1.zip = 60457;
      shipToAddresses.Add(SAMPLE_PO_ID1,shipToPO1);
      // Add a ShipTo for PO2
      USAddress shipToPO2 = new USAddress();
      shipToPO2.name = "Brian C Mercado";
      shipToPO2.street = "3190 Vesta Drive";
      shipToPO2.city = "Hickory Hills";
      shipToPO2.state = "Illinois";
      shipToPO2.zip = 60457;
      shipToAddresses.Add(SAMPLE_PO_ID2,shipToPO2);
    }

    private void InitBillToAddress (){
      billToAddresses = new Dictionary<String,USAddress>();
      // Add a BillTo for PO1
      USAddress billTo1 = new USAddress();
      billTo1.name = "Lemuel J Gentner";
      billTo1.street = "4593 Emily Renzelli Boulevard";
      billTo1.city = "Aptos";
      billTo1.state = "California";
      billTo1.zip = 95003;
      billToAddresses.Add(SAMPLE_PO_ID1,billTo1);
      // Add a BillTo for PO2
      USAddress billTo2 = new USAddress();
      billTo2.name = "Lemuel J Gentner";
      billTo2.street = "4593 Emily Renzelli Boulevard";
      billTo2.city = "Aptos";
      billTo2.state = "California";
      billTo2.zip = 95003;
      billToAddresses.Add(SAMPLE_PO_ID2,billTo2);
    }

    private void InitItems (){
      purchaseOrderItems = new Dictionary<String,Items>();
      // For PO1
      // Lets return 2 hardcoded items - 1 Asset and another Consumable
      Items itemsPO1 = new Items();
      // item1 - HP HP Deskjet 2001 Printer
      item itm1 = new item();
      itm1.itemType = ItemType.Asset;
      itm1.productName = "HP Deskjet 2001 Printer";
      itm1.quantity = 10;
      itm1.USPrice = 100.00M;
      // shipDate can be set to null as per XSD
      itm1.shipDate = null;
      itm1.partNum = "HPDJ2001";
      itm1.comment = "A funky comment";
      itemsPO1.Add(itm1);
      // item2 - Pack of 2xDuracell AA
      item itm2 = new item();
      itm2.itemType = ItemType.Consumable;
      itm2.productName = "Pack of 2xDuracell AA";
      itm2.quantity = 1000;
      itm2.USPrice = 1.00M;
      // Shipping on 20th November 2016
      itm2.shipDate = new DateTime(2016,11,20);
      itm2.partNum = "DURACELLAAx2";
      // Comment can be set to null as per XSD
      itm2.comment = null;
      itemsPO1.Add(itm2);
      purchaseOrderItems.Add(SAMPLE_PO_ID1,itemsPO1);
      // For PO2
      // Lets return 2 hardcoded items - 1 Asset and another Consumable
      Items itemsPO2 = new Items();
      // item1 - HP HP Deskjet 2001 Printer
      item itm3 = new item();
      itm3.itemType = ItemType.Asset;
      itm3.productName = "HP Deskjet 2010 Printer";
      itm3.quantity = 18;
      itm3.USPrice = 100.00M;
      // shipDate can be set to null as per XSD
      itm3.shipDate = null;
      itm3.partNum = "HPDJ2010";
      itm3.comment = "Includes 1yr onsite service garuntee";
      itemsPO2.Add(itm3);
      // item2 - Pack of 2xDuracell AA
      item itm4 = new item();
      itm4.itemType = ItemType.Consumable;
      itm4.productName = "Pack of 2xDuracell AA";
      itm4.quantity = 1000;
      itm4.USPrice = 1.00M;
      // Shipping on 20th November 2016
      itm4.shipDate = new DateTime(2016,11,20);
      itm4.partNum = "DURACELLAAx2";
      // Comment can be set to null as per XSD
      itm4.comment = null;
      itemsPO2.Add(itm4);
      purchaseOrderItems.Add(SAMPLE_PO_ID2,itemsPO2);
    }
  }
}
