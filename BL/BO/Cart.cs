﻿
using System.Security.Cryptography;

namespace BO;

public class Cart
{
    public string CustomerName { get; set; }
    public string CustomerEmail { get; set; }
    public string CustomerAdress { get; set; }
    public List<OrderItem> Items { get; set; }
    public double TotalPrice { get; set; }

    public override string ToString() => $@"
    customerName: {CustomerName}
    customerEmail: {CustomerEmail}
    customerAdress: {CustomerAdress}
    orderItemList: {Items}
    price: {TotalPrice}";
}
