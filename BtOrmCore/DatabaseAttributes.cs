using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BtOrmCore
{
    public class DatabaseAttributes : Attribute
    {
        public bool isID { get; set; }

    }
}
