using System;
using System.Collections.Generic;
using System.Text;

namespace oop_2
{

    class PackageItem
    {

        private string code;
        private string name;
        private int quantity;

        private bool changeCost;
        private int newCost;

        public PackageItem(string code, string name, int quantity)
        {
            this.code = code;
            this.name = name;
            this.quantity = quantity;
            changeCost = false;
        }

        public PackageItem(string code, string name, int cost, int quantity)
        {
            this.code = code;
            this.name = name;
            this.quantity = quantity;
            newCost = cost;
            changeCost = true;
        }

        public void AddToShop(Shop shop)
        {
            if (!shop.HasItem(code))
            {
                if (!changeCost)
                {
                    throw new Exception("expected a cost set for a new item");
                }
                shop.AddItemIfNotPresent(new Item(code, name, newCost));
            }
            if (!changeCost)
            {
                shop.UpdateExistingItem(code, quantity);
            }
            else
            {
                shop.UpdateExistingItem(code, newCost, quantity);
            }
        }

    }

}
