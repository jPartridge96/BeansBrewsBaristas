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
    /// <summary>
    /// Handles all of the customer spawning and manipulation
    /// </summary>
    public class CustomerManager
    {
        public const int QUEUE_LIMIT = 5;
        public const int CUST_LIMIT = 10;
        public static Order activeOrder;
        public static List<Keys> activeOrderKeys;

        private static Random rand = new Random();

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

        /// <summary>
        /// Instansiates an array of customer skins
        /// </summary>
        public CustomerManager()
        {
            // TODO: Convert List to dictionary, so dog type can be detected?
            CustomerAssets = new List<Texture2D>()
            {
                Global.GameManager.Content.Load<Texture2D>("Images/customer1"),
                Global.GameManager.Content.Load<Texture2D>("Images/customer2"),
                Global.GameManager.Content.Load<Texture2D>("Images/customer3"),
                Global.GameManager.Content.Load<Texture2D>("Images/customer4"),
                Global.GameManager.Content.Load<Texture2D>("Images/customer5"),
                Global.GameManager.Content.Load<Texture2D>("Images/customer6"),
                Global.GameManager.Content.Load<Texture2D>("Images/customer7"),
            };
        }

        /// <summary>
        /// Creates Singleton instance of CustomerManager
        /// </summary>
        /// <returns>Singleton Instance</returns>
        public static CustomerManager GetInstance()
        {
            if (_instance == null)
                _instance = new CustomerManager();
            return _instance;
        }
        private static CustomerManager _instance;

        /// <summary>
        /// 
        /// </summary>
        /// <returns>Random Customer Image from Array</returns>
        public static Texture2D GetRandomCustomerAsset()
        {    
            return CustomerAssets[rand.Next(CustomerAssets.Count)];
        }

        /// <summary>
        /// Task that generates a customer every 3-10 seconds with 
        /// </summary>
        /// <returns>Completed Task</returns>
        public static async Task CreateCustomer()
        {
            // While a level is loaded
            if (SceneManager.ActiveScene != null)
            {
                if (SceneManager.ActiveScene.Contains("Level"))
                {
                    // If line of customers is below line limit
                    if (Customers.Count < CUST_LIMIT && OrderQueue.Count < QUEUE_LIMIT)
                    {
                        Customer cust = new Customer(
                            new Vector2(-150, Global.Stage.Y / 3),
                            GetRandomCustomerAsset(), Color.White,
                            750, 300
                        );


                        Customers.Add(cust);
                        EnterQueue(cust, OrderQueue, Global.Stage.X / 8 * 3, QueueDirection.LEFT);
                        AudioManager.PlaySound("CatMeow");
                        await Task.Delay(rand.Next(3000, 10000));
                        CreateCustomer();
                    }
                    
                }
            }
        }

        /// <summary>
        /// Removes customer from queue, deletes them, and shifts all customers up to new positions
        /// </summary>
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

        /// <summary>
        /// Moves
        /// </summary>
        /// <param name="oldQueue">queue to remove from</param>
        /// <param name="newQueue">New queue to transfer to</param>
        /// <param name="xPos">new position</param>
        /// <param name="direction">Line up direction</param>
        public static void TransferQueue(Queue<Customer> oldQueue, Queue<Customer> newQueue, float xPos, QueueDirection direction)
        {
            if (newQueue.Count >= QUEUE_LIMIT)
                return;

            EnterQueue(oldQueue.Dequeue(), newQueue, (int)xPos, direction);
        }

        /// <summary>
        /// Gets the index of the customer in the queue
        /// </summary>
        /// <param name="cust">Customer queried</param>
        /// <param name="queue">Queue to query</param>
        /// <returns>Index of customer in queue</returns>
        public static int GetQueueIndex(Customer cust, Queue<Customer> queue)
        {
            return queue.ToList().IndexOf(cust);
        }

        /// <summary>
        /// Generates a random customer name from stored array
        /// </summary>
        /// <returns>Customer name</returns>
        public static string GetCustomerName()
        {
            Random rand = new Random();
            return CustomerNames[rand.Next(0, CustomerNames.Length)];
        }

        /// <summary>
        /// sets the current customer order in the order queue as the active order. Used for UI
        /// </summary>
        /// <param name="nextOrder">The order to be updated to</param>
        /// <returns>The updated order</returns>
        public static Order setActiveOrder(Order nextOrder)
        {
            activeOrder = nextOrder;
            activeOrderKeys = new List<Keys>();
            foreach (var key in activeOrder.Modifications)
            {
                activeOrderKeys.Add((Keys)key.Control);
            }
            foreach (var key in activeOrder.PreModKeys)
            {
                activeOrderKeys.Add((Keys)key);
            }
            foreach (var key in activeOrder.PostModKeys)
            {
                activeOrderKeys.Add((Keys)key);
            }

            foreach (Keys key in activeOrderKeys)
                Debug.WriteLine(key.ToString());

            nextOrder.DrinkDrawnIndex = new int[activeOrder.DrinkAssets.Length];
            nextOrder.DrinkDrawnIndex[0] = -1;

            GameManager.Frames = nextOrder.DrinkFrames;

            return activeOrder;
        }


        /// <summary>
        /// Takes the Customer from the orderQueue and moves to pickupQueue 
        /// </summary>
        public static void TakeNextOrder()
        {
            if (OrderQueue.Count != 0 && PickupQueue.Count < QUEUE_LIMIT)
            {
                Customer cust = OrderQueue.ToList()[0];

                TransferQueue(OrderQueue, PickupQueue, Global.Stage.X / 8 * 5, QueueDirection.RIGHT);


                Orders.Add(cust.Order);
                if(activeOrder == null)
                {
                    setActiveOrder(PickupQueue.ToList()[0].Order);
                    AudioManager.PlaySound("Ding");
                }
                

                // Update positon for all in OrderQueue
                foreach (Customer customer in OrderQueue)
                {
                    Vector2 newPos = new Vector2(
                        (Global.Stage.X / 8 * 3) - GetQueueIndex(customer, OrderQueue) * 50 - cust.Texture.Width,
                        Global.Stage.Y / 2);

                    customer.TravelToPos(newPos);
                }
            }
        }

        public static List<Customer> Customers = new List<Customer>();
        public static List<Order> Orders = new List<Order>();

        public static Queue<Customer> OrderQueue = new Queue<Customer>();
        public static Queue<Customer> PickupQueue = new Queue<Customer>();
    }
}
