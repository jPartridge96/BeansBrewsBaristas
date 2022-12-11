using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework.Input;
using BeansBrewsBaristas.BaseClassScripts;
using BeansBrewsBaristas.ComponentScripts;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;

namespace BeansBrewsBaristas.Managers
{
    public class CustomerManager
    {
        public const int QUEUE_LIMIT = 5;
        public const int CUST_LIMIT = 10;
        public static Order activeOrder;
        public static List<Keys> activeOrderKeys;
        public enum QueueDirection
        {
            LEFT,
            RIGHT
        }

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
            if (Customers.Count < CUST_LIMIT && OrderQueue.Count < QUEUE_LIMIT)
            {
                Customer cust = new Customer(
                    new Vector2(-125, Global.Stage.Y / 3),
                    GetRandomCustomerAsset(), Color.White,
                    750, 300
                );

                Customers.Add(cust);
                EnterQueue(cust, OrderQueue, Global.Stage.X / 8 * 3, QueueDirection.LEFT);
            }
        }

        public static void dequeueCustomer()
        {
            if(PickupQueue.Count > 0)
            {
                Customer customer = PickupQueue.Dequeue();
                Orders.Remove(customer.Order);
                DestroyCustomer(customer);

                if(Orders.Count > 0)
                    setActiveOrder(Orders[0]);
                else
                {
                    activeOrder = null;
                }
                // Update positon for all in PickupQueue
                foreach (Customer cust in PickupQueue)
                {
                    Vector2 newPos = new Vector2(
                        (Global.Stage.X / 8 * 5) + GetQueueIndex(cust, PickupQueue) * 50,
                        Global.Stage.Y / 2);

                    cust.TravelToPos(newPos);
                }
            }

        }

        /// <summary>
        /// Removes Customer from Game Components and from Customers list
        /// </summary>
        /// <param name="cust">The customer to be destroyed</param>
        /// <returns>True if customer successfully destroyed</returns>
        /// <exception cref="Exception">Thrown if Customers was not added to List</exception>
        public static async Task<bool> DestroyCustomer(Customer cust)
        {
            if (Customers.IndexOf(cust) != -1)
            {
                await cust.TravelToPos(new Vector2(Global.Stage.X, cust.Position.Y));
                Customers.Remove(cust);
                return true;
            }
            throw new Exception("Customer was not part of Customers list.");
        }

        public static void EnterQueue(Customer cust, Queue<Customer> queue, float xPos, QueueDirection direction)
        {
            queue.Enqueue(cust);

            int calcPos = 0;
            switch (direction)
            {
                case QueueDirection.LEFT:
                    calcPos = ((int)xPos - GetQueueIndex(cust, queue) * 50) - cust.Texture.Width;
                    break;
                case QueueDirection.RIGHT:
                    calcPos = (int)xPos + GetQueueIndex(cust, queue) * 50;
                    break;
            }

            Vector2 queuePos = new Vector2(
                calcPos,
                Global.Stage.Y / 2
            );

            cust.TravelToPos(queuePos);
        }

        public static void TransferQueue(Queue<Customer> oldQueue, Queue<Customer> newQueue, float xPos, QueueDirection direction)
        {
            if (newQueue.Count >= QUEUE_LIMIT)
                return;

            EnterQueue(oldQueue.Dequeue(), newQueue, (int)xPos, direction);
        }

        public static int GetQueueIndex(Customer cust, Queue<Customer> queue)
        {
            return queue.ToList().IndexOf(cust);
        }

        public static string GetCustomerName()
        {
            Random rand = new Random();
            return CustomerNames[rand.Next(0, CustomerNames.Length)];
        }

        //sets the current customer order in the order queue as the active order.
        //ui reflects the matching stuff
        public static Order setActiveOrder(Order nextOrder)
        {
            activeOrder = nextOrder;
            activeOrderKeys = new List<Keys>();
            foreach (var key in activeOrder.Modifications)
            {
                activeOrderKeys.Add((Keys)key.Control);
            }

            return activeOrder;
        }

        public static void TakeNextOrder()
        {

            if (OrderQueue.Count != 0)
            {
                Customer cust = OrderQueue.ToList()[0];

                TransferQueue(OrderQueue, PickupQueue, Global.Stage.X / 8 * 5, QueueDirection.RIGHT);


                Orders.Add(cust.Order);
                setActiveOrder(PickupQueue.ToList()[0].Order);

            }
            // Update positon for all in OrderQueue
            foreach (Customer cust in OrderQueue)
            {
                Vector2 newPos = new Vector2(
                    (Global.Stage.X / 8 * 3) - GetQueueIndex(cust, OrderQueue) * 50 - cust.Texture.Width,
                    Global.Stage.Y / 2);

                cust.TravelToPos(newPos);
            }
        }

        // Use to determine what modifications are able to go on what drinks?
        //public static Order GetCustomerOrder()
        //{
        //    return new Order();
        //}

        public static List<Customer> Customers = new List<Customer>();
        public static List<Order> Orders = new List<Order>();

        public static Queue<Customer> OrderQueue = new Queue<Customer>();
        public static Queue<Customer> PickupQueue = new Queue<Customer>();
    }
}
