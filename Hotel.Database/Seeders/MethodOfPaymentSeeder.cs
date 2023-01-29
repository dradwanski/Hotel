using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hotel.Database.Entities;

namespace Hotel.Database.Seeders
{
    public class MethodOfPaymentSeeder : ISeeder<MethodOfPayment>
    {
        public IEnumerable<MethodOfPayment> GetDefaultValues()
        {
            return new[]
            {
                new MethodOfPayment()
                {
                    MethodOfPaymentName = "Blik"
                },
                new MethodOfPayment()
                {
                    MethodOfPaymentName = "Money"
                },
                new MethodOfPayment()
                {
                    MethodOfPaymentName = "ByCard"
                },
                new MethodOfPayment()
                {
                    MethodOfPaymentName = "Transfer"
                },
            };
        }
    }
}
