using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnRoute.Domain.Constants
{
    public enum OrderStatus
    {
        New,
        Delivered,
        CancelledByCustomer,
        CancelledByShop,
        Rejected,
        Fulfilled
    }
}
