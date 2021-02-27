using System;
using System.Collections.Generic;
using System.Text;

namespace oop_2
{

    class Item
    {

        private int cost;
        private string code;
        private string name;
        private int quantity;

        public Item(string code, string name, int cost)
        {
            this.code = code;
            this.name = name;
            this.cost = cost;
            this.quantity = 0;
        }

        public string GetCode()
        {
            return code;
        }

        public string GetName()
        {
            return name;
        }

        public int GetCost()
        {
            return cost;
        }

        public int GetCostIfAvailable()
        {
            return quantity > 0 ? cost : -1;
        }

        public int GetQuantity()
        {
            return quantity;
        }

        public void SetCost(int cost)
        {
            this.cost = cost;
        }

        public void ChangeQuantity(int quantity)
        {
            this.quantity += quantity;
        }

        public void Update(int cost, int quantity)
        {
            SetCost(cost);
            ChangeQuantity(quantity);
        }

        public bool IsAvailable(int amount)
        {
            return amount <= this.quantity;
        }

        public int Buy(int amount)
        {
            this.quantity -= amount;
            return amount * this.cost;
        }

    }

}
