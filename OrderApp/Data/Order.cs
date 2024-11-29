using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderApp.Data
{
    internal class Order
    {
        public int Id { get; set; }
        public string OrderNumber {  get; set; }
        public string CustomerName { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal OrderAmount { get; set; }
    }
}
