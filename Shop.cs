using System;
using System.Collections.Generic;
using System.Text;

namespace oop_2
{

    class Shop
    {

        private Dictionary<string, Item> catalog;
        private string code;
        private string name;
        private string address;

        public Shop(string code, string name, string address)
        {
            this.code = code;
            this.name = name;
            this.address = address;
            this.catalog = new Dictionary<string, Item>();
        }

        public string GetName()
        {
            return name;
        }

        public bool HasItem(string itemCode)
        {
            return catalog.ContainsKey(itemCode);
        }

        public void UpdateExistingItem(string itemCode, int quantity)
        {
            if (!HasItem(itemCode))
            {
                throw new Exception("no item with given id in this shop");
            }
            catalog[itemCode].ChangeQuantity(quantity);
        }

        public void UpdateExistingItem(string itemCode, int cost, int quantity)
        {
            if (!HasItem(itemCode))
            {
                throw new Exception("no item with given id in this shop");
            }
            catalog[itemCode].Update(cost, quantity);
        }

        public void AddItemIfNotPresent(Item item)
        {
            if (!HasItem(item.GetCode()))
            {
                catalog[item.GetCode()] = item;
            }
        }

        public void DeliverPackage(List<PackageItem> package)
        {
            foreach (PackageItem item in package)
            {
                item.AddToShop(this);
            }
        }

        public Item CheapestItemByName(string name)
        {
            int ans = -1;
            Item item = new Item("", "", -1);
            foreach (KeyValuePair<string, Item> i in catalog)
            {
                if (i.Value.GetName() == name)
                {
                    int cost = i.Value.GetCostIfAvailable();
                    if (cost != -1 && (ans == -1 || cost < ans))
                    {
                        ans = cost;
                        item = i.Value;
                    }
                }
            }
            return item;
        }

        public int GetItemCost(string code)
        {
            return catalog.ContainsKey(code) ? catalog[code].GetCost() : -1;
        }

        public int GetItemCostByName(string name)
        {
            return CheapestItemByName(name).GetCost();
        }

        public List<KeyValuePair<string, int>> GetItemsForMoney(int money)
        {
            List<KeyValuePair<string, int>> ans = new List<KeyValuePair<string, int>>();
            foreach (KeyValuePair<string, Item> i in catalog)
            {
                Item item = i.Value;
                int count = money / item.GetCost();
                ans.Add(new KeyValuePair<string, int>(item.GetName(), Math.Min(count, item.GetQuantity())));
            }
            return ans;
        }

        public int IsPackageAvailable(List<KeyValuePair<string, int>> package, bool byName = false)
        {
            int ans = 0;
            foreach (KeyValuePair<string, int> i in package)
            {
                Item item = new Item("", "", -1);
                if (!byName && catalog.ContainsKey(i.Key))
                {
                    item = catalog[i.Key];
                }
                if (byName)
                {
                    item = CheapestItemByName(i.Key);
                }
                if (item.GetCode() == "" || catalog[item.GetCode()].GetQuantity() < i.Value)
                {
                    return -1;
                }
                ans += item.GetCost() * i.Value;
            }
            return ans;
        }

        public int BuyPackage(List<KeyValuePair<string, int>> package, bool byName = false)
        {
            int status = IsPackageAvailable(package, byName);
            if (status == -1)
            {
                return -1;
            }
            else
            {
                foreach (KeyValuePair<string, int> i in package)
                {
                    if (byName)
                    {
                        CheapestItemByName(i.Key).ChangeQuantity(-i.Value);
                    }
                    else
                    {
                        catalog[i.Key].ChangeQuantity(-i.Value);
                    }
                }
                return status;
            }
        }

    }

}
