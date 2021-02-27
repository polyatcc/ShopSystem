using System;
using System.Collections.Generic;

namespace oop_2
{

    class Program
    {

        static List<KeyValuePair<string, int>> ReadPackage(string request)
        {
            Console.WriteLine("please enter number of items in the package:");
            int amount = Convert.ToInt32(Console.ReadLine());
            List<KeyValuePair<string, int>> package = new List<KeyValuePair<string, int>>();
            for (int j = 0; j < amount; j++)
            {
                Console.WriteLine("please enter item " + request + " and quantity");
                string[] words = Console.ReadLine().Split(' ');
                package.Add(new KeyValuePair<string, int>(words[0], Convert.ToInt32(words[1])));
            }
            return package;
        }

        static void PrintPackage(List<KeyValuePair<string, int>> package)
        {
            foreach (KeyValuePair<string, int> i in package)
            {
                Console.WriteLine(i.Key + " " + i.Value);
            }
        }

        static void Main(string[] args)
        {
            Mall mall = new Mall();

            mall.CreateNewShop("1", "okay", "address1");
            mall.CreateNewShop("2", "Tommy", "address2");
            mall.CreateNewShop("3", "Levi's", "address3");

            mall.CreateNewItem("1", "id1", 50, "Cola");
            mall.CreateNewItem("2", "id2", 7000, "Bag");
            mall.CreateNewItem("3", "id3", 5000, "Bag");
            mall.CreateNewItem("1", "id4", 60, "Water");
            mall.CreateNewItem("1", "id5", 300, "Bag");
            mall.CreateNewItem("1", "id7", 20, "BubbleGum");
            mall.CreateNewItem("1", "id8", 10, "Tomato");
            mall.CreateNewItem("1", "id9", 5, "Cucumber");
            mall.CreateNewItem("1", "id10", 10000, "Bicycle");

            mall.DeliverPackage("1", new List<PackageItem>(){
                new PackageItem("id1", "Cola", 100),
                new PackageItem("id5", "Bag", 6000, 10)
            });
            mall.DeliverPackage("2", new List<PackageItem>() {
                new PackageItem("id6", "Cringe", 120, 11)
            });

            Console.WriteLine(mall.FindCheapest("Bag"));

            bool running = true;
            while (running)
            {
                try
                {
                    Console.WriteLine("Select an option");
                    Console.WriteLine("1: create a new shop");
                    Console.WriteLine("2: create a new item");
                    Console.WriteLine("3: deliver a package");
                    Console.WriteLine("4: find the cheapest cost of an item");
                    Console.WriteLine("5: find items available for fixed amount of money");
                    Console.WriteLine("6: buy a package");
                    Console.WriteLine("7: find the cheapest cost of a package");
                    Console.WriteLine("8: exit");

                    var a = Console.ReadLine();
                    switch (a)
                    {
                        case "1":
                            Console.WriteLine("please enter code, name, address:");
                            string b = Console.ReadLine();
                            string c = Console.ReadLine();
                            string d = Console.ReadLine();
                            mall.CreateNewShop(b, c, d);
                            break;
                        case "2":
                            Console.WriteLine("please enter shop code, item code, item cost, item name:");
                            string e = Console.ReadLine();
                            string f = Console.ReadLine();
                            int g = Convert.ToInt32(Console.ReadLine());
                            string h = Console.ReadLine();
                            mall.CreateNewItem(e, f, g, h);
                            break;
                        case "3":
                            Console.WriteLine("please enter shop code:");
                            string shopCode = Console.ReadLine();
                            Console.WriteLine("please enter number of delivered types of items:");
                            int i = Convert.ToInt32(Console.ReadLine());
                            List<PackageItem> package = new List<PackageItem>();
                            for (int j = 0; j < i; j++)
                            {
                                Console.WriteLine("please enter item code, item name, cost (if u want to change it) and quantity:");
                                string k = Console.ReadLine();
                                string[] words = k.Split(' ');
                                if (words.Length == 3)
                                {
                                    package.Add(new PackageItem(words[0], words[1], Convert.ToInt32(words[2])));
                                }
                                else if (words.Length == 4)
                                {
                                    package.Add(new PackageItem(words[0], words[1], Convert.ToInt32(words[2]), Convert.ToInt32(words[3])));
                                }
                                else
                                {
                                    throw new Exception("invalid item description, expected 3 or 4 words");
                                }
                            }
                            mall.DeliverPackage(shopCode, package);
                            break;
                        case "4":
                            Console.WriteLine("please enter item name:");
                            string l = Console.ReadLine();
                            Console.WriteLine(mall.FindCheapest(l));
                            break;
                        case "5":
                            Console.WriteLine("please enter shop code:");
                            string nameShop = Console.ReadLine();
                            Console.WriteLine("please enter how much money would u like to spend:");
                            int money = Convert.ToInt32(Console.ReadLine());
                            PrintPackage(mall.GetItemsForMoneyFromShop(nameShop, money));
                            break;
                        case "6":
                            Console.WriteLine("please enter shop code:");
                            string nameShop7 = Console.ReadLine();
                            List<KeyValuePair<string, int>> buypartion = ReadPackage("code");
                            int outans = mall.BuyPackageFromShop(nameShop7, buypartion);
                            Console.WriteLine("Total cost: " + outans);
                            break;
                        case "7":
                            List<KeyValuePair<string, int>> buyshoplist = ReadPackage("name");
                            Console.WriteLine(mall.FindCheapestPackage(buyshoplist));
                            break;
                        case "8":
                            running = false;
                            break;
                        default:
                            throw new Exception("invalid number entered");
                    }
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.Message);
                }
            }
        }
    }
}
