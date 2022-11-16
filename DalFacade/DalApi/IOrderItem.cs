using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalApi
{
    public interface IOrderItem: ICrud<OrderItem>
    {
        public List<OrderItem> ReadByOrderID(int id);
        public OrderItem ReadByProductIDAndOrderID(int productId, int orderId);

    }
}
