using System;
using System.Collections.Generic;
using System.Text;

namespace oop_2
{

    class Mall
    {

        private Dictionary<string, Shop> shops;

        public Mall()
        {
            this.shops = new Dictionary<string, Shop>();
        }

        public void CreateNewShop(string code, string name, string address)
        {
            Shop shop = new Shop(code, name, address);
            if (shops.ContainsKey(code))
            {
                throw new Exception("could not create shop, id already in use");
            }
            shops[code] = shop;
        }

        public void CreateNewItem(string shopCode, string itemCode, int itemCost, string itemName)
        {
            if (!shops.ContainsKey(shopCode))
            {
                throw new Exception("could not create item in this shop, invalid shop id");
            }
            shops[shopCode].AddItemIfNotPresent(new Item(itemCode, itemName, itemCost));
        }

        public void DeliverPackage(string shopCode, List<PackageItem> package)
        {
            if (!shops.ContainsKey(shopCode))
            {
                throw new Exception("could not deliver package to the shop, invalid shop id");
            }
            shops[shopCode].DeliverPackage(package);
        }

        public string FindCheapest(string itemName)
        {
            int ans = -1;
            string shopName = "";
            foreach (KeyValuePair<string, Shop> i in shops)
            {
                int cost = i.Value.GetItemCostByName(itemName);
                if (cost != -1 && (ans == -1 || cost < ans))
                {
                    ans = cost;
                    shopName = i.Key;
                }
            }
            if (shopName == "")
            {
                throw new Exception("no item with such code is present");
            }
            return shops[shopName].GetName();
        }

        public List<KeyValuePair<string, int>> GetItemsForMoneyFromShop(string shopCode, int money)
        {
            if (!shops.ContainsKey(shopCode))
            {
                throw new Exception("could not find shop, money saved! check shop name");
            }
            return shops[shopCode].GetItemsForMoney(money);
        }

        public int BuyPackageFromShop(string shopCode, List<KeyValuePair<string, int>> package)
        {
            if (!shops.ContainsKey(shopCode))
            {
                throw new Exception("could not find shop, money saved! your shoplist haven't been done :(");
            }
            int ans = shops[shopCode].BuyPackage(package);
            if (ans == -1)
            {
                throw new Exception("could not find all the items, not enough quantity");
            }
            return ans;
        }

        public string FindCheapestPackage(List<KeyValuePair<string, int>> package)
        {
            int ans = -1;
            string shopName = "";
            foreach (KeyValuePair<string, Shop> i in shops)
            {
                int packageCost = i.Value.IsPackageAvailable(package, true);
                if (packageCost != -1 && (ans == -1 || packageCost < ans))
                {
                    ans = packageCost;
                    shopName = i.Key;
                }
            }
            if (shopName == "")
            {
                throw new Exception("bad package, could not find it in any shop");
            }
            return shops[shopName].GetName();
        }

    }

}
