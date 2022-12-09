using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BeansBrewsBaristas.Managers
{
    public class CustomerManager
    {
        public const int ORDER_LIMIT = 5;
        public const int PICKUP_LIMIT = 5;
        public const int CUST_LIMIT = 10;

        public enum QueueDirection
        {
            LEFT,
            RIGHT
        }

        // public static Rectangle SpawnPoint { get; set; } = new Rectangle(0, (int)Global.Stage.Y / 3, 100, 200);
        public static List<Texture2D> CustomerAssets;

        public static string[] CustomerNames = {
            "Eman", "Sabbir", "Morgan", "Malcom", "Ayush", "Gabriela",
            "Khaleil", "Nathan", "David", "Ritik", "Blake H.", "Yusuf",
            "Patrick", "Thomas", "Boa", "Ali", "Hyunjin", "Nancy",
            "Coby", "Daniela", "Jordan", "Kandarp", "Blake P.", "Rishabh",
            "Gloria", "Michael", "Jonathan", "Dev", "Bhupinderjeet", "Liam",
            "Elliot", "Danial", "Ashley", "Sheng"
        };

        public enum DrinkType
        {
            COFFEE,
            LATTE,
            ESPRESSO,
            TAKEOUT_COFFEE,
            TAKEOUT_LATTE
        }

        public CustomerManager()
        {
            CustomerAssets = new List<Texture2D>()
            {
                Global.GameManager.Content.Load<Texture2D>("Images/customer1"),
                Global.GameManager.Content.Load<Texture2D>("Images/customer2"),
                Global.GameManager.Content.Load<Texture2D>("Images/customer3"),
            };
        }

        private static CustomerManager _instance;
        public static CustomerManager GetInstance()
        {
            if (_instance == null)
                _instance = new CustomerManager();
            return _instance;
        }

        public static Texture2D GetRandomCustomerAsset()
        {
            Random rand = new Random();
            return CustomerAssets[rand.Next(CustomerAssets.Count)];
        }

        public static void CreateCustomer()
        {
            // If line of customers is below line limit
            if(Customers.Count < CUST_LIMIT && OrderQueue.Count < ORDER_LIMIT)
            {
                Customer cust = new Customer(
                    new Vector2(0, Global.Stage.Y / 3),
                    GetRandomCustomerAsset(), Color.White,
                    750, 300
                );

                Global.GameManager.Components.Add(cust);
                Customers.Add(cust);

                Debug.WriteLine($"\n\nName: {cust.Name}");
                Debug.WriteLine($"Order: \n{cust.Order}");

                EnterQueue(cust, OrderQueue, Global.Stage.X / 8 * 3, QueueDirection.RIGHT);
            }
        }

        /// <summary>
        /// Removes Customer from Game Components and from Customers list
        /// </summary>
        /// <param name="cust">The customer to be destroyed</param>
        /// <returns>True if customer successfully destroyed</returns>
        /// <exception cref="Exception">Thrown if Customers and Components lists are not in sync</exception>
        public static bool DestroyCustomer(Customer cust)
        {
            if (Global.GameManager.Components.IndexOf(cust) != -1)
            {
                if(Customers.IndexOf(cust) != -1)
                {
                    Global.GameManager.Components.Remove(cust);
                    Customers.Remove(cust);
                    return true;
                }
                throw new Exception("Customer was not part of Customers list.");
            }
            throw new Exception("Customer was not part of Game Components.");
        }

        public static async void EnterQueue(Customer cust, Queue<Customer> queue, float xPos, QueueDirection direction)
        {
            int calcPos = 0;
            switch (direction)
            {
                case QueueDirection.LEFT:
                    calcPos = (int)xPos - queue.Count * 25;
                    break;
                case QueueDirection.RIGHT:
                    calcPos = (int)xPos + queue.Count * 25;
                    break;
            }

            Vector2 queuePos = new Vector2(
                calcPos,
                Global.Stage.Y / 2
            );

            cust.TravelToPos(queuePos);
            queue.Enqueue(cust);
        }

        public static string GetCustomerName()
        {
            Random rand = new Random();
            return CustomerNames[rand.Next(0, CustomerNames.Length)];
        }

        //public static Order GetCustomerOrder()
        //{
        //    return new Order();
        //}

        private static List<Customer> Customers = new List<Customer>();

        private static Queue<Customer> OrderQueue = new Queue<Customer>();
        private static Queue<Customer> PickupQueue = new Queue<Customer>();
    }
}
