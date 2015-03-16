using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using NHibernate.Linq;
using OrmExample.Resource;
using Xunit;
namespace OrmExample.Test

{
    public class NestedQueryFact : OrmFactBase
    {
        [Fact]
        public void should_select_nested_data()
        {
            Order conanOrder = session
                .Query<Order>()
                .Where(order => order.User.Name == "Conan")
                .Fetch(order => order.OrderItems)
                .Single();

            Assert.Equal(2015, conanOrder.Date.Year);
            Assert.Equal("Conan", conanOrder.User.Name);
            Assert.Equal(3, conanOrder.OrderItems.Count);
            Assert.True(conanOrder.OrderItems.Any(oi => oi.Product.Name == "Glass"));
        }

        [Fact]
        public void should_update_nested_data()
        {
            var order = CreateOrder();
            Assert.Equal(2015, order.Date.Year);
            Assert.Equal("lulu", order.OrderItems[0].Product.Name);
            Assert.Equal("doudou", order.User.Name);
        }

        private Order CreateOrder()
        {
            var user = new User
            {
                Name = "doudou"
            };
            session.Save(user);

            var order = new Order
            {
                Date = DateTime.UtcNow,
                OrderItems = new List<OrderItem>
                {
                    new OrderItem
                    {
                        Product = new Product()
                        {
                            Name = "lulu"
                        }
                    }
                },
                User = user
            };
            session.Save(order);
            session.Flush();
            return order;
        }

        [Fact]
        public void should_delete_nested_data()
        {
            var order = CreateOrder();
            session.Delete(order);
            session.Flush();
            Assert.Equal(0, session.Query<Order>().Count(o => o.User.Name == "doudou"));
        }
    }
}