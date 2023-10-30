using DatabasePersistence;
using DatabasePersistence.DatabaseCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CallerPurchaseDataCapture
{
    public class Orders : DBModelsBaseObject
    {
        [DatabaseAttributes(isID = true)]
        public int Id
        {
            get; set;
        }

        public DateTime Date_Ordered { get; set; }

        public int Product_ID { get; set; }

        public double Qty { get; set; }

        public int Caller_ID { get; set; }

        public bool Cancelled { get; set; }

        public bool Complete { get; set; }

    }
}
