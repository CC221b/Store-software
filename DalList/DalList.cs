﻿using DalApi;
using System.Security.Principal;

namespace Dal;

sealed public class DalList: IDal
{
    public IOrder Order => new DalOrder();
    public IOrderItem OrderItem => new DalOrderItem();
    public IProduct Product => new DalProduct();
}