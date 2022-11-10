using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BeansBrewsBaristas.Managers
{
    public class CustomerManager
    {
        public static Rectangle SpawnPoint { get; set; } = new Rectangle(0, (int)Global.Stage.Y / 3, 100, 200);
        public static List<Texture2D> CustomerAssets;

        public CustomerManager()
        {
            CustomerAssets = new List<Texture2D>()
            {
                Global.GameManager.Content.Load<Texture2D>("Images/ball (1)"),
            };
        }

        private static CustomerManager _instance;
        public static CustomerManager GetInstance()
        {
            if (_instance == null)
                _instance = new CustomerManager();
            return _instance;
        }


        /// <summary>
        /// Generates a random spawn position for the Customer
        /// </summary>
        /// <returns>Vector2 position relative to the Rectangle</returns>
        public static Vector2 GetSpawnPoint()
        {
            // Take coords of SpawnPoint rect
            // Generate random vector2 within bounds of rect
            // Return vector2
            return Vector2.Zero;
        }

        public static Texture2D GetRandomCustomerAsset()
        {
            Random rand = new Random();
            return CustomerAssets[rand.Next(CustomerAssets.Count)];
        }

        public static Customer CreateCustomer()
        {
            Customer cust = new Customer(
                CustomerManager.GetSpawnPoint(),
                GetRandomCustomerAsset(),
                Color.Green, 750, 300
            );

            Global.GameManager.Components.Add(cust);
            Customers.Add(cust);
            return cust; 
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

        private static List<Customer> Customers = new List<Customer>();

        private static Queue<Customer> OrderQueue = new Queue<Customer>();
        private static Queue<Customer> PickupQueue = new Queue<Customer>();
    }
}
