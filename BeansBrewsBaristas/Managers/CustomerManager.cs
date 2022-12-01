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

        public static Rectangle SpawnPoint { get; set; } = new Rectangle(0, (int)Global.Stage.Y / 3, 100, 200);
        public static List<Texture2D> CustomerAssets;

        public CustomerManager()
        {
            CustomerAssets = new List<Texture2D>()
            {
                // Global.GameManager.Content.Load<Texture2D>("Images/ball (1)"),
                Global.GameManager.Content.Load<Texture2D>("SpriteSheets/george"),
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
                // Calculate Order Queue pos
                Vector2 orderQueuePos = new Vector2(
                    (int)((Global.Stage.X / 8 * 3) - (OrderQueue.Count * 25)),
                    Global.Stage.Y / 2
                );

                Customer cust = new Customer(
                    new Vector2(0, Global.Stage.Y / 2),
                    GetRandomCustomerAsset(),
                    Color.Green, 750, 300
                );

                Global.GameManager.Components.Add(cust);
                Customers.Add(cust);

                EnterQueue(cust, OrderQueue);

                Debug.Output($"New customer travelling to '{orderQueuePos.X},{orderQueuePos.Y}'");
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

        public static async void EnterQueue(Customer customer, Queue<Customer> queue)
        {
            Vector2 queuePos = new Vector2(
                    (int)((Global.Stage.X / 8 * 3) - (queue.Count * 25)),
                    Global.Stage.Y / 2
                );
            customer.TravelToPos(queuePos);
            queue.Enqueue(customer);
        }

        private static List<Customer> Customers = new List<Customer>();

        private static Queue<Customer> OrderQueue = new Queue<Customer>();
        private static Queue<Customer> PickupQueue = new Queue<Customer>();
    }
}
