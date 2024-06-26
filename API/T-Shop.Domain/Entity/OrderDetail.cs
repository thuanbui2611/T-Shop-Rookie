﻿using System.ComponentModel.DataAnnotations.Schema;

namespace T_Shop.Domain.Entity;
public class OrderDetail
{
    [Column("PK_FK_order_id")]
    public Guid OrderID { get; set; }
    [Column("PK_FK_product_id")]
    public Guid ProductID { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public virtual Order Order { get; set; }
    public virtual Product Product { get; set; }
    public virtual ProductReview ProductReview { get; set; }
}
