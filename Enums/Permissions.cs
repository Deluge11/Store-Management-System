using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enums;

public enum Permission
{
    Inventory_Check = 1,
    Orders_Manage,
    Stocks_Create,
    Stocks_Add_Quantity,
    Stocks_Accept_Add_Quantity,
    Trucks_Create_Connection_String,
    Trucks_Manage_Connection,
    Trucks_Manage_Stocks,
}
