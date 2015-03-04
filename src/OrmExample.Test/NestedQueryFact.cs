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
    }
}