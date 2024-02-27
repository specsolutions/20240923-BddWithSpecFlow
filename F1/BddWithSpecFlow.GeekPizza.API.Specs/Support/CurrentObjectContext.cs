using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BddWithSpecFlow.GeekPizza.Web.DataAccess;
using TechTalk.SpecFlow;

namespace BddWithSpecFlow.GeekPizza.Specs.Support
{
    public class CurrentObjectContext
    {
        public Dictionary<int, PizzaMenuItem> MenuItems { get; } = new();

        public void Map(IList<PizzaMenuItem> menuItems, Table tableWithTestId)
        {
            Assert.AreEqual(tableWithTestId.RowCount, menuItems.Count, "Cannot map list items to test id: different counts!");
            for (int i = 0; i < tableWithTestId.RowCount; i++)
            {
                MenuItems[int.Parse(tableWithTestId.Rows[i]["#"])] = menuItems[i];
            }
        }
    }
}
