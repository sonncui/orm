using System;
using System.Collections.Generic;
using FluentNHibernate.Mapping;

namespace OrmExample.Resource
{
    public class Order
    {
        public virtual long Id { get; set; }
        public virtual User User { get; set; }
        public virtual DateTime Date { get; set; }
        public virtual IList<OrderItem> OrderItems { get; set; }
    }

    public class OrderMap : ClassMap<Order>
    {
        public OrderMap()
        {
            Table("[order]");
            Id(o => o.Id).Column("id");
            References(o => o.User).Column("user_id");
            Map(o => o.Date).Column("date");
            HasMany(o => o.OrderItems)
                .KeyColumn("order_id")
                .Inverse()
                .Cascade.Delete();
        }
    }

    public class OrderItem
    {
        public virtual long Id { get; set; }
        public virtual Product Product { get; set; }
    }

    public class OrderItemMap : ClassMap<OrderItem>
    {
        public OrderItemMap()
        {
            Table("[order_items]");
            Id(orderItem => orderItem.Id).Column("id");
            References(orderItem => orderItem.Product).Column("product_id");
        }
    }

    public class Product
    {
        public virtual long Id { get; set; }
        public virtual string Name { get; set; }
    }

    public class ProductMap : ClassMap<Product>
    {
        public ProductMap()
        {
            Table("product");
            Id(p => p.Id).Column("id");
            Map(p => p.Name).Column("name");
        }
    }
}