using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using BddWithReqnroll.GeekPizza.Web.Services;
using Newtonsoft.Json;

namespace BddWithReqnroll.GeekPizza.Web.DataAccess
{
    /// <summary>
    /// A database connection to perform data query and manipulation
    /// </summary>
    public class DataContext
    {
        public List<PizzaMenuItem> MenuItems { get; } = new List<PizzaMenuItem>();
        public List<User> Users { get; set; } = new List<User>();
        public List<Order> MyOrders { get; set; } = new List<Order>();

        public DataContext()
        {
            LoadData();
        }

        public Order GetMyOrder(string userName)
        {
            var myOrder = MyOrders.FirstOrDefault(u => u.UserName.Equals(userName, StringComparison.CurrentCultureIgnoreCase));
            if (myOrder == null)
            {
                myOrder = Order.CreateOrder(userName);
                MyOrders.Add(myOrder);
            }

            return myOrder;
        }

        public void DeleteMyOrder(string userName)
        {
            var myOrder = MyOrders.FirstOrDefault(u => u.UserName.Equals(userName, StringComparison.CurrentCultureIgnoreCase));
            if (myOrder != null)
                MyOrders.Remove(myOrder);
        }

        public User FindUserByName(string userName)
        {
            return Users.FirstOrDefault(u => u.Name.Equals(userName, StringComparison.CurrentCultureIgnoreCase));
        }

        public PizzaMenuItem FindMenuItemByName(string menuItemName)
        {
            return MenuItems.FirstOrDefault(u => u.Name.Equals(menuItemName, StringComparison.CurrentCultureIgnoreCase));
        }

        public void SaveChanges()
        {
            SaveData();
        }

        public void TruncateTables()
        {
            ClearData();
            SaveData();
        }

        #region Simulating Database

        private class Database
        {
            public PizzaMenuItem[] PizzaMenuItems { get; set; }
            public User[] Users { get; set; }
            public Order[] MyOrders { get; set; }
        }

        private readonly IDataPersist _dataPersist =
            AgentServices.IsAgentMode ? new AgentMemoryPersist()
            : new TempFileDataPersist();

        private void ClearData()
        {
            MenuItems.Clear();
            Users.Clear();
            MyOrders.Clear();
        }

        private void SaveData()
        {
            var db = new Database
            {
                PizzaMenuItems = MenuItems.ToArray(),
                Users = Users.ToArray(),
                MyOrders = MyOrders.ToArray()
            };
            var json = JsonConvert.SerializeObject(db);
            _dataPersist.SaveToFile(json);
        }

        private void LoadData()
        {
            var json = _dataPersist.LoadFromFile();
            ClearData();
            if (string.IsNullOrEmpty(json) || !DeserializeDatabase(json, out var db))
            {
                DefaultDataServices.SeedWithDefaultData(this);
                return;
            }
            MenuItems.AddRange(db.PizzaMenuItems ?? new PizzaMenuItem[0]);
            Users.AddRange(db.Users ?? new User[0]);
            MyOrders.AddRange(db.MyOrders ?? new Order[0]);
        }

        private bool DeserializeDatabase(string json, out Database database)
        {
            try
            {
                database = JsonConvert.DeserializeObject<Database>(json);
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex, "DeserializeDatabase");
                database = null;
                return false;
            }
        }

        private interface IDataPersist
        {
            void SaveToFile(string json);
            string LoadFromFile();
        }

        private class TempFileDataPersist : IDataPersist
        {
            private static readonly object LockObj = new object();

            private readonly string _databaseFilePath = Path.Combine(Path.GetTempPath(), "GeekPizzaDb.json");

            public void SaveToFile(string json)
            {
                lock (LockObj)
                {
                    File.WriteAllText(_databaseFilePath, json, Encoding.UTF8);
                }
            }

            public string LoadFromFile()
            {
                lock (LockObj)
                {
                    if (!File.Exists(_databaseFilePath))
                        return string.Empty;
                    return File.ReadAllText(_databaseFilePath, Encoding.UTF8);
                }
            }
        }

        private class AgentMemoryPersist : IDataPersist
        {
            private static readonly Dictionary<string, string> AgentDatabases = new Dictionary<string, string>();

            public void SaveToFile(string json)
            {
                var agent = AgentServices.GetAgent();
                lock (AgentDatabases)
                {
                    AgentDatabases[agent] = json;
                }
            }

            public string LoadFromFile()
            {
                var agent = AgentServices.GetAgent();
                string dbContent;
                lock (AgentDatabases)
                {
                    if (!AgentDatabases.TryGetValue(agent, out dbContent))
                        return string.Empty;
                }
                return dbContent;
            }
        }

        #endregion
    }
}