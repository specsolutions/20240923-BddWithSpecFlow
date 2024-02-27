using System;
using System.Linq;

namespace BddWithSpecFlow.GeekPizza.Web.DataAccess
{
    public class User
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public string Password { get; set; }
    }
}